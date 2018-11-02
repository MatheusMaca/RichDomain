using System;
using Flunt.Notifications;
using Flunt.Validations;
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
    public class SubscriptionHandler :
    Notifiable,
    IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail fast validation
            command.Validade();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Infelizmente não foi possivel realizar seu cadastro.");
            }
            
            //Checar as notificações
            if(Invalid)
                return new CommandResult(false, "Não foi possivel realizar sua assinatura.");

            //Verificar se documento já existe
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já esta em uso");
            
            //Verificar se e-mail existe
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já esta em uso");
            
            //Gerar os values objects
            var name = new Name(command.FristName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            //Gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(
                command.BarCode,
                command.BoletoNumber,
                command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                command.Payer,
                new Document(command.PayerDocument, command.PayerDocumentType),
                address,
                email
            );

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar as validações
            AddNotifications(name, document, address, student, subscription, payment);
            
            //Salvar as informações
            _repository.CreateSubscription(student);
            
            //Enviar e-mail de boas vindas
            _emailService.send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Sua assinatura foi criada com sucesso!");

            //Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso!");
        }
    }
}