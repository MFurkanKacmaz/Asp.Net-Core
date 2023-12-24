using AutoMapper;
using IPortFolio.Models;
using IPortFolio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPortFolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class ResumeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ResumeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [Route("/Admin/Resume")]
        [Route("/Admin/Resume/Index")]
        public ActionResult Index()
        {
            var userName = "";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(userName)))
            {
                return RedirectToAction("", "Login", new { area = "" });
            }
            var resumes = _context.Resumes.OrderBy(o => o.EndYear).ToList();
            var user = _context.Users.ToList();

            ViewBag.Resumes = resumes;
            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [Route("/Admin/Resume/Add")]
        public ActionResult Add()
        {
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Resume/Add")]
        public ActionResult Add(ResumeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resume = _mapper.Map<Resume>(model);
                    _context.Resumes.Add(resume);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(ResumeController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        [Route("/Admin/Resume/Edit/{Id}")]
        public ActionResult Edit(int id)
        {
            var resumes = _context.Resumes.ToList();
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            var resume = _context.Resumes.Find(id);

            return View(_mapper.Map<ResumeViewModel>(resume));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Resume/Edit/{Id}")]
        public ActionResult Edit(ResumeViewModel model)
        {
            try
            {
                var resume = _context.Resumes.FirstOrDefault(p => p.Id == model.Id);

                if (resume != null)
                {
                    resume.Department = model.Department;
                    resume.Content = model.Content;
                    resume.StartYear = model.StartYear;
                    resume.EndYear = model.EndYear;
                    resume.Organisation = model.Organisation;

                    _context.Resumes.Update(_mapper.Map<Resume>(resume));
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(ResumeController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        // GET: ResumeController/Delete/5
        public ActionResult Delete(int id)
        {
            var resume = _context.Resumes.Find(id);

            if (resume != null)
            {
                _context.Resumes.Remove(resume);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(ResumeController.Index));
        }

    }
}
