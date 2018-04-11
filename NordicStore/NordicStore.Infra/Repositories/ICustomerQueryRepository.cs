using NordicStore.Infra.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NordicStore.Infra.Repositories
{
    public interface ICustomerQueryRepository
    {
        Task<CustomerPopulatedQuery> Get(int id);
        Task<List<CustomerListedQuery>> GetAll();
    }
}