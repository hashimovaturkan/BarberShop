using BarberShop.Application.EntityCQ.Payment.Interfaces;
using BarberShop.Application.Models.Dto.Payment;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Payment.Services
{
    public class PaymentService : IPaymentService
    {
        public PaymentService()
        {

        }
        public async Task<dynamic> PayAsync(PaymentDto paymentDto)
        {
            try
            {
                StripeConfiguration.ApiKey = "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";

                var optionsToken = new TokenCreateOptions
                {
                    
                    Card = new TokenCardOptions
                    {
                        Number = paymentDto.CardNumber,
                        ExpMonth = paymentDto.Month,
                        ExpYear = paymentDto.Year,
                        Cvc = paymentDto.Cvc
                    }
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(optionsToken);

                var options = new ChargeCreateOptions
                {
                    Amount = paymentDto.Value,
                    Currency = "pln",
                    Description = "test",
                    Source = stripeToken.Id
                };

                var service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.Paid)
                    return "Success";
                else
                    return "Failed";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
