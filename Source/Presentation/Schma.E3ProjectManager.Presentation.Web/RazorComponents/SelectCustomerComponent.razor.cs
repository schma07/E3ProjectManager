using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Schma.E3ProjectManager.Core.Application.Queries.Customers;
using Schma.E3ProjectManager.Core.Application.ReadModels.Customers;
using Schma.E3ProjectManager.Presentation.Framework.Components;

namespace Schma.E3ProjectManager.Presentation.Web.RazorComponents
{
    public partial class SelectCustomerComponent : BaseComponent
    {
        [Parameter]
        public Func<CustomerReadModel, bool> Filter { get; set; }        
        [Parameter]
        public EventCallback<CustomerReadModel> OnCustomerSelected { get; set; }
        [Parameter]
        public bool ShowLoader { get; set; }
        public CustomerReadModel SelectedCustomer { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string ParentId { get; set; }

        public IEnumerable<CustomerReadModel> AvailableCustomers { get; set; } = new List<CustomerReadModel>();

        public Select2<CustomerReadModel> SelectReference;
        protected override async Task OnParametersSetAsync()
        {
            var query = new GetAllCustomersQuery();

            var result = await SendRequestAsync(query, ShowLoader);

            if (result.Failed)
            {
                return;
            }

            AvailableCustomers = result.Data.ToList();
                        
            if (Filter != null)
                AvailableCustomers = AvailableCustomers.Where(Filter);

            await base.OnParametersSetAsync();
        }

        public async Task CustomerSelected(CustomerReadModel customer)
        {
            SelectReference?.ResetSelection();
            await OnCustomerSelected.InvokeAsync(customer);
        }
    }
}
