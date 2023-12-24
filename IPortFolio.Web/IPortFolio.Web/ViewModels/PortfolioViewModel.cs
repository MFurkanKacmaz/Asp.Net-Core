using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPortFolio.ViewModels
{
    public class PortfolioViewModel
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string Image { get; set; } = null!;
        public string Category { get; set; } = null!;
        [NotMapped]
        [ValidateNever]
        public IFormFile UploadImage { get; set; } = null!;
    }
}
