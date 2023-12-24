using AutoMapper;
using Azure.Core;
using IPortFolio.Models;
using IPortFolio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPortFolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[controller]/[action]")]
    public class SkillController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SkillController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [Route("/Admin/Skill")]
        [Route("/Admin/Skill/Index")]
        public ActionResult Index()
        {
            var userName = "";

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(userName)))
            {
                return RedirectToAction("", "Login", new { area = "" });
            }
            var skills = _context.Skills.ToList();
            var user = _context.Users.ToList();

            ViewBag.Skills = skills.ToList();
            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [Route("/Admin/Skill/Add")]
        public ActionResult Add()
        {
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Skill/Add")]
        public ActionResult Add(SkillViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var skill = _mapper.Map<Skill>(model);
                    _context.Skills.Add(skill);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(SkillController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        [Route("/Admin/Skill/Edit/{Id}")]
        public ActionResult Edit(int id)
        {
            var skills = _context.Skills.ToList();
            var user = _context.Users.ToList();

            ViewBag.User = user.FirstOrDefault();

            var skill = _context.Skills.Find(id);

            return View(_mapper.Map<SkillViewModel>(skill));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Admin/Skill/Edit/{Id}")]
        public ActionResult Edit(SkillViewModel model)
        {
            try
            {
                var skill = _context.Skills.FirstOrDefault(p => p.Id == model.Id);

                if (skill != null)
                {
                    skill.Name = model.Name;
                    skill.Ratio = model.Ratio;

                    _context.Skills.Update(_mapper.Map<Skill>(skill));
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(SkillController.Index));
            }
            catch
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
        }

        public ActionResult Delete(int id)
        {
            var skill = _context.Skills.Find(id);

            if (skill != null)
            {
                _context.Skills.Remove(skill);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(SkillController.Index));
        }

    }
}
