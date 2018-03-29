using NordicStore.Shared.Notifications;
using System.Text.RegularExpressions;

namespace NordicStore.Domain.ValueObjects
{
    public class Email : Notifiable
    {
        private const int MaxEmailSize = 50;

        public Email(string email)
        {
            Address = email;

            if (!IsEmail(email))
                AddNotification(nameof(email), "Invalid email format.");

            if (email.Length > MaxEmailSize)
                AddNotification(nameof(email), "Email size must be lower than 50.");
        }

        private bool IsEmail(string address)
        {
            const string pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return Regex.IsMatch(address, pattern);
        }

        public string Address { get; private set; }

        public override string ToString()
        {
            return Address;
        }
    }
}
