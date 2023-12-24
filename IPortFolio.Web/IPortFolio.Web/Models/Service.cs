using System.ComponentModel.DataAnnotations;

namespace IPortFolio.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
