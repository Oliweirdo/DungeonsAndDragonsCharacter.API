using DungeonsAndDragonsCharacter.API.Entities;
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
        public CharacterController(CharacterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Character>> GetAll()
        {
            var characters = _dbContext
                .Characters
                .ToList();

            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Character>> Get([FromRoute] int id)
        {
            var character = _dbContext
                .Characters
                .FirstOrDefault(r =>r.Id ==id);

            if(character is null)
            {
                return NotFound();
            }

            return Ok(character);

        }
    }
}
