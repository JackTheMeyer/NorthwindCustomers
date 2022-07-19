using Microsoft.EntityFrameworkCore;
using NorthwindCustomers.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindCustomers.Data
{
    public class NorthwindDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options) : base(options)
        {
            
        }
    }
}
