namespace TourOfHeroes.Models
{
    public class SkillModel : ModelBase
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }
        public List<HeroModel> Heroes { get; set; }
    }
}
