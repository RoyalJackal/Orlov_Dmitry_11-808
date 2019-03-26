using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContrLinq
{
    public static class Task3
    {
        public static Tuple<string, int, int>[] Solve(Product[] products, Discount[] discounts, Price[] prices, PurchaseData[] purchases)
        {
            return purchases
                .GroupBy(purchase => Tuple.Create(
                    //отсеиваем продукты с ненужным артиклес, должен остаться один, берём его страну
                    products.Where(product => product.Article == purchase.Article).First().Country,
                    purchase.CustomerData))
                .Select(group => Tuple.Create(
                    group.Key.Item1,
                    group.Key.Item2,
                    group.Sum(purchase =>
                    {
                        //отсеиваем нужную скидку и цену
                        var purchaseDiscount = discounts.Where(discount => discount.CustomerCode == purchase.CustomerData &&
                            discount.ShopName == purchase.ShopName).First().Value;
                        var purchasePrice = prices.Where(price => price.Article == purchase.Article &&
                            price.ShopName == purchase.ShopName).First().Value;
                        //считаем по формуле
                        return purchasePrice - purchasePrice * purchaseDiscount / 100;
                    })))
                .OrderBy(tuple => Tuple.Create(tuple.Item1, tuple.Item2))
                .ToArray();
        }
    }

    public class Customer
    {
        public readonly int CustomerCode;
        public readonly int BirthYear;
        public readonly string LivingLocation;

        public Customer(int code, int birthYear, string livingLocation)
        {
            CustomerCode = code;
            BirthYear = birthYear;
            LivingLocation = livingLocation;
        }
    }

    public class Product
    {
        public readonly string Article;
        public readonly string Category;
        public readonly string Country;

        public Product(string article, string category, string country)
        {
            Article = article;
            Category = category;
            Country = country;
        }
    }

    public class Discount
    {
        public readonly int CustomerCode;
        public readonly string ShopName;
        public readonly int Value;

        public Discount(int customerCode, string shopName, int value)
        {
            CustomerCode = customerCode;
            ShopName = shopName;
            Value = value;
        }
    }

    public class Price
    {
        public readonly string Article;
        public readonly string ShopName;
        public readonly int Value;

        public Price(string article, string shopName, int value)
        {
            Article = article;
            ShopName = shopName;
            Value = value;
        }
    }

    public class PurchaseData
    {
        public readonly int CustomerData;
        public readonly string Article;
        public readonly string ShopName;

        public PurchaseData(int customerData, string article, string shopName)
        {
            CustomerData = customerData;
            Article = article;
            ShopName = shopName;
        }
    }
}
