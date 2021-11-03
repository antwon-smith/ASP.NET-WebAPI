using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChoresWebAPI.Models;

namespace ChoresWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoAssignmentsController : ControllerBase
    {
        private readonly ChoresContext _context;

        public TodoAssignmentsController(ChoresContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
            // _context.Database.ExecuteSqlRaw("USE TodoChores; IF NOT EXISTS (  SELECT * FROM sys.tables WHERE name = 'TodoAssignments' ) CREATE TABLE TodoAssignments (ID int primary key IDENTITY(1, 1) NOT NULL, FirstName nvarchar(max) NOT NULL, LastName nvarchar(max)) ;");
        }

        // GET: api/TodoAssignments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoAssignment>>> GetTodoAssignment()
        {
            return await _context.TodoAssignment.ToListAsync();
        }

        // GET: api/TodoAssignments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoAssignment>> GetTodoAssignment(string id)
        {
            var todoAssignment = await _context.TodoAssignment.FindAsync(id);

            if (todoAssignment == null)
            {
                return NotFound();
            }

            return todoAssignment;
        }

        // PUT: api/TodoAssignments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoAssignment(string id, TodoAssignment todoAssignment)
        {
            if (id != todoAssignment.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoAssignmentExists(id))
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

        // POST: api/TodoAssignments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoAssignment>> PostTodoAssignment(TodoAssignment todoAssignment)
        {
            _context.TodoAssignment.Add(todoAssignment);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TodoAssignmentExists(todoAssignment.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTodoAssignment", new { id = todoAssignment.Id }, todoAssignment);
        }

        // DELETE: api/TodoAssignments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoAssignment(string id)
        {
            var todoAssignment = await _context.TodoAssignment.FindAsync(id);
            if (todoAssignment == null)
            {
                return NotFound();
            }

            _context.TodoAssignment.Remove(todoAssignment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoAssignmentExists(string id)
        {
            return _context.TodoAssignment.Any(e => e.Id == id);
        }
    }
}
