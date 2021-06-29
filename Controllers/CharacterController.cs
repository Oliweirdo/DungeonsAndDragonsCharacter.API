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
    }
}
