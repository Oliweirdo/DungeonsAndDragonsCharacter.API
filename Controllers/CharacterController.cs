using AutoMapper;
using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
using DungeonsAndDragonsCharacter.API.Services;
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
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CharacterDto>> GetAll()
        {
            var charactersDtos = _characterService.GetAll();

            return Ok(charactersDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<CharacterDto> Get([FromRoute] int id)
        {
            var characterDto = _characterService.GetById(id);

            return Ok(characterDto);

        }

        [HttpPost]
        public ActionResult CreateCharacter([FromBody] CreateCharacterDto dto)
        {
            var id = _characterService.Create(dto);

            return Created($"/api/character/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _characterService.Delete(id);

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
