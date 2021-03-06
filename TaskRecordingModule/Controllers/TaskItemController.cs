using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRecordingModule.Models;
using TaskRecordingModule.Services;


namespace TaskRecordingModule.Controllers
{
    
    public class TaskItemController : Controller
    {
        private readonly ITaskItemService _taskItemService;
        private readonly UserManager<ApplicationUser> _userManager;
        public TaskItemController(ITaskItemService taskItemService , UserManager<ApplicationUser> userManager)
        {
            _taskItemService = taskItemService;
            _userManager = userManager;
        } 
        public  async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            var items = await _taskItemService.GetIncompleteItemsAsync(currentUser);
            var model = new TaskItemViewModel()
            {
                Items = items
            };

            return View(model);
            
        }
        
        public async Task<IActionResult> AddItem(TaskItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var successful = await _taskItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int ID)
        {
            var task = _taskItemService.GetTaskItemById(ID);
            return View();
        }
        public IActionResult Edit(TaskItem task)
        {
            _taskItemService.UpdateTask(task);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(TaskItem task)
        {
            _taskItemService.DeleteTask(task);
            return RedirectToAction("Index");
        }
    }
}
