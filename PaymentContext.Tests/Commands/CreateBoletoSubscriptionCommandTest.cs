using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTest
    {
        //Red, Green, Refactor
        //Falhar, passar, refatorar
        //Retornar um erro quando cnpj for inválido
        //Retornar erro quando cnpj for invalido
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FristName = "";
            command.Validade();
            Assert.AreEqual(false, command.Valid);
        }
    }
}
