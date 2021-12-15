using Flunt.Validations;
using Shared.ValueObjects;

namespace Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(
                new Contract<Name>()
                .Requires()
                .IsGreaterOrEqualsThan(FirstName.ToString(), 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres.")
                .IsGreaterOrEqualsThan(LastName.ToString(), 3, "Name.LastName", "Nome deve conter pelo menos 3 caracteres.")
           );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
