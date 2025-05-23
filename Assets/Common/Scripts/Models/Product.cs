using UnityEngine;

namespace Common.Models
{
    public class Product
    {
        public string ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public float PriceValue { get; private set; }
        public string CurrencyIconUrl { get; private set; }
        public string FullPrice { get; private set; }
        public Sprite CurrencyIcon { get; private set; }

        public Product(
            string id,
            string name,
            string description,
            float priceValue,
            string currencyIconUrl,
            string fullPrice,
            Sprite currencyIcon = null
        )
        {
            ID = id;
            Name = name;
            Description = description;
            PriceValue = priceValue;
            CurrencyIconUrl = currencyIconUrl;
            FullPrice = fullPrice;
            CurrencyIcon = currencyIcon;
        }
    }
}
