
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Data.Entities
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsComplete { get; set; }
    }
}
