using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.CNPJ);
            Assert.IsTrue(!doc.IsValid);
        }
        [TestMethod]
        public void ShouldReturnSucessWhenCNPJIsValid()
        {
            var doc = new Document("12345678912345", Domain.Enums.EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }
        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", Domain.Enums.EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }
        [TestMethod]
        public void ShouldReturnSucessWheCPFIsValid()
        {
            var doc = new Document("12345678911", Domain.Enums.EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}
