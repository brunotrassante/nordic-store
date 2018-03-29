using NordicStore.Domain.Entities;

namespace NordicStore.Domain.Repositories
{
    public interface ICustomerCommandRepository
    {
        int Create(Customer customer);
        int AddOrder(int id, Order order);
        bool IsEmailInUse(string email);
        bool Exists(int id);
    }
}