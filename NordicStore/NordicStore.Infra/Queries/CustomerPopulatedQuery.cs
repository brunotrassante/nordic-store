using System.Collections.Generic;

namespace NordicStore.Infra.Queries
{
    public class CustomerPopulatedQuery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<OrderPopulatedQuery> Orders { get; set; }
    }
}
