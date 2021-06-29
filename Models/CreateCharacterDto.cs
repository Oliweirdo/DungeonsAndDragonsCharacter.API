namespace DungeonsAndDragonsCharacter.API.Models
{
    public class CreateCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public string Race { get; set; }
        public string Description { get; set; }
        public string Alignment { get; set; }
        public double ExperiencePoints { get; set; }
    }
}