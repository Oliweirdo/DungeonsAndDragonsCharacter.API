using AutoMapper;
using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Controllers
{
    [Route("api/character")]
    public class CharacterController : ControllerBase
    {
        private readonly CharacterDbContext _dbContext;
        private readonly IMapper _mapper;

        public CharacterController(CharacterDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CharacterDto>> GetAll()
        {
            var characters = _dbContext
                .Characters
                .ToList();


            var charactersDtos = _mapper.Map<List<CharacterDto>>(characters);

            return Ok(charactersDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CharacterDto> Get([FromRoute] int id)
        {
            var character = _dbContext
                .Characters
                .FirstOrDefault(r =>r.Id ==id);

            if(character is null)
            {
                return NotFound();
            }

            var charactersDtos = _mapper.Map<CharacterDto>(character);

            return Ok(charactersDtos);

        }

        [HttpPost]
        public ActionResult CreateCharacter([FromBody] CreateCharacterDto dto )
        {
            var character = _mapper.Map<Character>(dto);
            _dbContext.Characters.Add(character);
            _dbContext.SaveChanges();

            return Created($"/api/character/[character.Id]", null);
        }
    }
}
