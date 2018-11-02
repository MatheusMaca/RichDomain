
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTest
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.BarCode = "345678";
            command.FristName = "Chino";
            command.LastName = "Aburame";
            command.Document = "99999999999";
            command.Email = "Mac@rini.io";
            command.BarCode = "123456789";
            command.BoletoNumber = "1234567";
            command.PaymentNumber = "123121";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Aburame Corp";
            command.PayerDocument = "1234567891011";
            command.PayerDocumentType = EDocumentType.CPF;
            command.Street = "Bugs street";
            command.Number = "1";
            command.Neighborhood = "Hive";
            command.City = "Leaf village";
            command.State = "Fire state";
            command.Country = "Fire country";
            command.ZipCode = "12345678";
            command.PayerEmail = "Chino@aburame.com";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}