using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPortFolio.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Twitter { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Skype { get; set; }
        public string? LinkedIn { get; set; }
        public string? Location { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        [ValidateNever]
        public string Image { get; set; } = null!;
        [NotMapped]
        [ValidateNever]
        public IFormFile UploadImage { get; set; } = null!;
        public string Job { get; set; } = null!;
    }
}
