using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NorthwindCustomers.Core;
using NorthwindCustomers.Data;

namespace NorthwindCustomers.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ICustomerData customerData;
        private readonly ILogger<PrivacyModel> _logger;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public IEnumerable<Customer>? Customers { get; set; }

        public PrivacyModel(ICustomerData customerData, ILogger<PrivacyModel> logger)
        {
            this.customerData = customerData;
            SearchTerm = "A";

            _logger = logger;
        }

        public void OnGet()
        {
            Customers = customerData.GetCustomersByName(SearchTerm);
        }
    }
}