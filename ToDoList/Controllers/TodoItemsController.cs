using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        public readonly TodoContext _todoContext;

        public TodoItemsController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _todoContext.TodoItems.ToListAsync();
        }

        //Get: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var TodoItem = await _todoContext.TodoItems.FindAsync(id);
            if (TodoItem == null)
            {
                return NotFound();
            }
            return TodoItem;
        }
        //POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (string.IsNullOrEmpty(todoItem.Description) || todoItem.Description.Length > 100)
            {
                return BadRequest("La descripcion es obligatoria y debe tner menos de 100 caracteres.");
            }

            _todoContext.TodoItems.Add(todoItem);
            await _todoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        //PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            _todoContext.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _todoContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodoItem(int id)
        {
            var todoItem = await _todoContext.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            _todoContext.TodoItems.Remove(todoItem);
            await _todoContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExist(int id)
        {
            return _todoContext.TodoItems.Any(e => e.Id == id);
        }
    }
}