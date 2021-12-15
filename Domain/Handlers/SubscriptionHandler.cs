using Domain.Commands;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Services;
using Domain.ValueObjects;
using Flunt.Notifications;
using Shared.Commands;
using Shared.Handlers;
using System;

namespace Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, ICommandHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;
        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validation
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }

            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso.");

            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este email já está em uso;");

            //VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType), address, email, command.BarCode, command.BoletoNumber);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Validaçoes
            AddNotifications(name, document, email, address, student, subscription, payment);
            
            if (!IsValid)
                return new CommandResult(false,"Não foi possível completar sua assinatura.");

            _studentRepository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo a plataforma.", "Sua assinatura foi criada.");

            return new CommandResult(true, "Assinatura realizada com sucesso.");
        }
    }
}
