﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using userprofileAPI.Models;

namespace userprofileAPI.Controllers
{
    [Route("api/Expertises")]
    [ApiController]
    public class ExpertisesController : ControllerBase
    {
        private readonly ProfileDbContext _context = new ProfileDbContext();

        //public ExpertisesController(UserprofileDbContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Expertises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expertise>>> GetExpertises()
        {
            return await _context.Expertises.ToListAsync();
        }

        // GET: api/Expertises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expertise>> GetExpertise(int id)
        {
            var expertise = await _context.Expertises.FindAsync(id);

            if (expertise == null)
            {
                return NotFound();
            }

            return expertise;
        }

        // PUT: api/Expertises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpertise(int id, Expertise expertise)
        {
            if (id != expertise.Id)
            {
                return BadRequest();
            }

            _context.Entry(expertise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpertiseExists(id))
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

        // POST: api/Expertises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Expertise>> PostExpertise(Expertise expertise)
        {
            _context.Expertises.Add(expertise);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpertise", new { id = expertise.Id }, expertise);
        }

        // DELETE: api/Expertises/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpertise(int id)
        {
            var expertise = await _context.Expertises.FindAsync(id);
            if (expertise == null)
            {
                return NotFound();
            }

            _context.Expertises.Remove(expertise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpertiseExists(int id)
        {
            return _context.Expertises.Any(e => e.Id == id);
        }
    }
}
