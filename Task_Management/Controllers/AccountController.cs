using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Task_Management.Data;   // ✅ For ApplicationDbContext
using Task_Management.DataAccess;
using Task_Management.Domain.Interfaces;
using Task_Management.Service.Helpers;
using Task_Management.Models;


namespace Task_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _context;
        private readonly ITaskService _taskService;

        public AccountController(UserContext context,ITaskService taskService)
        {
            _context = context;
            _taskService = taskService; 
        }

        [HttpGet]
        public IActionResult Register() => View();

        //[HttpPost]
        //public IActionResult Register(User user)
        //{
        //    // ✅ Check if email already exists in DB
        //    var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

        //    if (existingUser != null)
        //    {
        //        ViewBag.Error = "Email already exists.";
        //        return View(user);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // ✅ Hash the password before saving
        //        user.Password = PasswordHasher.Hash(user.Password);

        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        return RedirectToAction("Login");
        //    }
        //    return View(user);
        //}
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _taskService.RegisterUserAsync(user);

                if (result.Success)
                {
                    return RedirectToAction("Login");
                }

                ViewBag.Error = result.Message;
                return View(user);
            }

            return View(user);
        }



        [HttpGet]
        public IActionResult Login() => View();



        //[HttpPost]
        //public async Task<IActionResult> LoginAsync(string email, string password)
        //{
        //    string hashedPassword = PasswordHasher.Hash(password);

        //    var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        //    if (user != null)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name , user.Username),
        //            new Claim(ClaimTypes.Email , user.Email)
        //        };

        //        var identity = new ClaimsIdentity(claims, "CustomAuth");
        //        var principal = new ClaimsPrincipal(identity);

        //        await HttpContext.SignInAsync(principal);

        //        TempData["Username"] = user.Username; // Optional: still storing username
        //        return RedirectToAction("Index", "Home");
        //    }

        //    ViewBag.Error = "Invalid email or password";
        //    return View(user);
        //}

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            var user = await _taskService.AuthenticateUserAsync(email, password);

            if (user != null)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email)
        };

                var identity = new ClaimsIdentity(claims, "CustomAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                TempData["Username"] = user.Username;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CustomAuth");
            return RedirectToAction("Login", "Account");
        }

    }
}
