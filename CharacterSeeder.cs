using DungeonsAndDragonsCharacter.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API
{
    public class CharacterSeeder
    {
        private readonly CharacterDbContext _dbContext;

        public CharacterSeeder(CharacterDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Characters.Any())
                {
                    var characters = GetCharacters();
                    _dbContext.Characters.AddRange(characters);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Character> GetCharacters()
        {
            var characters = new List<Character>()
            {
                 new Character()
                 {
                    Name = "Gotrek",
                    Class = "Fighter 4",
                    Race = "Mountain Dwarf",
                    Level = 5,
                    Description = "Gotrek, son of Gurni, was born in 2370 IC and raised in the corridors of Karaz-a-Karak. Like all citizens of the King's Council he did his military service in the depths below the Everpeak as a youth, learning how to fight in tunnels. He has supposedly also spent time as a mercenary and an engineer, but details of his past are scarce. ",
                    Alignment = "",
                    ExperiencePoints = 666
                 }
            };

            return characters;
        }

    }
}
