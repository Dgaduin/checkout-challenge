using CheckoutChallenge.Domain.PaymentAggregate.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckoutChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentProcessorService _service;

        public PaymentController(ILogger<PaymentController> logger, IPaymentProcessorService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public IActionResult GetPayment() => Ok();

        [HttpPost]
        public IActionResult MakePayment() => Ok();
    }
}
