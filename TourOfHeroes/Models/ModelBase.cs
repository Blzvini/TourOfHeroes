using System.ComponentModel.DataAnnotations;

namespace TourOfHeroes.Models
{
    public class ModelBase
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        [MaxLength(100)]
        public string? CreatedBy { get; set; }
        [MaxLength(100)]
        public string? UpdatedBy { get; set; }
        [MaxLength(100)]
        public string? DeletedBy { get; set; }
    }
}
