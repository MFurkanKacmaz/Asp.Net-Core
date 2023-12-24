using System.ComponentModel.DataAnnotations;

namespace IPortFolio.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
