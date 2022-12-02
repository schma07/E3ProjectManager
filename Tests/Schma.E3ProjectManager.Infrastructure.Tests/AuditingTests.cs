using System.Linq;
using Schma.E3ProjectManager.Infrastructure.Models;
using Schma.E3ProjectManager.Tests;
using Xunit;

namespace Schma.E3ProjectManager.Infrastructure.Tests
{
    public class AuditingTests : TestBase
    {
        [Fact]
        public void Auditing_OnAdd_AuditableEntity_CreatesAuditHistoryRecord()
        {
            OrderEntity order = new OrderEntity();
            ApplicationContext.Orders.Add(order);
            ApplicationContext.SaveChanges();

            var auditHistory = ApplicationContext.AuditHistory.SingleOrDefault();

            Assert.NotNull(auditHistory);
        }
    }
}