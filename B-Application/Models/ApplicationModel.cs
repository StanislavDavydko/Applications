using Application.DomainModel;
using System;

namespace B_Application.Web.Models
{
    public class ApplicationModel
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }

        public ApplicationModel() { }

        public ApplicationModel(PriceData price)
        {
            if (price != null)
            {
                Id = price.Id;
                CurrencyPair = price.CurrencyPair;
                Price = price.Price;
                Timestamp = price.Timestamp;
            }
        }
    }
}
