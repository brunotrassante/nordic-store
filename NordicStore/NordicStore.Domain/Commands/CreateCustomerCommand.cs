using NordicStore.Shared.Commands;

namespace NordicStore.Domain.Commands
{
    public class CreateCustomerCommand : ICommand
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
