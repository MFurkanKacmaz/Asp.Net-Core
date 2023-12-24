using AutoMapper;
using IPortFolio.Models;
using IPortFolio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPortFolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class ServiceController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServiceController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [Route("/Admin/Service")]
        [Route("/Admin/Service/Index")]
        public ActionResult Index()
        {
            var userName = "";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(userName)))
            {
                return RedirectToAction("", "Login", new { area = "" });
            }
            var services = _context.Services.ToList();
            var user = _context.Users.ToList();

            ViewBag.Services = services;
            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [Route("/Admin/Service/Add")]
        public ActionResult Add()
        {
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Service/Add")]
        public ActionResult Add(ServiceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var service = _mapper.Map<Service>(model);
                    _context.Services.Add(service);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(ServiceController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        [Route("/Admin/Service/Edit/{Id}")]
        public ActionResult Edit(int id)
        {
            var services = _context.Services.ToList();
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            var service = _context.Services.Find(id);

            return View(_mapper.Map<ServiceViewModel>(service));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Service/Edit/{Id}")]
        public ActionResult Edit(ServiceViewModel model)
        {
            try
            {
                var service = _context.Services.FirstOrDefault(p => p.Id == model.Id);

                if (service != null)
                {
                    service.Title = model.Title;
                    service.Description = model.Description;

                    _context.Services.Update(_mapper.Map<Service>(service));
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(ServiceController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        // GET: ServiceController/Delete/5
        public ActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);

            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(ServiceController.Index));
        }

    }
}
