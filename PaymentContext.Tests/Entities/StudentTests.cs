using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void AdicionarAssinatura()
    {
        var subscription = new Subscription(null);
        var student = new Student("Vinicius", "Benicio de Santana", "1221212", "viniciusbenicio@email.com");
        student.AddSubscription(subscription);
    }
}