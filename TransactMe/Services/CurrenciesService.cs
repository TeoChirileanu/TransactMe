using System.Collections.Generic;
using System.Linq;
using TransactMe.Models;

namespace TransactMe.Services
{
    public class CurrenciesService
    {
        public static List<MyCurrency> Currencies { get; set; } =
            new List<MyCurrency>
            {
                new MyCurrency
                {
                    Name = "American Dollar (USD)",
                    Abbreviation = "USD",
                    PurchaseRate = 3.9,
                    SaleRate = 4.1
                },
                new MyCurrency
                {
                    Name = "Euro (EUR)",
                    Abbreviation = "EUR",
                    PurchaseRate = 4.5,
                    SaleRate = 4.7
                },
                new MyCurrency
                {
                    Name = "Great Britan Pound (GBP)",
                    Abbreviation = "GBP",
                    PurchaseRate = 5.1,
                    SaleRate = 5.3
                }
            };

        public double GetPurchaseRate(string abbreviation)
        {
            return Currencies.FirstOrDefault(x => x.Abbreviation == abbreviation).PurchaseRate;
        }

        public double GetSaleRate(string abbreviation)
        {
            return Currencies.FirstOrDefault(x => x.Abbreviation == abbreviation).SaleRate;
        }
    }
}