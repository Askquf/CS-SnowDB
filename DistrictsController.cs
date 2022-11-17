using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi3._0.Controllers
{
    [Route("api/districts")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly DBContext _context;

        public DistrictsController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Districts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<District>>> GetDisctricts()
        {
            return await _context.Districts.ToListAsync();
        }

        // GET: api/Streets/top-10
        [HttpGet("worst")]
        public async Task<ActionResult<IEnumerable<District>>> GetWorstDistricts()
        {
            List<District> top = new List<District>();
            top.AddRange(await _context.Districts.Where(u => u.District_ID % 100 != 0).OrderByDescending(u => u.Number_Of_Complaints).Take(10).ToListAsync());
            if (top == null)
            {
                return NotFound();
            }
            return top;
        }

        [HttpGet("best")]
        public async Task<ActionResult<IEnumerable<District>>> GetBestDistricts()
        {
            List<District> top = new List<District>();

            top.AddRange(await _context.Districts.Where(u => u.District_ID % 100 != 0).Where(u => u.Number_Of_Complaints != 0).OrderBy(u => u.Number_Of_Complaints).Take(10).ToListAsync());
            if (top == null)
            {
                return NotFound();
            }
            return top;
        }

        [HttpGet("admdistr")]
        public async Task<ActionResult<IEnumerable<District>>> GetAdmDistricts()
        {
            List<District> top = new List<District>();

            top.AddRange(await _context.Districts.Where(u => u.District_ID % 100 == 0).ToListAsync());
            if (top == null)
            {
                return NotFound();
            }
            return top;
        }

        // GET: api/Districts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<District>> GetDistrict(int id)
        {
            var district = await _context.Districts.FindAsync(id);

            if (district == null)
            {
                return NotFound();
            }

            return district;
        }

        // PUT: api/Districts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistrict(int id, District district)
        {
            if (id != district.District_ID)
            {
                return BadRequest();
            }

            _context.Entry(district).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictExists(id))
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

        // POST: api/Districts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<District>> PostDistrict(District district)
        {
            _context.Districts.Add(district);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistrict", new { id = district.District_ID }, district);
        }

        // DELETE: api/Districts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }

            _context.Districts.Remove(district);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DistrictExists(int id)
        {
            return _context.Districts.Any(e => e.District_ID == id);
        }
    }
}
