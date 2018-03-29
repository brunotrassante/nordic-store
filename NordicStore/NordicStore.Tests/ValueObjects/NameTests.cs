using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordicStore.Domain.ValueObjects;

namespace NordicStore.Tests.ValueObjects
{
    [TestClass]
    public class NameTests
    {
        [TestMethod]
        public void ShouldReturnNotificationWhenMissingFirstName()
        {
            var name = new Name(null, "Trassante");
            Assert.IsFalse(name.IsValid);
            Assert.AreEqual(1, name.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenMissingLastName()
        {
            var name = new Name("Bruno", string.Empty);
            Assert.IsFalse(name.IsValid);
            Assert.AreEqual(1, name.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenLastNameIsBiggerThanMaxValueAllowed()
        {
            var name = new Name("Bruno", "123456789012345678901234567890123456789012345678901234567890");
            Assert.IsFalse(name.IsValid);
            Assert.AreEqual(1, name.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenFirstNameIsBiggerThanMaxValueAllowed()
        {
            var name = new Name("123456789012345678901234567890123456789012345678901234567890", "Trassante");
            Assert.IsFalse(name.IsValid);
            Assert.AreEqual(1, name.Notifications.Count);
        }

        [TestMethod]
        public void ShouldBeValidWhenAllParametersAreProvided()
        {
            var name = new Name("Bruno", "Trassate");
            Assert.IsTrue(name.IsValid);
            Assert.AreEqual(0, name.Notifications.Count);
        }
    }
}
