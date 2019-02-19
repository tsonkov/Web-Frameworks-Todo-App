using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class TodoViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Text { get; set; }

        public bool IsComplete { get; set; }
    }
}
