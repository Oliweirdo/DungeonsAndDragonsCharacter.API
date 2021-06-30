using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
using DungeonsAndDragonsCharacter.API.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DungeonsAndDragonsCharacter.API.Services
{

    public interface IAccountService
    {
        void RegisterGamer(RegisterGamerDto dto);
        string GenerateJwt(LoginDto dto);
    }

    public class AccountService : IAccountService 
    {
        private readonly CharacterDbContext _context;
        private readonly IPasswordHasher<Gamer> _passwordHasher;
        private readonly AutenticationSettings _autenticationSettings;

        public AccountService(CharacterDbContext context, IPasswordHasher<Gamer> passwordHasher, AutenticationSettings autenticationSettings )
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _autenticationSettings = autenticationSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var gamer = _context.Gamers
                .Include(u=>u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            
            if(gamer is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result=  _passwordHasher.VerifyHashedPassword(gamer, gamer.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, gamer.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{gamer.FirstName}{gamer.LastName}"),
                new Claim(ClaimTypes.Role, $"{gamer.Role.Name}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_autenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_autenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_autenticationSettings.JwtIssuer,
                _autenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }

        public void RegisterGamer(RegisterGamerDto dto)
        {
            var newGamer = new Gamer()
            {
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            var hashPassword = _passwordHasher.HashPassword(newGamer, dto.Password);

            newGamer.PasswordHash = hashPassword;
            _context.Gamers.Add(newGamer);
            _context.SaveChanges();
        }
    }
}
