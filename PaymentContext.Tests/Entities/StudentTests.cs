﻿using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests;

[TestClass]
public class StudentTests
{

    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;


    public StudentTests()
    {
        _name = new Name("Vinicius", "Benicio");
        _document = new Document("42689167093", Domain.Enums.EDocumentType.CPF);
        _email = new Email("vinicius.benicio@google.com");
        _address = new Address("Av. Brig. Faria Lima", "3477", "Itaim Bibi", "São Paulo", "SP", "BR", "04538133");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("12345687", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Google SA", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);

        Assert.IsTrue(!_student.IsValid);
    }

    //[TestMethod]
    //public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
    //{
    //    var subscription = new Subscription(null);
    //    _student.AddSubscription(_subscription);
    //    Assert.IsTrue(!_student.IsValid);
    //}

    //[TestMethod]
    //public void ShouldReturnSucessWhenAddSubscription()
    //{
    //    var subscription = new Subscription(null);
    //    var payment = new PayPalPayment("12345687", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Google SA", _document, _address, _email);
    //    _subscription.AddPayment(payment);
    //    _student.AddSubscription(_subscription);
    //    Assert.IsTrue(_student.IsValid);

    //}
}