using System.Collections.Generic;

using Common.Models;

using Cysharp.Threading.Tasks;

namespace API.Interfaces
{
    public interface IPurchaseAPI
    {
        UniTask Init();
        bool IsPaymentsAvailable();
        UniTask<List<Product>> GetProducts(List<string> productIds);
        UniTask<Purchase> PurchaseProduct(string productId);
        UniTask<bool> ConsumePurchase(string purchaseId);
        UniTask<List<Purchase>> GetPurchases();
    }
}
