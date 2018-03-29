using NordicStore.Domain.Entities;
using NordicStore.Domain.Repositories;
using NordicStore.Infra.DataContexts;
using NordicStore.Infra.Queries;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace NordicStore.Infra.Repositories
{
    public class CustomerRepository : ICustomerCommandRepository, ICustomerQueryRepository
    {
        private NordicStoreDataContext _context;

        public CustomerRepository(NordicStoreDataContext context)
        {
            _context = context;
        }

        public int AddOrder(int id, Order order)
        {
            return _context
                .Connection
                .ExecuteScalar<int>(@"INSERT INTO [Order]
                            (CustomerId, Price ,CreatedDate)
                            OUTPUT Inserted.ID
                            VALUES(@Id,@Price,@CreatedDate)",
                            new { Id = id, Price = order.Price, CreatedDate = order.CreatedDate });
        }
        public int Create(Customer customer)
        {
            return _context
                 .Connection
                 .ExecuteScalar<int>(@"INSERT INTO Customer 
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

        public bool Exists(int id)
        {
            return _context
                .Connection
                .Query<bool>(@"SELECT CAST(COUNT(1) AS bit) 
                                FROM Customer 
                                WHERE Id = @id",
                                new { Id = id })
                .FirstOrDefault();
        }

        public CustomerPopulatedQuery Get(int id)
        {
            var customer = _context
                   .Connection
                   .Query<CustomerPopulatedQuery>(@"SELECT Id, Concat(FirstName,' ', LastName) as Name , Email
                                                    FROM Customer 
                                                    WHERE Id = @Id",
                                                    new { Id = id })
                    .FirstOrDefault();

            if (customer != null)
            {
                customer.Orders = _context
                   .Connection
                   .Query<OrderPopulatedQuery>(@"SELECT Id, Price, CreatedDate
                                                    FROM [Order]
                                                    WHERE CustomerId = @Id",
                                                    new { Id = customer.Id })
                   .ToList();
            }

            return customer;
        }

        public List<CustomerListedQuery> GetAll()
        {
            return _context
              .Connection
              .Query<CustomerListedQuery>(@"SELECT Id, Concat(FirstName,' ', LastName) as Name , Email
                                                FROM Customer")
              .ToList();
        }

        public bool IsEmailInUse(string email)
        {
            return _context
               .Connection
               .Query<bool>(@"SELECT CAST(COUNT(1) AS bit) 
                                FROM Customer 
                                WHERE Email = @Email",
                               new { Email = email })
               .FirstOrDefault();
        }
    }
}
