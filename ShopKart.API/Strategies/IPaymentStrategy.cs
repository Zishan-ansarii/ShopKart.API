namespace ShopKart.API.Strategies
{
    public interface IPaymentStrategy
    {
        Task<PaymentResult> ProcessPayment(decimal amount);
    }
}
