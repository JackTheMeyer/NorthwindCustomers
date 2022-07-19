using NorthwindCustomers.Core;

namespace NorthwindCustomers.Data
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetCustomersByName(string name);
        Customer GetById(string id);
        Customer Update(Customer updatedCustomer);
        Customer Add(Customer newCustomer);
        Customer Delete(string id);
        int Commit();
    }
}