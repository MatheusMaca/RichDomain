namespace PaymentContext.Domain.Services
{
    public interface IEmailService
    {
        void send(string to, string email, string subject, string body);
    }
}