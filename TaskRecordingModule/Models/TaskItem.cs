using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskRecordingModule.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        public bool IsDone { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTimeOffset? DueAt { get; set; }
        public string UserId { get; set; }
    }
    public class TaskItemViewModel
    {
        public TaskItem[] Items { get; set; }
    }
}
