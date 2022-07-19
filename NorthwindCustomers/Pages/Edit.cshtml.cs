using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NorthwindCustomers.Core;
using NorthwindCustomers.Data;

namespace NorthwindCustomers.Pages
{
    public class EditModel : PageModel
    {
        private readonly ICustomerData customerData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Customer Customer { get; set; }
        
        public EditModel(ICustomerData customerData, IHtmlHelper htmlHelper)
        {
            this.customerData = customerData;
            this.htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(string? customerId)
        {
            if (customerId != null)
            {
                Customer = customerData.GetById(customerId);
            }
            else
            {
                Customer = new Customer();
            }

            if (Customer == null)
                return RedirectToPage("./NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {

            }

            if (Customer.CustomerId != null)
            {
                customerData.Update(Customer);
                TempData["Message"] = "Customer Updated";
            }
            else
            {
                customerData.Add(Customer);
                TempData["Message"] = "Customer Added";
            }
            customerData.Commit();

            return Page();
        }
    }
}
