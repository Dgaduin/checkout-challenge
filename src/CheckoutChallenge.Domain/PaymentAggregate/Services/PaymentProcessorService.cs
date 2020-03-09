using System;
using System.Threading.Tasks;

namespace CheckoutChallenge.Domain.PaymentAggregate.Services
{
    public class PaymentProcessorService : IPaymentProcessorService
    {
        private readonly ICurrencySupportedService _currencyService;
        private readonly IPaymentSenderService _paymentSenderService;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentProcessorService(
            ICurrencySupportedService currencyService,
            IPaymentSenderService paymentSenderService,
            IPaymentRepository paymentRepository
        )
        {
            _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService)); ;
            _paymentSenderService = paymentSenderService ?? throw new ArgumentNullException(nameof(paymentSenderService)); ;
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository)); ;
        }

        public async Task<Guid> ProcessPayment(Payment payment)
        {
            if (payment is null)
                throw new ArgumentNullException(nameof(payment));

            var isCurrencySupported = await _currencyService.IsCurrencySupported(payment.Currency);
            if (!isCurrencySupported)
                throw new PaymentAggregateException();

            var paymentStatus = await _paymentSenderService.SendPayment(payment);
            var updatedPayment = payment.UpdateStatus(paymentStatus);

            _paymentRepository.Add(updatedPayment);
            var paymentId = await _paymentRepository.UnitOfWork.SaveChangesAsync();
            return paymentId;
        }
    }
}