using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPortFolio.ViewModels
{
    public class AboutViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string SubTitle { get; set; } = null!;
        public string SubContent { get; set; } = null!;
        public string? Image { get; set; }
        [NotMapped]
        [ValidateNever]
        public IFormFile UploadImage { get; set; } = null!;
    }
}
