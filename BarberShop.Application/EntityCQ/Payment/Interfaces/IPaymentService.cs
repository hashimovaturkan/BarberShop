using BarberShop.Application.Models.Dto.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.EntityCQ.Payment.Interfaces
{
    public interface IPaymentService : IBaseService
    {
        public Task<dynamic> PayAsync(PaymentDto paymentDto);
    }
}
