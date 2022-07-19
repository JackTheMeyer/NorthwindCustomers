using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindCustomers.Core;
using NorthwindCustomers.Data;

namespace NorthwindCustomers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly NorthwindDbContext _context;
        public CustomersController(NorthwindDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers

        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        // GET: api/Customers/5
        [HttpGet("{customerid}")]
        public async Task<IActionResult> GetCustomer([FromRoute] string customerId)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [HttpPut("{customerid}")]
        public async Task<IActionResult> PutCustomer([FromRoute] string customerId, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (customerId != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CustomerExists(customerId))
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
        public async Task<IActionResult> PostRestaurant([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Restaurants/5
        [HttpDelete("{customerid}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] string customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(customerId
                );
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        private bool CustomerExists(string customerId)
        {
            return _context.Customers.Any(e => e.CustomerId == customerId);
        }
    }
}
