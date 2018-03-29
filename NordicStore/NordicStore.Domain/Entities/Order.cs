using NordicStore.Shared.Notifications;
using System;

namespace NordicStore.Domain.Entities
{
    public class Order : Notifiable
    {
        public Order(decimal price)
        {
            Price = price;
            CreatedDate = DateTime.Now;

            if (price <= 0)
                AddNotification(nameof(price), "Price must be bigger than zero.");

            if (price > 999999999999999)
                AddNotification(nameof(price), "Price must be lower than 999999999999999,00.");
        }

        public decimal Price { get; private set; }
        public DateTime CreatedDate { get; private set; }
    }
}
