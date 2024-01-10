using System;

namespace Application.DomainModel
{
    public class PriceInformation
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
