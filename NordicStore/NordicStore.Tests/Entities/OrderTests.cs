using NordicStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace NordicStore.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void ShouldReturnNotificationWhenPriceIsLowerThanZero()
        {
            var orderNegativePrice = new Order(-10);
            Assert.IsFalse(orderNegativePrice.IsValid);
            Assert.AreEqual(1, orderNegativePrice.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenPriceIsZero()
        {
            var orderNoPrice = new Order(0);
            Assert.IsFalse(orderNoPrice.IsValid);
            Assert.AreEqual(1, orderNoPrice.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenPriceIsBiggerThanMaxValue()
        {
            var orderNoPrice = new Order(9999999999999999999);
            Assert.IsFalse(orderNoPrice.IsValid);
            Assert.AreEqual(1, orderNoPrice.Notifications.Count);
        }

        [TestMethod]
        public void ShouldBeValidWhenPriceIsBiggerThanZero()
        {
            var orderOk = new Order(1000);
            Assert.IsTrue(orderOk.IsValid);
            Assert.AreEqual(0, orderOk.Notifications.Count);
        }
    }
}
