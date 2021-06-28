using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DungeonsAndDragonsCharacter.API.Entities
{
    public class CharacterBackground
    {
        public int Id { get; set; }
        public string PersonalityTraits { get; set; }
        public string Ideals { get; set; }
        public string Bonds { get; set; }
        public string Flaws { get; set; }

        public string FeaturesAndTraits { get; set; }

        public int CharacterId { get; set; }
        public virtual Character Character { get; set; }

    }
}
