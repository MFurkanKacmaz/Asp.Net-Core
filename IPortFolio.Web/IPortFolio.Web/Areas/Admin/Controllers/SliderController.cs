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
    public class SliderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        public SliderController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        [Route("/Admin/Slider")]
        [Route("/Admin/Slider/Index")]
        public IActionResult Index()
        {
            var userName = "";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(userName)))
            {
                return RedirectToAction("", "Login", new { area = "" });
            }
            var sliders = _context.Sliders.ToList();
            var user = _context.Users.ToList();

            ViewBag.Slider = sliders.FirstOrDefault();
            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [Route("/Admin/Slider/Edit/{Id}")]
        public ActionResult Edit(int id)
        {
            var sliders = _context.Sliders.ToList();
            var user = _context.Users.ToList();

            ViewBag.Slider = sliders.FirstOrDefault();
            ViewBag.User = user.FirstOrDefault();

            var slider = _context.Sliders.Find(id);

            return View(_mapper.Map<SliderViewModel>(slider));
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Slider/Edit/{Id}")]
        public ActionResult Edit(SliderViewModel model)
        {
            try
            {
                var slider = _context.Sliders.FirstOrDefault(p => p.Id == model.Id);

                if (slider != null)
                {
                    slider.Image = model.Image;
                    slider.Title = model.Title;
                    slider.Content = model.Content;

                    if (model.UploadImage != null && model.UploadImage.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(model.UploadImage.FileName);

                        var path = Path.Combine(images.PhysicalPath!, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);
                        model.UploadImage.CopyTo(stream);

                        slider!.Image = randomImageName;
                    }
                }

                _context.Sliders.Update(_mapper.Map<Slider>(slider));
                _context.SaveChanges();

                return RedirectToAction(nameof(SliderController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

    }
}
