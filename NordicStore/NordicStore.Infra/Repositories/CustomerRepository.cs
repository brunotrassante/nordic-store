using NordicStore.Domain.Entities;
using NordicStore.Domain.Repositories;
using NordicStore.Infra.DataContexts;
using NordicStore.Infra.Queries;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NordicStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerCommandRepository, ICustomerQueryRepository
    {
        private NordicStoreDataContext _context;

        public CustomerRepository(NordicStoreDataContext context)
        {
            _context = context;
        }

        public async Task<int> AddOrder(int id, Order order)
        {
            return await _context
                .Connection
                .ExecuteScalarAsync<int>(@"INSERT INTO [Order]
                            (CustomerId, Price ,CreatedDate)
                            OUTPUT Inserted.ID
                            VALUES(@Id,@Price,@CreatedDate)",
                            new { Id = id, Price = order.Price, CreatedDate = order.CreatedDate });
        }
        public async Task<int> Create(Customer customer)
        {
            return await _context
                 .Connection
                 .ExecuteScalarAsync<int>(@"INSERT INTO Customer 
                            (FirstName, LastName, Email)
                            OUTPUT Inserted.ID
                            VALUES(@FirstName,@LastName,@Email)",
                            new
                            {
                                FirstName = customer.Name.FirstName,
                                LastName = customer.Name.LastName,
                                Email = customer.Email.ToString()
                            });
        }

        public async Task<bool> Exists(int id)
        {
            return await _context
                .Connection
                .QueryFirstOrDefaultAsync<bool>(@"SELECT CAST(COUNT(1) AS bit) 
                                FROM Customer 
                                WHERE Id = @id",
                                new { Id = id });
        }

        public async Task<CustomerPopulatedQuery> Get(int id)
        {
            var customer = await _context
                   .Connection
                   .QueryFirstAsync<CustomerPopulatedQuery>(@"SELECT Id, Concat(FirstName,' ', LastName) as Name , Email
                                                    FROM Customer 
                                                    WHERE Id = @Id",
                                                    new { Id = id });


            if (customer != null)
            {
                customer.Orders = (await _context
                  .Connection
                  .QueryAsync<OrderPopulatedQuery>(@"SELECT Id, Price, CreatedDate
                                                    FROM [Order]
                                                    WHERE CustomerId = @Id",
                                                   new { Id = customer.Id })).ToList();
            }

            return customer;
        }

        public async Task<List<CustomerListedQuery>> GetAll()
        {
            return (await _context
              .Connection
              .QueryAsync<CustomerListedQuery>(@"SELECT Id, Concat(FirstName,' ', LastName) as Name , Email
                                                FROM Customer"))
              .ToList();
        }

        public async Task<bool> IsEmailInUse(string email)
        {
            return await _context
               .Connection
               .QueryFirstOrDefaultAsync<bool>(@"SELECT CAST(COUNT(1) AS bit) 
                                FROM Customer 
                                WHERE Email = @Email",
                               new { Email = email });
        }
    }
}
