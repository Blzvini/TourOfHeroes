using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TourOfHeroes.Models
{
    public class HeroModel : ModelBase
    {
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public List<SkillModel> Skills { get; set; }

    }
}
