using NordicStore.Shared.Commands;

namespace NordicStore.Domain.Commands
{
    public class AddOrderToCustomerCommand : ICommand
    {
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
    }
}
