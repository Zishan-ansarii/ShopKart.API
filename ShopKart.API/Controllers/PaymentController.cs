using Microsoft.AspNetCore.Mvc;
using ShopKart.API.DTOs;
using ShopKart.API.Strategies;

namespace ShopKart.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentStrategyResolver _resolver;
        public PaymentController(IPaymentStrategyResolver resolver)
        {
            _resolver = resolver;
        }
        [HttpPost("process")]
        public async Task<IActionResult> PaymentProcess(PaymentDTO dto)
        {
            try
            {
                var strategy = _resolver.GetStrategy(dto.PaymentMethod);
                var paymentResult = await strategy.ProcessPayment(dto.Amount);

                if (!paymentResult.IsSuccess)
                {
                    return BadRequest(paymentResult);
                }

                return Ok(paymentResult);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
