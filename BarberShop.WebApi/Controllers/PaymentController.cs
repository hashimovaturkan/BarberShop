using AutoMapper;
using BarberShop.Application.EntityCQ.Payment.Interfaces;
using BarberShop.Application.Models.Dto.Payment;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.WebApi.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration, IMapper mapper)
        {
            _paymentService = paymentService;
            (_configuration, _mapper) = (configuration, mapper);
        }

        [HttpPost("Pay")]
        public async Task<ActionResult<int>> Create([FromBody] PaymentDto paymentDto)
        {
            var message = await _paymentService.PayAsync(paymentDto);

            return Ok(message);
        }

    }
}
