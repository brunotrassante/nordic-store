using NordicStore.Domain.Entities;
using NordicStore.Domain.Repositories;
using System.Threading.Tasks;

namespace NordicStore.Tests.Mocks
{
    public class FakeCustomerRepositiry : ICustomerCommandRepository
    {
        public Task<int> AddOrder(int id, Order order)
        {
            return Task.FromResult(1);
        }

        public Task<bool> IsEmailInUse(string email)
        {
            return Task.FromResult(email == "alreadyused@email.com");
        }

        public Task<int> Create(Customer customer)
        {
            return Task.FromResult(1);
        }

        public Task<bool> Exists(int id)
        {
            return Task.FromResult(id == 1);
        }
    }
}
