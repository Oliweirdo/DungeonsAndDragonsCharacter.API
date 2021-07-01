using DungeonsAndDragonsCharacter.API.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Authorization
{
    public class ResourceOperationRequirementHandler :AuthorizationHandler<ResourceOperationRequirement, Character>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Character character)
        {
            if(requirement.ResourceOperation == ResourceOperation.Read ||
               requirement.ResourceOperation == ResourceOperation.Create)
            {
                context.Succeed(requirement);
            }

            var gamerId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (character.CreatedById == int.Parse(gamerId))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
