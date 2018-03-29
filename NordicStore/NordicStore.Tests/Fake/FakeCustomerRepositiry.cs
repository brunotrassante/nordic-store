using NordicStore.Domain.Entities;
using NordicStore.Domain.Repositories;

namespace NordicStore.Tests.Mocks
{
    public class FakeCustomerRepositiry : ICustomerCommandRepository
    {
        public int AddOrder(int id, Order order)
        {
            return 1;
        }

        public bool IsEmailInUse(string email)
        {
            return email == "alreadyused@email.com";
        }

        public int Create(Customer customer)
        {
            return 1;
        }

        public bool Exists(int id)
        {
            return id == 1;
        }
    }
}
