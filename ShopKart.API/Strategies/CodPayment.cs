namespace ShopKart.API.Strategies
{
    public class CodPayment : IPaymentStrategy
    {
        public async Task<PaymentResult> ProcessPayment(decimal amount)
        {
            if (amount > 5000)
            {
                return new PaymentResult
                {
                    IsSuccess = false,
                    Message = "Cash on Delivery is not allowed on amount more than ₹5000"
                };
            }

            var randomId = Guid.NewGuid().ToString("N")[..8];

            return new PaymentResult
            {
                IsSuccess = true,
                TransactionId = $"COD-ORD-{randomId}",
                PaymentMethod = "COD",
                Message = "Payment Successful"
            };
        }
    }
}
