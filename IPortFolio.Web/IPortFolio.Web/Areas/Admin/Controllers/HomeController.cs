using AutoMapper;
using IPortFolio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IPortFolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [Route("/Admin")]
        [Route("/Admin/Home")]
        [Route("/Admin/Home/Index")]
        public IActionResult Index()
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("", "", new { area = "" });
        }
    }
}
