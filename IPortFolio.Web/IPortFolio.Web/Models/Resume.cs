using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IPortFolio.Models
{
    public class Resume
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
