using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApp_MVC_.Data;
using TaskManagementApp_MVC_.Models;
using TaskManagementApp_MVC_.Entities;
using TaskManagementApp_MVC_.Repositories;
using Microsoft.Extensions.Configuration.UserSecrets;
namespace TaskManagementApp_MVC_.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskrepository;
        public TaskController(ITaskRepository taskrepository)
        {
            _taskrepository = taskrepository;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateTask");
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Description,DueDate,Priority,Status")] TaskItemViewModel model)
        {  if (ModelState.IsValid)
            {
                int? userid = HttpContext.Session.GetInt32("UserId");
                if (userid == null) 
                {
                    return RedirectToAction("Login", "User");
                }
                var newtask = new TaskItem
                {   

                    Title = model.Title,
                    Description = model.Description,
                    Status = model.Status,
                    Priority = model.Priority,
                    DueDate = model.DueDate,
                    UserId= userid.Value,
                }; ;

                await _taskrepository.AddAsync(newtask);
                return RedirectToAction("Dashboard", "User");
                
            }
            ModelState.AddModelError("", "Invalid details");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                int? userid = HttpContext.Session.GetInt32("UserId");
                var task = await _taskrepository.GetByIdAsync(id, userid.Value);
                var model = new TaskItemViewModel();
                  model.Id = task.Id;
                  model.Description= task.Description;
                  model.DueDate = task.DueDate;
                  model.Status = task.Status;
                  model.Title = task.Title;
                  model.Priority = task.Priority;
                  

                return View(model);
            }
            catch (TaskNotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Status,Priority,DueDate")] TaskItemViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                int? userid = HttpContext.Session.GetInt32("UserId");
                var newtask = new TaskItem{
                 Id = model.Id,
                 Title = model.Title,
                 Description = model.Description,
                 Status=model.Status,
                 Priority=model.Priority,
                 DueDate=model.DueDate,
                 UserId = userid.Value,
                };
                await _taskrepository.UpdateAsync(newtask);
                return RedirectToAction("Dashboard", "User");
            }
            ModelState.AddModelError("", "Invalid Credential");
            return View(model);
        }

        public async Task<IActionResult> Delete(int id) 
        {
            try
            {
                int? userid = HttpContext.Session.GetInt32("UserId");
                await _taskrepository.DeleteAsync(id, userid.Value);
            }catch(TaskNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return RedirectToAction("Dashboard", "User");

        }

         
    }
}
