using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsUseRepoController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoItemsUseRepoController(TodoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/TodoItemsUseRepo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var result = await _repository.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return result.ToList();
        }

        // GET: api/TodoItemsUseRepo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _repository.GetOne(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItemsUseRepo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            try
            {
                var result = await _repository.Update(todoItem);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST: api/TodoItemsUseRepo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            try
            {
                var result = await _repository.Add(todoItem);
                return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/TodoItemsUseRepo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                var result = await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
