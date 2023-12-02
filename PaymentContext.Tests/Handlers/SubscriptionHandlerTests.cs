using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.Handlers
{

    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new StudentQueriesTests(), new FakeEmailServices());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Vinicius";
            command.LastName = "Benicio de Santana";
            command.Document = "9999999999";
            command.Email = "hello@microsoft.com";
            command.BarCode = "1212121212";
            command.BoletoNumber = "1212121212";
            command.PaymentNumber = "1212121212";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Microsoft";
            command.PayerDocument = "121212121212";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "hello@microsoft.com";
            command.Street = "asjasas";
            command.Number = "1212";
            command.Neighborhood = "asasas";
            command.City = "asas";
            command.State = "asas";
            command.Country = "asas";
            command.ZipCode = "98218912893123";

            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);


        }
    }

}
