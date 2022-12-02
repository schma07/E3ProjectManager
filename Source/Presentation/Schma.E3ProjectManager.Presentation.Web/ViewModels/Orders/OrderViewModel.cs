using System;

namespace Schma.E3ProjectManager.Presentation.Web.ViewModels.Orders
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string TrackingNumber { get; set; }
        public decimal TotalAmount { get; set; }
    }
}