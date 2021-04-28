using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AddressBookContext _context;

        public ContactsController(AddressBookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacts>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contacts>> GetContacts(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);

            if (contacts == null)
            {
                return NotFound();
            }

            return contacts;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacts(int id, Contacts contacts)
        {
            contacts.UpdatedDate = DateTime.Now.ToString();
            if (id != contacts.Id)
            {
                return BadRequest();
            }

            _context.Entry(contacts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Contacts>> PostContacts(Contacts contacts)
        {
            //contacts.Id = 3;
            contacts.UpdatedDate = DateTime.Now.ToString();
            _context.Contacts.Add(contacts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactsExists(contacts.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContacts", new { id = contacts.Id }, contacts);
        }

        [Route("PostMultipleContacts")]
        [HttpPost]
        public async Task<ActionResult<Contacts>> PostMultipleContacts(Contacts[] contacts)
        {
            //contacts.Id = 3;
            foreach (var item in contacts)
            {
                item.UpdatedDate = DateTime.Now.ToString();
                _context.Contacts.Add(item);
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                foreach (var item in contacts)
                {
                    if (ContactsExists(item.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Contacts>> DeleteContacts(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();

            return contacts;
        }

        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
