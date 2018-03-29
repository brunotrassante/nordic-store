using NordicStore.Domain.ValueObjects;
using NordicStore.Shared.Notifications;
using System.Collections.Generic;

namespace NordicStore.Domain.Entities
{
    public class Customer : Notifiable
    {
        private List<Order> _orders;

        public Customer(Name name, Email email)
        {
            Name = name;
            Email = email;
            _orders = new List<Order>();

            if (name == null)
                AddNotification(nameof(name), "Custumer must have a name.");

            if (email == null)
                AddNotification(nameof(email), "Custumer must have an email.");
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public IReadOnlyCollection<Order> Orders => _orders;

        public void AddOrder(Order order)
        {
            if (order == null)
                AddNotification(nameof(order), "Order cannot be null.");
            else
                _orders.Add(order);
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
