using AutoMapper;
using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
using DungeonsAndDragonsCharacter.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Controllers
{
    [Route("api/character")]
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult<CharacterDto> Get([FromRoute] int id)
        {
            var characterDto = _characterService.GetById(id);

            return Ok(characterDto);

        }

        [HttpPost]
        public ActionResult CreateCharacter([FromBody] CreateCharacterDto dto)
        {
            var gamerId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _characterService.Create(dto, gamerId);

            return Created($"/api/character/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, GameMaster")]
        public ActionResult Delete([FromRoute] int id, ClaimsPrincipal gamer)
        {
            _characterService.Delete(id, gamer);
            return NotFound();
        }

        [HttpPatch("{id}")]
        public ActionResult Update([FromBody] UpdateCharacterDto dto, [FromRoute] int id, ClaimsPrincipal gamer)
        {
            _characterService.Update(id, dto, gamer);

            return Ok();
        }
    }
}
 