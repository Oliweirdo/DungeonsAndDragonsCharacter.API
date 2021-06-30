using DungeonsAndDragonsCharacter.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopWatch;
        private readonly ILogger<CharacterService> _logger;

        public RequestTimeMiddleware(ILogger<CharacterService> logger)
        {
            _stopWatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            var elapsedMiliseconds= _stopWatch.ElapsedMilliseconds; 
            if(elapsedMiliseconds / 1000 > 4)
            {
                var message = $"Request[{context.Request.Method}] at {context.Request.Path} took {elapsedMiliseconds}ms]";

                _logger.LogInformation(message);
            }
        }
    }
}
