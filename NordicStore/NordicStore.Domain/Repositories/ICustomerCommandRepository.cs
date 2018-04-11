using NordicStore.Domain.Entities;
using System.Threading.Tasks;

namespace NordicStore.Domain.Repositories
{
    public interface ICustomerCommandRepository
    {
        Task<int> Create(Customer customer);
        Task<int> AddOrder(int id, Order order);
        Task<bool> IsEmailInUse(string email);
        Task<bool> Exists(int id);
    }
}