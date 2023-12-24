using System.ComponentModel.DataAnnotations;

namespace IPortFolio.ViewModels
{
    public class SkillViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Ratio { get; set; } = null!;
    }
}
