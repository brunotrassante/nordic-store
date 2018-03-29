using NordicStore.Domain.Commands;
using NordicStore.Domain.Commands.Results;
using NordicStore.Domain.Entities;
using NordicStore.Domain.Repositories;
using NordicStore.Domain.ValueObjects;
using NordicStore.Shared.Commands;
using NordicStore.Shared.Notifications;

namespace NordicStore.Domain.Handlers
{
    public class CustomerHandler :
        Notifiable,
        ICommandHandler<AddOrderToCustomerCommand>,
        ICommandHandler<CreateCustomerCommand>
    {
        private ICustomerCommandRepository _repository;

        public CustomerHandler(ICustomerCommandRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateCustomerCommand command)
        {
            if (_repository.IsEmailInUse(command.Email))
                AddNotification(nameof(command.Email), "This email is already in use.");

            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var customer = new Customer(name, email);

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (!IsValid)
                return CreateGenericErrorCommandResult();

            int customerId = _repository.Create(customer);

            return new SuccessCommandResult("Customer created.", customerId);
        }

        public ICommandResult Handle(AddOrderToCustomerCommand command)
        {
            if (!_repository.Exists(command.CustomerId))
                AddNotification(nameof(command.CustomerId), "Customer does not exists.");

            var order = new Order(command.Price);

            AddNotifications(order.Notifications);

            if (!IsValid)
                return CreateGenericErrorCommandResult();

            int orderId = _repository.AddOrder(command.CustomerId, order);

            return new SuccessCommandResult("Order successfully added.", orderId);
        }

        private ICommandResult CreateGenericErrorCommandResult()
        {
            return new ErrorCommandResult("Some parameters were not valid. Please check the notification list.", Notifications);
        }
    }
}
