using NordicStore.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace NordicStore.Tests.ValueObjects
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void ShouldReturnNotificationWhenEmailIsNotValid()
        {
            var senselessEmail = new Email("invalidEmail");
            Assert.IsFalse(senselessEmail.IsValid);
            Assert.AreEqual(1, senselessEmail.Notifications.Count);

            var missingEndEmail = new Email("invalidEmail@domain");
            Assert.IsFalse(missingEndEmail.IsValid);
            Assert.AreEqual(1, missingEndEmail.Notifications.Count);

            var missingStartEmail = new Email("@domain");
            Assert.IsFalse(missingStartEmail.IsValid);
            Assert.AreEqual(1, missingStartEmail.Notifications.Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenEmailSizeIsBiggerThanMaxAllowed()
        {
            var bigEmail = new Email("reeeellybiiiiiiiiiiigEmailllllllllllllllllllllllllllllllllllllll@gmail.com");
            Assert.IsFalse(bigEmail.IsValid);
            Assert.AreEqual(1, bigEmail.Notifications.Count);
        }

        [TestMethod]
        public void ShouldBeValidWhenEmailIsValid()
        {
            var validEmail = new Email("brunotrassante@gmail.com");
            Assert.IsTrue(validEmail.IsValid);
            Assert.AreEqual(0, validEmail.Notifications.Count);
        }
    }
}
