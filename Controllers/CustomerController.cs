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
    [AllowAnonymous]
    [Route("api/Customer")]

    public class CustomersController : ControllerBase
    {
        private readonly TheWholeContext _context;

        public CustomersController(TheWholeContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cust = _context.CustomersAcc;
            return Ok(new {message = "success retrieve data", status = true, data = cust});
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try 
            {
            var getit = _context.CustomersAcc.First(e => e.id == id);
            _context.SaveChanges();
            return Ok(new {message = "success retrieve data", status = true, data = getit});
            }

            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult Post(Customers custs)
        {
            var inputCustomer = new Customer
            {
                full_name = custs.data.attributes.full_name,
                username = custs.data.attributes.username,
                birthdate = custs.data.attributes.birthdate,
                password = custs.data.attributes.password,
                gender = custs.data.attributes.gender,
                email = custs.data.attributes.email,
                phone_number = custs.data.attributes.phone_number
            };
            
            _context.CustomersAcc.Add(inputCustomer);
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            inputCustomer.created_at = (long)time;
            inputCustomer.updated_at = (long)time;
            _context.SaveChanges();

            var customer = new CustomerResponse 
            { 
                id = inputCustomer.id,
                full_name = custs.data.attributes.full_name,
                username = custs.data.attributes.username,
                birthdate = custs.data.attributes.birthdate,
                password = custs.data.attributes.password,
                gender = custs.data.attributes.gender.ToString(),
                email = custs.data.attributes.email,
                phone_number = custs.data.attributes.phone_number
            };
            
            return Ok(new {message = "success retrieve data", status = true, data = customer});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
            var del = _context.CustomersAcc.First(e => e.id == id );
            _context.CustomersAcc.Remove(del);
            _context.SaveChanges();
            return Ok("Your data has been deleted.");
            }

            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Customers cust)
        {
            var customer = cust.data.attributes;

            try
            {
            var g = _context.CustomersAcc.Find(id);
            g.full_name = customer.full_name;
            g.username = customer.username;
            g.birthdate = customer.birthdate;
            g.password = customer.password;
            g.gender = customer.gender;
            g.email = customer.email;
            g.phone_number = customer.phone_number;
            var time = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).TotalSeconds;
            g.updated_at = (long)time;
            _context.SaveChanges();
            return Ok(new {message = "success retrieve data", status = true, data = customer});
            }

            catch (Exception)
            {
                return NotFound();
            }
        }
    }

    public class Customers
    {
        public Attributes data { get; set; }
    }

    public class Attributes
    {
        public Customer attributes { get; set; }
    }

     public class Customers1
    {
        public Attributes1 data { get; set; }
    }

    public class Attributes1
    {
        public CustomerResponse attributes { get; set; }
    }
}