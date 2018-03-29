using NordicStore.Infra.Queries;
using System.Collections.Generic;

namespace NordicStore.Infra.Repositories
{
    public interface ICustomerQueryRepository
    {
        CustomerPopulatedQuery Get(int id);
        List<CustomerListedQuery> GetAll();
    }
}