using NordicStore.Domain.Entities;
using NordicStore.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NordicStore.Tests.Entities
{
    [TestClass]
    public class CustomerTests
    {
        private Name _validName;
        private Email _validEmail;
        private Order _validOrder;

        public CustomerTests()
        {
            _validName = new Name("Bruno", "Trassante");
            _validEmail = new Email("brunotrassante@gmail.com");
            _validOrder = new Order(300);

        }
        [TestMethod]
        public void ShouldReturnNotificationWhenNameIsNull()
        {
            var customer = new Customer(null, _validEmail);
            Assert.IsFalse(customer.IsValid);
            Assert.AreEqual(1, customer.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenEmailIsNull()
        {
            var customer = new Customer(_validName, null);
            Assert.IsFalse(customer.IsValid);
            Assert.AreEqual(1, customer.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationsWhenAllParametersAreNull()
        {
            var customer = new Customer(null, null);
            Assert.IsFalse(customer.IsValid);
            Assert.AreEqual(2, customer.Notifications.Count);
        }

        [TestMethod]
        public void ShouldBeValidWhenAllParametersAreProvided()
        {
            var customer = new Customer(_validName, _validEmail);
            Assert.IsTrue(customer.IsValid);
            Assert.AreEqual(0, customer.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationsWhenAllTryingToAddNullOrder()
        {
            var customer = new Customer(_validName, _validEmail);
            customer.AddOrder(null);
            Assert.AreEqual(0, customer.Orders.Count);
            Assert.AreEqual(1, customer.Notifications.Count);
        }

        [TestMethod]
        public void ShouldAddOrderWhenValid()
        {
            var customer = new Customer(_validName, _validEmail);
            customer.AddOrder(_validOrder);
            Assert.AreEqual(1, customer.Orders.Count);
        }
    }
}
