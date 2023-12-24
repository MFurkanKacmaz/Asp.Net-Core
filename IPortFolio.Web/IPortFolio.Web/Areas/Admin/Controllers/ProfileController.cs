using AutoMapper;
using Azure.Core;
using IPortFolio.Models;
using IPortFolio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace IPortFolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ProfileController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        [Route("/Admin/Profile")]
        [Route("/Admin/Profile/Index")]
        public ActionResult Index()
        {
            var userName = "";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(userName)))
            {
                return RedirectToAction("", "Login", new { area = "" });
            }
            var sliders = _context.Sliders.ToList();
            var abouts = _context.Abouts.ToList();
            var skills = _context.Skills.ToList();
            var resumes = _context.Resumes.OrderBy(o => o.EndYear).ToList();
            var portfolios = _context.Portfolios.ToList();
            var services = _context.Services.ToList();
            var user = _context.Users.ToList();

            ViewBag.Slider = sliders.FirstOrDefault();

            ViewBag.About = abouts.FirstOrDefault();

            ViewBag.Skills = skills;

            ViewBag.Resumes = resumes;

            ViewBag.Portfolios = portfolios;

            ViewBag.Services = services;

            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [Route("/Admin/Profile/Edit/{Id}")]
        public ActionResult Edit(int id)
        {
            var sliders = _context.Sliders.ToList();
            var abouts = _context.Abouts.ToList();
            var skills = _context.Skills.ToList();
            var resumes = _context.Resumes.OrderBy(o => o.EndYear).ToList();
            var portfolios = _context.Portfolios.ToList();
            var services = _context.Services.ToList();
            var user = _context.Users.ToList();

            ViewBag.Slider = sliders.FirstOrDefault();

            ViewBag.About = abouts.FirstOrDefault();

            ViewBag.Skills = skills;

            ViewBag.Resumes = resumes;

            ViewBag.Portfolios = portfolios;

            ViewBag.Services = services;

            ViewBag.User = user.FirstOrDefault();

            var profilUser = _context.Users.Find(id);

            return View(_mapper.Map<ProfileViewModel>(profilUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Profile/Edit/{Id}")]
        public ActionResult Edit(ProfileViewModel model)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(p => p.Id == model.Id);

                if (user != null)
                {
                    user.FullName = model.FullName;
                    user.Twitter = model.Twitter;
                    user.Facebook = model.Facebook;
                    user.Instagram = model.Instagram;
                    user.LinkedIn = model.LinkedIn;
                    user.Location = model.Location;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Image = model.Image;
                    user.Job = model.Job;

                    if (model.UploadImage != null && model.UploadImage.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(model.UploadImage.FileName);

                        var path = Path.Combine(images.PhysicalPath!, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);
                        model.UploadImage.CopyTo(stream);

                        user!.Image = randomImageName;
                    }
                }

                _context.Users.Update(_mapper.Map<User>(user));
                _context.SaveChanges();

                return RedirectToAction(nameof(ProfileController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

    }
}
