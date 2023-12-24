using System.ComponentModel.DataAnnotations;

namespace IPortFolio.ViewModels
{
    public class ServiceViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
    }
}
