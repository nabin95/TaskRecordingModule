using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRecordingModule.Data;
using TaskRecordingModule.Models;

namespace TaskRecordingModule.Services
{
    public interface ITaskItemService
    {
        Task<TaskItem[]> GetIncompleteItemsAsync(ApplicationUser user);
        Task<bool> AddItemAsync(TaskItem newItem);
        TaskItem GetTaskItemById(int ID);
        void UpdateTask(TaskItem task);
        void DeleteTask(TaskItem task);
    }
    public class TaskItemService : ITaskItemService
    {
        private readonly ApplicationDbContext _context;

        public TaskItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem[]> GetIncompleteItemsAsync(ApplicationUser user)
        {
            var items = await _context.Items
                .Where(x => x.IsDone == false)
                .ToArrayAsync();
            return items;
        }
        public async Task<bool> AddItemAsync(TaskItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);

            _context.Items.Add(newItem);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
        
        public void UpdateTask(TaskItem task)
        {
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void DeleteTask(TaskItem task)
        {
            _context.Entry(task).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public TaskItem GetTaskItemById(int ID)
        {
            return _context.Items.Find(ID);
        }
    }
}
