using System.Collections.Generic;

using API.Interfaces;
using Common.Models;

using Cysharp.Threading.Tasks;

namespace API.Mock
{
    public class PurchaseAPI : IPurchaseAPI
    {
        public UniTask Init()
        {
            return UniTask.CompletedTask;
        }

        public bool IsPaymentsAvailable()
        {
            return true;
        }

        public UniTask<List<Product>> GetProducts(List<string> productIds = null)
        {
            var utcs = new UniTaskCompletionSource<List<Product>>();
            UniTask.Delay(2000).ContinueWith(() => utcs.TrySetResult(MockData.Products)).Forget();
            return utcs.Task;
        }

        public UniTask<Purchase> PurchaseProduct(string productId)
        {
            var utcs = new UniTaskCompletionSource<Purchase>();
            void CompletePurchase(Purchase purchase)
            {
                UniTask.Delay(2000).ContinueWith(() => utcs.TrySetResult(purchase)).Forget();
            }
            CompletePurchase(new Purchase("1", productId));

            return utcs.Task;
        }

        public UniTask<bool> ConsumePurchase(string purchaseId)
        {
            var utcs = new UniTaskCompletionSource<bool>();
            void CompleteConsume(bool result)
            {
                UniTask.Delay(2000).ContinueWith(() => utcs.TrySetResult(result)).Forget();
            }
            CompleteConsume(true);

            return utcs.Task;
        }

        public UniTask<List<Purchase>> GetPurchases()
        {
            var utcs = new UniTaskCompletionSource<List<Purchase>>();
            UniTask.Delay(2000).ContinueWith(() => utcs.TrySetResult(new())).Forget();
            return utcs.Task;
        }
    }
}
