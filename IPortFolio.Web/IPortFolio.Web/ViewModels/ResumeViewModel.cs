using System.ComponentModel.DataAnnotations;

namespace IPortFolio.ViewModels
{
    public class ResumeViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Department { get; set; } = null!;
        public string? Content { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime EndYear { get; set; }
        public string Organisation { get; set; } = null!;
    }
}
