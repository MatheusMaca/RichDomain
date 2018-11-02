using Flunt.Validations;
using PaymentContext.Shared.ValueObject;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string fristName, string lastName)
        {
            FristName = fristName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FristName, 3, "Name.FristName", "Nome deve conter pelo 3 caracteres")
                .HasMinLen(FristName, 3, "Name.LastName", "Sobrenome deve conter pelo 3 caracteres")
                .HasMaxLen(FristName, 40, "Name.FristName", "Nome deve conter até 40 caracteres")
                );
        }

        public string FristName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FristName} {LastName}";
        }
    }
}