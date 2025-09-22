using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;
using TaskEntity = TaskAPI.Models.Task;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly AppContextDb _context;

        public TasksController(AppContextDb context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskEntity>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskEntity task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskEntity>> PostTask(TaskEntity task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        //Filtering
        // GET: api/Tasks/expired
        /// All tasks with a DueDate in the past 
        [HttpGet("expired")]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<TaskEntity>>> GetExpired()
        {
            var now = DateTime.UtcNow;
            return await _context.Tasks
                .Where(t => t.DueDate != null && t.DueDate < now)
                .ToListAsync();
        }

        // GET: api/Tasks/active
        /// All tasks not expired
        [HttpGet("active")]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<TaskEntity>>> GetActive()
        {
            var now = DateTime.UtcNow;
            return await _context.Tasks
                .Where(t => t.DueDate == null || t.DueDate >= now)
                .ToListAsync();
        }

        // GET: api/Tasks/from/2025/09/01
        /// All tasks with DueDate on or after the given date 
        [HttpGet("from/{yyyy:int:min(2000)}/{mm:int:range(1,12)}/{dd:int:range(1,31)}")]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<TaskEntity>>> GetFrom(int yyyy, int mm, int dd)
        {
            var date = new DateTime(yyyy, mm, dd, 0, 0, 0, DateTimeKind.Utc);
            return await _context.Tasks
                .Where(t => t.DueDate != null && t.DueDate >= date)
                .ToListAsync();
        }

        // GET: api/Tasks/by-user/5
        /// All tasks assigned to a specific user (by UserId)
        [HttpGet("by-user/{userId:int}")]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<TaskEntity>>> GetByUser(int userId)
        {
            return await _context.Tasks
                .Where(t => t.AssigneeUserId == userId)
                .ToListAsync();
        }

    }
}
