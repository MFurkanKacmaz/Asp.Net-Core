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
    public class PortfolioController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        public PortfolioController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        [Route("/Admin/Portfolio")]
        [Route("/Admin/Portfolio/Index")]
        public ActionResult Index()
        {
            var userName = "";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(userName)))
            {
                return RedirectToAction("", "Login", new { area = "" });
            }
            var portfolios = _context.Portfolios.ToList();
            var user = _context.Users.ToList();

            ViewBag.Portfolios = portfolios;
            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [Route("/Admin/Portfolio/Add")]
        public ActionResult Add()
        {
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Portfolio/Add")]
        public ActionResult Add(PortfolioViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var portfolio = _mapper.Map<Portfolio>(model);
                    if (model.UploadImage != null && model.UploadImage.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(model.UploadImage.FileName);

                        var path = Path.Combine(images.PhysicalPath!, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);
                        model.UploadImage.CopyTo(stream);

                        portfolio.Image = randomImageName;
                    }

                    _context.Portfolios.Add(portfolio);
                    _context.SaveChanges();

                }

                return RedirectToAction(nameof(PortfolioController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        [Route("/Admin/Portfolio/Edit/{Id}")]
        public ActionResult Edit(int id)
        {
            var portfolios = _context.Portfolios.ToList();
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            var portfolio = _context.Portfolios.Find(id);

            return View(_mapper.Map<PortfolioViewModel>(portfolio));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Portfolio/Edit/{Id}")]
        public ActionResult Edit(PortfolioViewModel model)
        {
            try
            {
                var portfolio = _context.Portfolios.FirstOrDefault(p => p.Id == model.Id);

                if (portfolio != null)
                {
                    portfolio.Category = model.Category;

                    if (model.UploadImage != null && model.UploadImage.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");
                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(model.UploadImage.FileName);

                        var path = Path.Combine(images.PhysicalPath!, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);
                        model.UploadImage.CopyTo(stream);

                        portfolio.Image = randomImageName;
                    }

                    _context.Portfolios.Update(_mapper.Map<Portfolio>(portfolio));
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(ResumeController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        public ActionResult Delete(int id)
        {
            var portfolio = _context.Portfolios.Find(id);

            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(PortfolioController.Index));
        }

    }
}
