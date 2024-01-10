using System;

namespace Application.DomainModel
{
    public class PriceData
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
