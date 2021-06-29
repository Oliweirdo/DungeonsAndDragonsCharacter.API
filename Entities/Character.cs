using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Entities
{
    public class Character
    {
        public  int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public string Race { get; set; }
        public string Description { get; set; }
        public string Alignment { get; set; }
        public double ExperiencePoints { get; set; }



        public virtual CharacterProperty CharacterProperties { get; set; }
        public virtual CharacterBackground CharacterBackground { get; set; }
        public virtual CharacterSkills CharacterSkills { get; set; }
        public virtual CharacterEquipment CharacterEquipment { get; set; }

    }
}
