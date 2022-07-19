using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindCustomers.Core;
using NorthwindCustomers.Data;

namespace NorthwindCustomers.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerData customerData;
        public Customer Customer { get; set; }
        public DeleteModel(ICustomerData customerData)
        {
            this.customerData = customerData;
        }

        public IActionResult OnGet(string customerId)
        {
            Customer = customerData.GetById(customerId);
            if (Customer == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public IActionResult OnPost(string customerId)
        {
            var customer = customerData.Delete(customerId);
            customerData.Commit();

            if (customerData == null)
                return RedirectToPage("./NotFound");

            TempData["Message"] = $"{customer.CompanyName} deleted";
            return RedirectToPage("./Customer");
        }
    }
}
