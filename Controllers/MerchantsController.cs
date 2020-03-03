using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TaskMediatrFluentValidation.Entity;
using Microsoft.AspNetCore.Authorization;

namespace TaskMediatrFluentValidation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Merchants")]

    public class MerchantsController : ControllerBase
    {
        private readonly TheWholeContext _context;

        public MerchantsController(TheWholeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var merch = _context.Merchants;
            return Ok(new {message = "success retrieve data", status = true, data = merch});
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try 
            {
            var getit = _context.Merchants.First(e => e.id == id);
            _context.SaveChanges();
            return Ok(new {message = "success retrieve data", status = true, data = getit});
            }

            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult Post(Merch m)
        {
            var merchs = m.data.attributes;
            _context.Merchants.Add(merchs);
            return Ok(new {message = "success retrieve data", status = true, data = merchs});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
            var del = _context.Merchants.First(e => e.id == id );
            _context.Merchants.Remove(del);
            _context.SaveChanges();
            return Ok("Your data has been deleted.");
            }

            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Merch m)
        {
            var merchs = m.data.attributes;

            try
            {
            var g = _context.Merchants.Find(id);
            g.name = merchs.name;
            g.image = merchs.image;
            g.address = merchs.address;
            g.rating = merchs.rating;
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            g.updated_at = (long)time;
            _context.SaveChanges();
            return Ok(new {message = "success retrieve data", status = true, data = merchs});
            }

            catch (Exception)
            {
                return NotFound();
            }
        }
    }

    public class Merch
    {
        public Attributes3 data { get; set; }
    }

    public class Attributes3
    {
        public Merchant attributes { get; set; }
    }

     
}