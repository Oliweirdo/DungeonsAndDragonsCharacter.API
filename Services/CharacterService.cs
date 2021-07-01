using AutoMapper;
using DungeonsAndDragonsCharacter.API.Authorization;
using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Exceptions;
using DungeonsAndDragonsCharacter.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Services
{
    public interface ICharacterService
    {
        CharacterDto GetById(int id);
        IEnumerable<CharacterDto> GetAll();
        int Create(CreateCharacterDto dto);
        void Delete(int id);
       void Update(int id, UpdateCharacterDto dto);
    }


    public class CharacterService : ICharacterService
    {
        private readonly CharacterDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CharacterService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IGamerContextService _gamerContextService;


        public CharacterService(CharacterDbContext dbContext , IMapper mapper, ILogger<CharacterService> logger  ,
            IAuthorizationService authorizationService, IGamerContextService gamerContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;

        }

        public CharacterDto GetById (int id)
        {
            var character = _dbContext
               .Characters
               .FirstOrDefault(r => r.Id == id);

            if (character is null)
                throw new NotFoundException("Character not found");

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
            character.CreatedById = _gamerContextService.GetGamerId;
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();

            return character.Id;
        }

        public void Delete(int id)
        {
            _logger.LogWarning($"Character with id: {id} DELETE action invoked");

            var character = _dbContext
              .Characters
              .FirstOrDefault(r => r.Id == id);

            if (character is null)
                throw new NotFoundException("Character not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_gamerContextService.Gamer, character,
              new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Characters.Remove(character);
            _dbContext.SaveChanges();

        }

        public void Update(int id, UpdateCharacterDto dto)
        {
            var character = _dbContext
              .Characters
              .FirstOrDefault(r => r.Id == id);

            if (character is null)
                throw new NotFoundException("Character not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(_gamerContextService.Gamer, character,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Characters.Update(character);
            _dbContext.SaveChanges();
        }
    }
}
