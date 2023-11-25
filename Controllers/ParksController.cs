using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using WaldeningApi.Models;
using AutoMapper.QueryableExtensions;


namespace WaldeningApi.Controllers
{


    namespace YourNamespace
    {
        [Route("api/[controller]")]
        [ApiController]
        public class ParksController : ControllerBase
        {
            private readonly WaldeningApiContext _context;

            public ParksController(WaldeningApiContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Retrieves a list of all parks from the database.
            /// </summary>
            /// <returns>A list of parks</returns>
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Park>>> Get([FromQuery] int? page = null, int pageSize = 10, string name = null, string state = null)
            {
                IQueryable<Park> query = _context.Parks.AsQueryable();
                if(name!=null)
                {
                    query = query.Where(entry => entry.Name == name);
                }
                if(state!=null)
                {
                    query = query.Where(entry => entry.State == state);
                }
                if (page == null)
                {
                    return Ok(await CreatePagedResults(query, page.Value, pageSize));
                }
                var toReturn = await CreatePagedResults(query, page.Value, pageSize);
                return Ok(toReturn);
            }

            protected async Task<PagedResults<Park>> CreatePagedResults(
                IQueryable<Park> queryable,
                int page,
                int pageSize)
            {

                var skipAmount = pageSize * (page - 1);
                var projection = queryable.Skip(skipAmount).Take(pageSize);

                var totalResults = await queryable.CountAsync();
                var results = await projection.ToListAsync();
                var mod = totalResults % pageSize;
                var totalPages = (totalResults / pageSize) + (mod == 0 ? 0 : 1);

                var nextUrl = page >= totalPages ? null : Url?.Link("Parks", new { page = page + 1, pageSize });
                return new PagedResults<Park>
                {
                    Results = results,
                    PageNumber = page,
                    PageSize = results.Count,
                    NumberOfPages = totalPages,
                    NumberOfResults = totalResults,
                    NextPageUrl = nextUrl
                };
            }

            /// <summary>
            /// Retrieves a specific park by its ID from the database.
            /// </summary>
            /// <param name="id">The ID of the park</param>
            /// <returns>The park with the specified ID</returns>
            [HttpGet("{id}")]
            public async Task<ActionResult<Park>> GetPark(int id)
            {
                var park = await _context.Parks.FindAsync(id);

                if (park == null)
                {
                    return NotFound();
                }

                return park;
            }

            /// <summary>
            /// Updates a park with the specified ID in the database.
            /// </summary>
            /// <param name="id">The ID of the park</param>
            /// <param name="park">The updated park object</param>
            /// <returns>No content if successful</returns>
            [HttpPut("{id}")]
            public async Task<IActionResult> PutPark(int id, Park park)
            {
                if (id != park.ParkId)
                {
                    return BadRequest();
                }

                _context.Entry(park).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkExists(id))
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

            /// <summary>
            /// Creates a new park in the database.
            /// </summary>
            /// <param name="park">The park object to create</param>
            /// <returns>The created park</returns>
            [HttpPost]
            public async Task<ActionResult<Park>> PostPark(Park park)
            {
                _context.Parks.Add(park);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPark), new { id = park.ParkId }, park);
            }

            /// <summary>
            /// Deletes a park with the specified ID from the database.
            /// </summary>
            /// <param name="id">The ID of the park to delete</param>
            /// <returns>No content if successful</returns>
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletePark(int id)
            {
                var park = await _context.Parks.FindAsync(id);
                if (park == null)
                {
                    return NotFound();
                }

                _context.Parks.Remove(park);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool ParkExists(int id)
            {
                return _context.Parks.Any(e => e.ParkId == id);
            }
        }
    }
}
