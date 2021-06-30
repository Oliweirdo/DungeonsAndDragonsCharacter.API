using DungeonsAndDragonsCharacter.API.Models;
using DungeonsAndDragonsCharacter.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;

        }

        [HttpPost("register")]
        public ActionResult RegisterGamer([FromBody]RegisterGamerDto dto )
        {
            _accountService.RegisterGamer(dto);
            return Ok();
        }
    }
}
