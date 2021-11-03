using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChoresWebAPI.Models;
using System.Data.SqlClient;


namespace ChoresWebAPI.Controllers
{
    [Route("api/TodoChores")]
    [ApiController]
    public class TodoChoresController : ControllerBase
    {
        private readonly ChoresContext _context;

        public TodoChoresController(ChoresContext context)
        {
            _context = context;
        }

        // GET: api/TodoChores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoChore>>> GetTodoChores()
        {
            return await _context.TodoChores.ToListAsync();
        }

        // GET: api/TodoChores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoChore>> GetTodoChore(int id)
        {
            var todoChore = await _context.TodoChores.FindAsync(id);

            if (todoChore == null)
            {
                return NotFound();
            }

            return todoChore;
        }

        // PUT: api/TodoChores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoChore(int id, TodoChore todoChore)
        {
            if (id != todoChore.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoChore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoChoreExists(id))
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

        // POST: api/TodoChores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoChore>> PostTodoChore(TodoChore todoChore)
        {
            _context.TodoChores.Add(todoChore);
            await _context.SaveChangesAsync();

            
            
            return CreatedAtAction(nameof(GetTodoChore), new { id = todoChore.Id }, todoChore);
        }

        // DELETE: api/TodoChores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoChore(int id)
        {
            var todoChore = await _context.TodoChores.FindAsync(id);
            if (todoChore == null)
            {
                return NotFound();
            }

            _context.TodoChores.Remove(todoChore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoChoreExists(int id)
        {
            return _context.TodoChores.Any(e => e.Id == id);
        }
    }
}
