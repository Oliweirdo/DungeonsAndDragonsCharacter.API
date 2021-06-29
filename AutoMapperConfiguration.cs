using AutoMapper;
using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API
{
    public class AutoMapperConfiguration : Profile 
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Character, CharacterDto>();
        }
    }
}
