using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPortFolio.ViewModels
{
    public class SliderViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez")]
        [StringLength(100)]
        [ValidateNever]
        public string Image { get; set; } = null!;
        public string? Title { get; set; }
        public string? Content { get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile UploadImage { get; set; } = null!;
    }
}
