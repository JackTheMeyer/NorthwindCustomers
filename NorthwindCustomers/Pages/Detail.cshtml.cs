using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindCustomers.Core;
using NorthwindCustomers.Data;

namespace NorthwindCustomers.Pages
{
    public class DetailModel : PageModel
    {
        private readonly ICustomerData customerData;
        public Customer Customer;
        [TempData]
        public string Message { get; set; }
        public DetailModel(ICustomerData customerData)
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
    }
}
