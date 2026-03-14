namespace ShopKart.API.Strategies
{
    public interface IPaymentStrategyResolver
    {
        IPaymentStrategy GetStrategy(string paymentMethod);
    }
    public class PaymentStrategyResolver : IPaymentStrategyResolver
    {
        private readonly Dictionary<string, IPaymentStrategy> _strategies;
        public PaymentStrategyResolver(CreditCardPayment creditCard, UpiPayment upi, CodPayment cod)
        {
            _strategies = new(StringComparer.OrdinalIgnoreCase)
            {
                {"CreditCard", creditCard},
                {"UPI", upi },
                {"COD", cod }
            };
        }
        public IPaymentStrategy GetStrategy(string paymentMethod)
        {
            if (!_strategies.TryGetValue(paymentMethod, out var strategy))
            {
                throw new NotSupportedException($"Payment method {paymentMethod} is not supported.");
            }
            return strategy;
        }
    }
}
