using System;
using CheckoutChallenge.API.Services;
using CheckoutChallenge.Domain.PaymentAggregate.Services;
using CheckoutChallenge.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CheckoutChallenge.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IDataService _service;

        public PaymentController(ILogger<PaymentController> logger, IDataService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(GetPaymentResponseDTO), 200)]
        public async Task<ActionResult<GetPaymentResponseDTO>> GetPayment(Guid id)
        {
            _logger.LogInformation("Getting payment with Id:{Id}", id);
            try
            {
                var dto = await _service.GetPayment(id).ConfigureAwait(false);
                if (dto is null)
                {
                    return NotFound();
                }
                return dto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an issue with getting the payment");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(MakePaymentResponseDTO), 200)]
        public async Task<ActionResult<MakePaymentResponseDTO>> MakePayment([FromBody]MakePaymentRequestDTO request)
        {
            _logger.LogInformation("Starting payment processing");
            try
            {
                return await _service.MakePayment(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an issue with getting the payment");
                return StatusCode(500);
            }
        }
    }
}
