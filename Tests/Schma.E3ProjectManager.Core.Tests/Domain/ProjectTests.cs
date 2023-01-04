using System;
using System.Linq;
using Schma.E3ProjectManager.Core.Domain.Entities.ProjectAggregate;
using Xunit;

namespace Schma.E3ProjectManager.Core.Domain.Tests
{
    public class ProjectTests
    {
        [Fact]
        public void Project_Create_SetsTrackingNumber()
        {
            string trackingNumber = "1234";
            Project project = new Project(trackingNumber);

            Assert.Equal(trackingNumber, project.TrackingNumber);
        }

        [Fact]
        public void Project_AddProjectDevice_ProjectDevices_NotEmpty()
        {
            Project project = new Project("");
            project.AddProjectDevice("", "", "", "", 1m);

            Assert.NotEmpty(project.ProjectDevices);
        }

        [Fact]
        public void Project_AddProjectDevice_AddsProjectDeviceWithSpecifiedData()
        {
            string supplierArticleNumber = "xxx-0000.AAAbb89";
            string deviceName = "K100";
            string deviceLocation = "LOC";
            string deviceFunction = "FUNC";
            decimal quantity = 1m;

            Project project = new Project("");
            project.AddProjectDevice(supplierArticleNumber, deviceName, deviceLocation, deviceFunction, quantity);
            var projectDevice = project.ProjectDevices.Single();

            Assert.Equal(supplierArticleNumber, projectDevice.SupplierArticleNumber);
            Assert.Equal(deviceName, projectDevice.DeviceName);
            Assert.Equal(deviceLocation, projectDevice.DeviceLocation);
            Assert.Equal(deviceFunction, projectDevice.DeviceFunction);
            Assert.Equal(quantity, projectDevice.Quantity);
        }

        [Fact]
        public void Project_AddProjectDevice_OnZeroQuantity_ThrowsArgumentException()
        {
            Project project = new Project("");
            Assert.Throws<ArgumentException>(() => project.AddProjectDevice("", "", "","", 0m));
        }

        [Fact]
        public void Project_AddProjectDevice_OnNegativeQuantity_ThrowsArgumentException()
        {
            Project project = new Project("");
            Assert.Throws<ArgumentException>(() => project.AddProjectDevice("", "", "", "", -1));
        }

        [Fact]
        public void Project_UpdateProjectDeviceQuantity_Quantity_EqualToSet()
        {
            decimal previousQuantity = 1m;
            decimal updatedQuantity = 2m;
            Project project = new Project("");
            project.AddProjectDevice("", "", "", "", previousQuantity);
            Guid projectDeviceId = project.ProjectDevices.Single().Id;

            project.UpdateProjectDeviceQuantity(projectDeviceId, updatedQuantity);

            Assert.Equal(updatedQuantity, project.ProjectDevices.Single().Quantity);
        }

        [Fact]
        public void ProjectDevice_UpdateProjectDeviceQuantity_ProjectDeviceNotFound_ThrowsNullReferenceException()
        {
            Project project = new Project("");
            Assert.Throws<NullReferenceException>(() => project.UpdateProjectDeviceQuantity(Guid.NewGuid(), 1m));
        }

        [Fact]
        public void Project_UpdateProjectDeviceQuantity_QuantityNegative_ThrowsArgumentException()
        {
            Project project = new Project("");
            project.AddProjectDevice("devicename", "", "", "", 1);
            Guid projectDeviceId = project.ProjectDevices.Single().Id;
            Assert.Throws<ArgumentException>(() => project.UpdateProjectDeviceQuantity(Guid.NewGuid(), -1m));
        }

        [Fact]
        public void Project_UpdateProjectDeviceQuantity_QuantityZero_ThrowsArgumentException()
        {
            Project project = new Project("");
            project.AddProjectDevice("devicename", "", "", "", 1);
            Guid projectDeviceId = project.ProjectDevices.Single().Id;
            Assert.Throws<ArgumentException>(() => project.UpdateProjectDeviceQuantity(Guid.NewGuid(), 0m));
        }
    }
}