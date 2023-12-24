using AutoMapper;
using IPortFolio.Models;
using IPortFolio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace IPortFolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class AboutController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        public AboutController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        [Route("/Admin/About")]
        [Route("/Admin/About/Index")]
        public ActionResult Index()
        {
            var userName = "";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(userName)))
            {
                return RedirectToAction("", "Login", new { area = "" });
            }
            var abouts = _context.Abouts.ToList();;
            var user = _context.Users.ToList();

            ViewBag.About = abouts.FirstOrDefault();
            ViewBag.User = user.FirstOrDefault();

            return View();
        }


        [Route("/Admin/About/Edit/{Id}")]
        public ActionResult Edit(int id)
        {
            var abouts = _context.Abouts.ToList();
            var user = _context.Users.ToList();

            ViewBag.About = abouts.FirstOrDefault();
            ViewBag.User = user.FirstOrDefault();

            var about = _context.Abouts.Find(id);

            return View(_mapper.Map<AboutViewModel>(about));
        }

        // POST: AboutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/About/Edit/{Id}")]
        public ActionResult Edit(AboutViewModel model)
        {
            try
            {
                var about = _context.Abouts.FirstOrDefault(p => p.Id == model.Id);

                if (about != null)
                {
                    about.Image = model.Image;
                    about.Title = model.Title;
                    about.Content = model.Content;
                    about.SubTitle = model.SubTitle;
                    about.SubContent = model.SubContent;

                    if (model.UploadImage != null && model.UploadImage.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(model.UploadImage.FileName);

                        var path = Path.Combine(images.PhysicalPath!, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);
                        model.UploadImage.CopyTo(stream);

                        about!.Image = randomImageName;
                    }
                }

                    _context.Abouts.Update(_mapper.Map<About>(about));
                    _context.SaveChanges();
                return RedirectToAction(nameof(AboutController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }
    }
}
