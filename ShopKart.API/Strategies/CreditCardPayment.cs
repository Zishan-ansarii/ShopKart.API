namespace ShopKart.API.Strategies
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public async Task<PaymentResult> ProcessPayment(decimal amount)
        {
            if (amount < 100)
            {
                return new PaymentResult
                {
                    IsSuccess = false,
                    Message = "Credit Card payment must be at least ₹100"
                };
            }

            var randomId = Guid.NewGuid().ToString("N")[..8];

            return new PaymentResult
            {
                IsSuccess = true,
                TransactionId = $"CC-TXN-{randomId}",
                PaymentMethod = "Credit Card",
                Message = "Payment Successful"
            };
        }
    }
}
