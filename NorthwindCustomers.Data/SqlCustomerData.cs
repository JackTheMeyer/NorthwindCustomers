using NorthwindCustomers.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindCustomers.Data
{
    public class SqlCustomerData : ICustomerData
    {
        private readonly NorthwindDbContext _db;
        public SqlCustomerData(NorthwindDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            var customerByName = from c in _db.Customers
                                 where c.CompanyName.Contains(name) || string.IsNullOrEmpty(name)
                                 orderby c.ContactName
                                 select c;

            return customerByName;
        }

        public Customer Add(Customer newCustomer)
        {
            _db.Add(newCustomer);
            return newCustomer;
        }

        public Customer Delete(string id)
        {
            var customer = GetById(id);
            if (customer != null)
                _db.Customers.Remove(customer);

            return customer;
        }

        public Customer GetById(string id)
        {
            return _db.Customers.Find(id);
        }

        public Customer Update(Customer updatedCustomer)
        {
            var entity = _db.Customers.Attach(updatedCustomer);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return updatedCustomer;
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }
    }
}
