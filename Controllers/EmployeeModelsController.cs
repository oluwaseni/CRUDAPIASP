using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnMyOwn.Model;

namespace OnMyOwn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeModelsController : ControllerBase
    {
        private readonly EmployeeDbContext _context;

        public EmployeeModelsController(EmployeeDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeModels
        [HttpGet]
        public IEnumerable<EmployeeModel> GetEmployeeModels()
        {
            return _context.EmployeeModels;
        }

        // GET: api/EmployeeModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeModel = await _context.EmployeeModels.FindAsync(id);

            if (employeeModel == null)
            {
                return NotFound();
            }

            return Ok(employeeModel);
        }

        // PUT: api/EmployeeModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeModel([FromRoute] int id, [FromBody] EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeModel.CardId)
            {
                return BadRequest();
            }

            _context.Entry(employeeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(id))
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

        // POST: api/EmployeeModels
        [HttpPost]
        public async Task<IActionResult> PostEmployeeModel([FromBody] EmployeeModel employeeModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.EmployeeModels.Add(employeeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeModel", new { id = employeeModel.CardId }, employeeModel);
        }

        // DELETE: api/EmployeeModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employeeModel = await _context.EmployeeModels.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            _context.EmployeeModels.Remove(employeeModel);
            await _context.SaveChangesAsync();

            return Ok(employeeModel);
        }

        private bool EmployeeModelExists(int id)
        {
            return _context.EmployeeModels.Any(e => e.CardId == id);
        }
    }
}