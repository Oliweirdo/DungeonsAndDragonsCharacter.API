using DungeonsAndDragonsCharacter.API.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Services
{
    public interface IGamerContextService
    {
        ClaimsPrincipal Gamer { get; }
        int? GetGamerId { get; }

    }

    public class GamerContextService : IGamerContextService
    {
        private readonly IHttpContextAccessor _httpContextAccesor;

        public GamerContextService(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor = httpContextAccesor;
        }

        public ClaimsPrincipal Gamer => _httpContextAccesor.HttpContext?.User;
        public int? GetGamerId =>
            Gamer is null ? null : (int?)int.Parse(Gamer.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}
