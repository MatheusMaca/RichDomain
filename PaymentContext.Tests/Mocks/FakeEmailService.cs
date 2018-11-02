using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Services;

namespace PaymentContext.Tests.Mocks
{
    public class FakeEmailService : IEmailService
    {
        public void send(string to, string email, string subject, string body)
        {
        }
    }
}