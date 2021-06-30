using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Services
{

    public interface IAccountService
    {
        void RegisterGamer(RegisterGamerDto dto);
    }

    public class AccountService : IAccountService 
    {
        private readonly CharacterDbContext _context;
        private readonly IPasswordHasher<Gamer> _passwordHasher;

        public AccountService(CharacterDbContext context, IPasswordHasher<Gamer> passwordHasher )
        {
            _context = context;
            _passwordHasher = passwordHasher;
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
