using System;
using System.Threading.Tasks;
using CheckoutChallenge.API.DTOs;

namespace CheckoutChallenge.API.Services
{
    public interface IDataService
    {
        Task<MakePaymentResponseDTO> MakePayment(MakePaymentRequestDTO request);
        Task<GetPaymentResponseDTO> GetPayment(Guid id);
    }
}