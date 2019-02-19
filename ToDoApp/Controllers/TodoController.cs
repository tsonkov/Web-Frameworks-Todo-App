using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Data.Entities;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoController(TodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> Get()
        {
            return await _repository.GetTodosAsync();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Todo>> Get(int id)
        {
            var todo = await _repository.GetTodoAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpPost]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Todo>> Post(TodoViewModel todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTodo = await _repository.AddTodoAsync(todo.Text, todo.IsComplete);

            return CreatedAtAction(nameof(Get), new { id = createdTodo.Id }, createdTodo);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Todo>> Put(int id, TodoViewModel todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _repository.UpdateTodoAsync(id, todo.Text, todo.IsComplete);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Todo>> Delete(int id)
        {
            var deleted = await _repository.DeleteTodo(id);
            if (!deleted)
            {
                return NotFound(id);
            }

            return Ok();
        }
    }
}
