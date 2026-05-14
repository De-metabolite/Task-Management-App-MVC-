using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp_MVC_.Data;
using TaskManagementApp_MVC_.Models;
using TaskManagementApp_MVC_.Entities;
using TaskManagementApp_MVC_.Repositories;
using System.ComponentModel.DataAnnotations;
namespace TaskManagementApp_MVC_.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskrepository;
        public UserController(IUserRepository userRepository, ITaskRepository taskrepository)
        {
            _userRepository = userRepository;
            _taskrepository = taskrepository;
        }
        public async Task<IActionResult> Dashboard()
        {
            int? userid = HttpContext.Session.GetInt32("UserId");
            if(userid == null)
            {
                return RedirectToAction(nameof(Login));
            }
            var tasks =  await _taskrepository.GetAllAsync(userid.Value);
            return View(tasks);
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
                return RedirectToAction(nameof(Dashboard));
            }
            ModelState.AddModelError("", "Invalid email or password");
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("RegisterUser");
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,Password,Username")] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Credentials");
                return View(model);
            }
            var newuser = new User
            {
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,

            };

            await _userRepository.CreateUserAsync(newuser);
            return RedirectToAction(nameof(Login));
        }
    }
   
}
