using Application.DomainModel;
using System;

namespace B_Application.Web.Models
{
    public class PriceModel
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }

        public PriceModel() { }

        public PriceModel(PriceInformation price)
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
