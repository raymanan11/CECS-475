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
        public async Task<ActionResult<IEnumerable<AnTodoItemDTO>>> GetTodoItems() {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnTodoItemDTO>> GetTodoItem(long id) {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null) {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, AnTodoItemDTO todoItemDTO) {
            if (id != todoItemDTO.Id) {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id)) {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<AnTodoItemDTO>> CreateTodoItem(AnTodoItemDTO todoItemDTO) {
            var todoItem = new AnTodoItem {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null) {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

        private static AnTodoItemDTO ItemToDTO(AnTodoItem todoItem) =>
            new AnTodoItemDTO {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
