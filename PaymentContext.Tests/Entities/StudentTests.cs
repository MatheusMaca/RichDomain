using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Shino", "Aburame");
            _document = new Document("34324947007", EDocumentType.CPF);
            _email = new Email("NoshiBugs@bg.com");
            _address = new Address("Rua Aburame", "1234", "Bairro Bugs", "Aldeia da folha", "PR", "BR", "09230100");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        /// <summary>
        /// Deve retornar um erro quando subscription já estiver ativa.
        /// </summary>
        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("1234567", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, 
                                            "ABURAME CORP", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        /// <summary>
        /// Deve retornar um erro quando a subscription não conter pagamento.
        /// </summary>
        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        /// <summary>
        /// Deve retornar sucesso quando a subscription conter apenas uma inscrição. 
        /// </summary>
        [TestMethod]
        public void ShouldReturnSucessWhenAddSubscription()
        {
            var payment = new PayPalPayment("1234567", DateTime.Now, DateTime.Now.AddDays(5), 10, 10,
                                            "ABURAME CORP", _document, _address, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Valid);
        }
    }
}