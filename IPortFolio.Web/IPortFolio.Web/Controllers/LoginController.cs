using AutoMapper;
using IPortFolio.Models;
using IPortFolio.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IPortFolio.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();

            var login = _context.Logins.ToList();
            ViewBag.Login = login.FirstOrDefault();

            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginViewModel)
        {

            HttpContext.Session.Clear();

            if (ModelState.IsValid)
            {
                var result = _context.Logins.FirstOrDefault(u => u.UserName == loginViewModel.UserName && u.Password == loginViewModel.Password);

                if (result != null)
                {
                    var userName = "";
                    var password = "";

                    HttpContext.Session.SetString(userName, loginViewModel.UserName);
                    HttpContext.Session.SetString(password, loginViewModel.Password);

                    return RedirectToAction("", "", new { area = "Admin" });
                }

                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre");
            }
            return View(loginViewModel);
        }
    }
}
