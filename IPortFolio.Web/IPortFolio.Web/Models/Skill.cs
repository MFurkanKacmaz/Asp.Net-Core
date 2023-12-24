using System.ComponentModel.DataAnnotations;

namespace IPortFolio.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Ratio { get; set; } = null!;
    }
}
