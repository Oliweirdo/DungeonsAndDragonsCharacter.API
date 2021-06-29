using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Entities
{
    public class CharacterProperty
    {
        public int Id { get; set; }
        public int Strenght { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Inteligence { get; set; }
        public int Windsom { get; set; }
        public int Charisma { get; set; }
        public int ArmorClass { get; set; }
        public int Initiatve { get; set; }
        public int Speed { get; set; }
        public int CurrentHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }

        public int Success { get; set; }
        public int Failures { get; set; }
        public string Race { get; set; }

        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }


    }
}
