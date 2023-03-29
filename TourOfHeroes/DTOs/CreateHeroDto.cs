using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TourOfHeroes.DTOs
{
    public class CreateHeroDto
    {
        [Required]
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public List<SkillInputDto> Skills { get; set; }
    }
}
