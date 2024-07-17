using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelefonApi.Models;

namespace TelefonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonController : ControllerBase
    {
        private readonly DbTelefonContext _context;

        public TelefonController(DbTelefonContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonTbl>>> GetTelefonListe()
        {
            var DbTelefon = await (from k in _context.TelefonTbls
                                   select k).ToListAsync();

            return Ok(DbTelefon);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TelefonTbl>> GetTelefonById(int id)
        {
            var telefon = await _context.TelefonTbls.FindAsync(id);

            if (telefon == null)
            {
                return NotFound();
            }

            return telefon;
        }

        [HttpPost]
        public async Task<ActionResult<TelefonTbl>> CreateTelefon(TelefonTbl telefon)
        {
            _context.TelefonTbls.Add(telefon);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTelefonById), new { id = telefon.Id }, telefon);
        }


        private bool TelefonExists(int id)
        {
            return _context.TelefonTbls.Any(e => e.Id == id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTelefon(int id, TelefonTbl telefon)
        {
            if (id != telefon.Id)
            {
                return BadRequest();
            }
            _context.Entry(telefon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefon(int id)
        {
            var telefon = await _context.TelefonTbls.FindAsync(id);
            if (telefon == null)
            {
                return NotFound();
            }

            _context.TelefonTbls.Remove(telefon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("sortBy")]
        public async Task<ActionResult<IEnumerable<TelefonTbl>>> GetSortedTelefonListe(string sortBy = "marka", bool ascending = true)
        {
            IQueryable<TelefonTbl> query = _context.TelefonTbls;

            switch (sortBy.ToLower())
            {
                case "marka":
                    query = ascending ? query.OrderBy(t => t.Marka) : query.OrderByDescending(t => t.Marka);
                    break;
                case "model":
                    query = ascending ? query.OrderBy(t => t.Model) : query.OrderByDescending(t => t.Model);
                    break;
               
                default:
                    query = query.OrderBy(t => t.Marka);
                    break;
            }

            var telefonListe = await query.ToListAsync();
            return Ok(telefonListe);
        }

        [HttpGet("where")]
        public async Task<ActionResult<IEnumerable<TelefonTbl>>> GetFilteredTelefonListe(string marka = null)
        {
            IQueryable<TelefonTbl> query = _context.TelefonTbls;

            if (!string.IsNullOrEmpty(marka))
            {
                query = query.Where(t => t.Marka.ToLower() == marka.ToLower());
            }

            var telefonListe = await query.ToListAsync();
            return Ok(telefonListe);
        }

        [HttpGet("sayfalama")]
        public async Task<ActionResult<IEnumerable<TelefonTbl>>> GetPaginatedTelefonList(int page = 1, int pageSize = 5)
        {
            var telefonList = await _context.TelefonTbls
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();

            return Ok(telefonList);
        }


    }
}
