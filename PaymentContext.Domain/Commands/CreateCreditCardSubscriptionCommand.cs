using System;

namespace PaymentContext.Domain.Commands
{   
    /*
    Commands = pegar informação e retornar a informação
    Command de entrada.
    Tudo que precisa para fazer uma subscription no Cartão de Credito
     */
    public class CreateCreditCardSubscriptionCommand
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; private set; }
        public string PaymentNumber { get; set; }
        public DateTime PaidDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPaid { get; set; }
        public string Payer { get; set; }
        public string PayerDocument { get; set; }
        public string PayerDocumentType { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string PayerEmail { get; set; }

    }
}