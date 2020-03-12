using System;
using System.Threading.Tasks;
using CheckoutChallenge.API.DTOs;
using CheckoutChallenge.Domain.PaymentAggregate;
using CheckoutChallenge.Domain.PaymentAggregate.Services;
using CheckoutChallenge.Infrastructure.Data;

namespace CheckoutChallenge.API.Services
{
    public class DataService : IDataService
    {
        private readonly IPaymentRepository _repository;
        private readonly IPaymentProcessorService _service;

        public DataService(IPaymentRepository repository, IPaymentProcessorService service)
        {
            _repository = repository;
            _service = service;
        }
        public async Task<GetPaymentResponseDTO> GetPayment(Guid id)
        {
            var payment = await _repository.FindByIdAsync(id).ConfigureAwait(false);

            if (payment is null) return null;

            return new GetPaymentResponseDTO
            {
                Amount = payment.Amount,
                Currency = payment.Currency.Value,
                Id = payment.Id,
                LastFourDigits = payment.CardNumber.LastFourDigits,
                MerchantId = payment.MerchantId.ToString(),
                NameOnCard = payment.NameOnCard,
            };
        }

        public async Task<MakePaymentResponseDTO> MakePayment(MakePaymentRequestDTO request)
        {
            var payment = new Payment(request.Currency,
                                      request.Amount,
                                      request.CardNumber,
                                      DateTime.Parse(request.ExpiryDate),
                                      request.NameOnCard,
                                      request.CVV,
                                      request.MerchantId,
                                      status: null);

            var result = await _service.ProcessPayment(payment).ConfigureAwait(false);

            return new MakePaymentResponseDTO
            {
                PaymentId = result.Id,
                PaymentStatus = result.PaymentStatus.Name,
            };
        }
    }
}