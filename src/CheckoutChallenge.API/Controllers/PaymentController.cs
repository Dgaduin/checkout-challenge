using System;
using CheckoutChallenge.Domain.PaymentAggregate.Services;
using CheckoutChallenge.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CheckoutChallenge.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentProcessorService _service;

        public PaymentController(ILogger<PaymentController> logger)//, IPaymentProcessorService service)
        {
            _logger = logger;
            // _service = service;
        }

        [HttpGet]
        public IActionResult GetPayment(Guid id) => Ok("test");

        [HttpPost]
        public IActionResult MakePayment([FromBody]PaymentRequestDTO request) => Ok();
    }
}
