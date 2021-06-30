using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
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

        public AccountService(CharacterDbContext context)
        {
            _context = context;
        }

        public void RegisterGamer(RegisterGamerDto dto)
        {
            var newGamer = new Gamer()
            {
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            _context.Gamers.Add(newGamer);
            _context.SaveChanges();
        }
    }
}
