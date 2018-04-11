using NordicStore.Domain.Commands;
using NordicStore.Domain.Commands.Results;
using NordicStore.Domain.Entities;
using NordicStore.Domain.Repositories;
using NordicStore.Domain.ValueObjects;
using NordicStore.Shared.Commands;
using NordicStore.Shared.Notifications;
using System.Threading.Tasks;

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

        public async Task<ICommandResult> Handle(CreateCustomerCommand command)
        {
            var isEmailInUse = _repository.IsEmailInUse(command.Email);

            var name = new Name(command.FirstName, command.LastName);
            var email = new Email(command.Email);
            var customer = new Customer(name, email);

            AddNotifications(name.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (await isEmailInUse)
                AddNotification(nameof(command.Email), "This email is already in use.");

            if (!IsValid)
                return CreateGenericErrorCommandResult();

            int customerId = await _repository.Create(customer);

            return new SuccessCommandResult("Customer created.", customerId);
        }

        public async Task<ICommandResult> Handle(AddOrderToCustomerCommand command)
        {
            var customerExists = _repository.Exists(command.CustomerId);
          
            var order = new Order(command.Price);

            AddNotifications(order.Notifications);

            if (!await customerExists)
                AddNotification(nameof(command.CustomerId), "Customer does not exists.");

            if (!IsValid)
                return CreateGenericErrorCommandResult();

            int orderId = await _repository.AddOrder(command.CustomerId, order);

            return new SuccessCommandResult("Order successfully added.", orderId);
        }

        private ICommandResult CreateGenericErrorCommandResult()
        {
            return new ErrorCommandResult("Some parameters were not valid. Please check the notification list.", Notifications);
        }
    }
}
