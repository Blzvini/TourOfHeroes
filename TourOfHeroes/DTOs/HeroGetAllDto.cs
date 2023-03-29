namespace TourOfHeroes.DTOs
{
    public class HeroGetAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<SkillGetByIdDto> Skills { get; set; }
    }
}
