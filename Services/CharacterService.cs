using AutoMapper;
using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Services
{
    public interface ICharacterService
    {
        CharacterDto GetById(int id);
        IEnumerable<CharacterDto> GetAll();
        int Create(CreateCharacterDto dto);
        bool Delete(int id);
    }


    public class CharacterService : ICharacterService
    {
        private readonly CharacterDbContext _dbContext;
        private readonly IMapper _mapper;

        public CharacterService(CharacterDbContext dbContext , IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public CharacterDto GetById (int id)
        {
            var character = _dbContext
               .Characters
               .FirstOrDefault(r => r.Id == id);

            if (character is null) return null;
            

            var result = _mapper.Map<CharacterDto>(character);
            return result;
        }

        public IEnumerable<CharacterDto> GetAll() 
            {
                var characters = _dbContext
                    .Characters
                    .ToList();


                var charactersDtos = _mapper.Map<List<CharacterDto>>(characters);

                return charactersDtos;
            }

        public int Create(CreateCharacterDto dto)
        {
            var character = _mapper.Map<Character>(dto);
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();

            return character.Id;
        }

        public bool Delete(int id)
        {
            var character = _dbContext
              .Characters
              .FirstOrDefault(r => r.Id == id);

            if (character is null) return false;

            _dbContext.Characters.Remove(character);
            _dbContext.SaveChanges();


            return true;
        }
    }
}
