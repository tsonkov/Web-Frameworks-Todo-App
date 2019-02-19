using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Data.Entities;

namespace ToDoApp.Services
{
    public class TodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> GetTodoAsync(int id)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo> AddTodoAsync(string text, bool isComplete)
        {
            var todo = new Todo
            {
                Text = text,
                IsComplete = isComplete
            };
            await _context.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<Todo> UpdateTodoAsync(int id, string text, bool isComplete)
        {
            var todo = await GetTodoAsync(id);
            if (todo != null)
            {
                todo.Text = text;
                todo.IsComplete = isComplete;

                await _context.SaveChangesAsync();
            }

            return todo;
        }

        public async Task<bool> DeleteTodo(int id)
        {
            var todo = await GetTodoAsync(id);
            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
