using AspNetCoreIdentityApp.Web.Services;
using AspNetCoreIdentityApp.Web.ViewModels;
using AutoMapper;
using IPortFolio.Areas.Admin.Controllers;
using IPortFolio.Models;
using IPortFolio.ViewModels;
using IPortFolio.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace IPortFolio.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;


        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper, IEmailService emailService)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var sliders = _context.Sliders.ToList();
            var abouts = _context.Abouts.ToList();
            var skills = _context.Skills.ToList();
            var resumes = _context.Resumes.OrderBy(o => o.EndYear).ToList();
            var portfolios = _context.Portfolios.ToList();
            var services = _context.Services.ToList();
            var user = _context.Users.ToList();
            
            ViewBag.Slider=sliders.FirstOrDefault();

            ViewBag.About=abouts.FirstOrDefault();

            ViewBag.Skills = skills;

            ViewBag.Resumes = resumes;

            ViewBag.Portfolios = portfolios;

            ViewBag.Services = services;

            ViewBag.User = user.FirstOrDefault();

            return View();
        }

        [HttpPost]
        public IActionResult Index(SendMailViewModel mail)
        {
            _emailService.SendEmail(mail.Name!, mail.Subject!, mail.Message, mail.Email);

            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}