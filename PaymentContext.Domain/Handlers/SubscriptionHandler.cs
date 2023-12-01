﻿using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailServices _emailServices;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailServices emailServices)
        {
            _studentRepository = studentRepository;
            _emailServices = emailServices;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar cadastro");
            }

            // Verificar se Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se E-mail já está cadastro
            if (_studentRepository.DocumentExists(command.Email))
                AddNotification("Email", "Este Email já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.Street, command.City, command.ZipCode);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate,
                                            command.Total, command.TotalPaid, command.Payer,
                                            new Document(command.PayerDocument, command.PayerDocumentType),
                                            address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações
            if (!IsValid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            // Salvar as informações
            _studentRepository.CreateSubscription(student);

            // Enviar e-mail de boas vindas...
            _emailServices.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada'");


            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
