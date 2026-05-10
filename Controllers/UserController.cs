using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp_MVC_.Data;
using TaskManagementApp_MVC_.Models;
using TaskManagementApp_MVC_.Repositories;
namespace TaskManagementApp_MVC_.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext _context;
        public UserController(IUserRepository userRepository, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }
        public async Task<IActionResult> Dashboard()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if(userid == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var tasks =  await _context.TaskItems.Where(u=>u.UserId==userid).ToListAsync();
            return View("task");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userRepository.ExistingAccountAsync(model.Email, model.Password);
            if(user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Dashboard", "Task");
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }
    }
}
