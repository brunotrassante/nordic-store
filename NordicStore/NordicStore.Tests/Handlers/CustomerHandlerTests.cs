using NordicStore.Domain.Commands;
using NordicStore.Domain.Handlers;
using NordicStore.Domain.Repositories;
using NordicStore.Shared.Notifications;
using NordicStore.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NordicStore.Tests.Handlers
{
    [TestClass]
    public class CustomerHandlerTests
    {
        private ICustomerCommandRepository _repository;
        private CustomerHandler _customerHandler;

        public CustomerHandlerTests()
        {
            _repository = new FakeCustomerRepositiry();
            _customerHandler = new CustomerHandler(_repository);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenCreatingCustomerWithExistingEmail()
        {
            var createCommand = CreateValidCreateCustomerCommand();
            createCommand.Email = "alreadyused@email.com";

            var commandResult = _customerHandler.Handle(createCommand);

            Assert.IsFalse(commandResult.Success);
            Assert.IsTrue(commandResult.Data is List<Notification>);
            Assert.AreEqual(1, ((List<Notification>)commandResult.Data).Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenCreatingCustomerWithInvalidEmail()
        {
            var createCommand = CreateValidCreateCustomerCommand();
            createCommand.Email = "invalidEmail";

            var commandResult = _customerHandler.Handle(createCommand);

            Assert.IsFalse(commandResult.Success);
            Assert.IsTrue(commandResult.Data is List<Notification>);
            Assert.AreEqual(1, ((List<Notification>)commandResult.Data).Count);
        }

        [TestMethod]
        public void ShouldCreateUserWhenAllParametersAreRight()
        {
            var createCommand = CreateValidCreateCustomerCommand();      
            var commandResult = _customerHandler.Handle(createCommand);

            Assert.IsTrue(commandResult.Success);
            Assert.IsTrue(commandResult.Data is int);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenAddingOrderToInexistentCustomer()
        {
            var addOrderCommand = CreateValidAddOrderCommand();
            addOrderCommand.CustomerId = 999;

            var commandResult = _customerHandler.Handle(addOrderCommand);

            Assert.IsFalse(commandResult.Success);
            Assert.IsTrue(commandResult.Data is List<Notification>);
            Assert.AreEqual(1, ((List<Notification>)commandResult.Data).Count);
        }

        [TestMethod]
        public void ShouldReturnNotificationWhenPriceIsInvalid()
        {
            var addOrderCommand = CreateValidAddOrderCommand();
            addOrderCommand.Price = 0;

            var commandResult = _customerHandler.Handle(addOrderCommand);

            Assert.IsFalse(commandResult.Success);
            Assert.IsTrue(commandResult.Data is List<Notification>);
            Assert.AreEqual(1, ((List<Notification>)commandResult.Data).Count);
        }

        [TestMethod]
        public void ShouldAddOrderWhenParametersAreRight()
        {
            var addOrderCommand = CreateValidAddOrderCommand();

            var commandResult = _customerHandler.Handle(addOrderCommand);

            Assert.IsTrue(commandResult.Success);
            Assert.IsTrue(commandResult.Data is int);
        }

        private CreateCustomerCommand CreateValidCreateCustomerCommand()
        {
            return new CreateCustomerCommand()
            {
                Email = "brunotrassante@gmail.com",
                FirstName = "Bruno",
                LastName = "Trassante"
            };
        }

        private AddOrderToCustomerCommand CreateValidAddOrderCommand()
        {
            return new AddOrderToCustomerCommand()
            {
                CustomerId = 1,
                Price = 100
        };
    }
}
}
