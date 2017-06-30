using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TransactMe.Services
{
    public class SelectListItemService
    {
        public SelectListItemService()
        {
            Currencies = new List<SelectListItem>();

            var currencies = CurrenciesService.Currencies;
            foreach (var currency in currencies)
                Currencies.Add(new SelectListItem {Text = currency.Name, Value = currency.Abbreviation});

            TransactionTypes = new List<SelectListItem>
            {
                new SelectListItem {Text = "Sale", Value = "Sale", Selected = true},
                new SelectListItem {Text = "Purchase", Value = "Purchase"}
            };
        }

        public List<SelectListItem> Currencies { get; set; }
        public List<SelectListItem> TransactionTypes { get; set; }
    }
}