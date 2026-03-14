namespace ShopKart.API.Strategies
{
    public class PaymentResult
    {
        public string PaymentMethod { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
