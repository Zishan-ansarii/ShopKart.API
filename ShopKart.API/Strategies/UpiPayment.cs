namespace ShopKart.API.Strategies
{
    public class UpiPayment : IPaymentStrategy
    {
        public async Task<PaymentResult> ProcessPayment(decimal amount)
        {
            if (amount < 1)
            {
                return new PaymentResult
                {
                    IsSuccess = false,
                    Message = "UPI payment must be at least ₹1"
                };
            }

            var randomId = Guid.NewGuid().ToString("N")[..8];

            return new PaymentResult
            {
                IsSuccess = true,
                TransactionId = $"UPI-TXN-{randomId}",
                PaymentMethod = "UPI",
                Message = "Payment Successful"
            };
        }
    }
}
