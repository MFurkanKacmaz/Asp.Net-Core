using Microsoft.EntityFrameworkCore;
using IPortFolio.ViewModels;
using AspNetCoreIdentityApp.Web.ViewModels;

namespace IPortFolio.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Login> Logins { get; set; }

        //public DbSet<IPortFolio.ViewModels.ProfileViewModel> ProfileViewModel { get; set; } = default!;
        //public DbSet<IPortFolio.ViewModels.SliderViewModel> SliderViewModel { get; set; } = default!;
        //public DbSet<IPortFolio.ViewModels.AboutViewModel> AboutViewModel { get; set; } = default!;
        //public DbSet<IPortFolio.ViewModels.SkillViewModel> SkillViewModel { get; set; } = default!;
        //public DbSet<IPortFolio.ViewModels.ResumeViewModel> ResumeViewModel { get; set; } = default!;
        //public DbSet<IPortFolio.ViewModels.PortfolioViewModel> PortfolioViewModel { get; set; } = default!;
        //public DbSet<IPortFolio.ViewModels.ServiceViewModel> ServiceViewModel { get; set; } = default!;
        //public DbSet<AspNetCoreIdentityApp.Web.ViewModels.SendMailViewModel> SendMailViewModel { get; set; } = default!;

    }
}
