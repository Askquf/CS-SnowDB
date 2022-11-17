using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi3._0;

namespace WebApi3._0.Controllers
{
    [Route("api/streets")]
    [ApiController]
    public class StreetsController : ControllerBase
    {
        private readonly DBContext _context;

        public StreetsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Streets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Street>>> GetStreets()
        {
            return await _context.Streets.ToListAsync();
        }

        [HttpGet("worst")]
        public async Task<ActionResult<IEnumerable<Street>>> GetWorstStreets()
        {
            List<Street> top = new List<Street>();
            top.AddRange(await _context.Streets.OrderByDescending(u => u.Number_Of_Complaints).Take(2).ToListAsync());
            top.AddRange(await _context.Streets.OrderByDescending(u => u.Number_Of_Complaints).Where(u => !(u.Name.Contains("Озёрная") || (u.Name.Contains("Нагатинская")))).Take(8).ToListAsync());
            if (top == null)
            {
                return NotFound();
            }
            return top;
        }

        [HttpGet("best")]
        public async Task<ActionResult<IEnumerable<Street>>> GetBestStreets()
        {
            List<Street> top = new List<Street>();
            top.AddRange(await _context.Streets.Where(u => u.Number_Of_Complaints != 0).OrderBy(u => u.Number_Of_Complaints).Take(10).ToListAsync());
            if (top == null)
            {
                return NotFound();
            }
            return top;
        }

        // GET: api/Streets/5
         [HttpGet("{id}")]
         public async Task<ActionResult<Street>> GetStreet(int id)
         {
             var street = await _context.Streets.FindAsync(id);

             if (street == null)
             {
                 return NotFound();
             }

             return street;
         }

        // PUT: api/Streets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStreet(int id, Street street)
        {
            if (id != street.Street_ID)
            {
                return BadRequest();
            }

            _context.Entry(street).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StreetExists(id))
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

        // POST: api/Streets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Street>> PostStreet(Street street)
        {
            _context.Streets.Add(street);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStreet", new { id = street.Street_ID }, street);
        }

        // DELETE: api/Streets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStreet(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }

            _context.Streets.Remove(street);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StreetExists(int id)
        {
            return _context.Streets.Any(e => e.Street_ID == id);
        }
    }
}
