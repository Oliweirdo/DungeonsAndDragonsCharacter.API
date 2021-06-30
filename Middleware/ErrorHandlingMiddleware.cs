using DungeonsAndDragonsCharacter.API.Exceptions;
using DungeonsAndDragonsCharacter.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<CharacterService> _logger;

        public ErrorHandlingMiddleware(ILogger<CharacterService> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadHttpRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch(NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch(Exception e)
            {
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Oh no, something went wrong...");

            }
        }
    }
}
