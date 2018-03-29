using System;

namespace NordicStore.Infra.Queries
{
    public class OrderPopulatedQuery
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
