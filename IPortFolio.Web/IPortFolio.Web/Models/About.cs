using System.ComponentModel.DataAnnotations;

namespace IPortFolio.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string SubContent { get; set; } = null!;
        public string? Image { get; set; }
    }
}
