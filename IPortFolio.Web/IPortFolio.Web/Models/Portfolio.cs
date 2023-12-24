using System.ComponentModel.DataAnnotations;

namespace IPortFolio.Models
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public string Category { get; set; } = null!;

    }
}
