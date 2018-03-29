using NordicStore.Shared.Notifications;
using static System.String;

namespace NordicStore.Domain.ValueObjects
{
    public class Name : Notifiable
    {
        private const int MaxFirstNameSize = 50;
        private const int MaxLastNameSize = 50;

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if (IsNullOrEmpty(firstName) || IsNullOrEmpty(lastName))
                AddNotification($"{nameof(firstName)},{nameof(lastName)}", $"{nameof(firstName)} and {nameof(lastName)} are required.");
            if (!IsNullOrEmpty(firstName) && firstName.Length > MaxFirstNameSize)
                AddNotification(nameof(firstName), $"{nameof(firstName)} size must be lower than 50.");
            if (!IsNullOrEmpty(lastName) && lastName.Length > MaxLastNameSize)
                AddNotification(nameof(lastName), $"{nameof(lastName)} size must be lower than 50.");
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
