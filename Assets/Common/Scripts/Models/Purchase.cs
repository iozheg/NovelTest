namespace Common.Models
{
    public class Purchase
    {
        public string PurchaseId { get; }
        public string ProductId { get; }
        public string OrderId { get; }
        public string SubscriptionId { get; }

        public Purchase(
            string purchaseId,
            string productId = null,
            string orderId = null,
            string subscriptionId = null
        )
        {
            PurchaseId = purchaseId;
            ProductId = productId;
            OrderId = orderId;
            SubscriptionId = subscriptionId;
        }
    }
}