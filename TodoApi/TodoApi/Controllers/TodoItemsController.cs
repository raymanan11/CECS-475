using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/TodoItems")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly AnTodoContext _context;

        public TodoItemsController(AnTodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnTodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnTodoItem>> GetAnTodoItem(long id)
        {
            var anTodoItem = await _context.TodoItems.FindAsync(id);

            if (anTodoItem == null)
            {
                return NotFound();
            }

            return anTodoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnTodoItem(long id, AnTodoItem anTodoItem)
        {
            if (id != anTodoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(anTodoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnTodoItemExists(id))
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

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<AnTodoItem>> PostTodoItem(AnTodoItem todoItem) {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetAnTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnTodoItem(long id)
        {
            var anTodoItem = await _context.TodoItems.FindAsync(id);
            if (anTodoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(anTodoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnTodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
