using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Task_Management.Data;   // ✅ For ApplicationDbContext
using Task_Management.Helpers;
using Task_Management.Models; // ✅ Adjust to your namespace


namespace Task_Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserContext _context;

        public AccountController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(User user)
        {
            // ✅ Check if email already exists in DB
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

            if (existingUser != null)
            {
                ViewBag.Error = "Email already exists.";
                return View(user);
            }

            if (ModelState.IsValid)
            {
                // ✅ Hash the password before saving
                user.Password = PasswordHasher.Hash(user.Password);

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login() => View();



        [HttpPost]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            string hashedPassword = PasswordHasher.Hash(password);

            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , user.Username),
                    new Claim(ClaimTypes.Email , user.Email)
                };

                var identity = new ClaimsIdentity(claims, "CustomAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                TempData["Username"] = user.Username; // Optional: still storing username
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid email or password";
            return View(user);
        }

       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CustomAuth");
            return RedirectToAction("Login", "Account");
        }

    }
}
