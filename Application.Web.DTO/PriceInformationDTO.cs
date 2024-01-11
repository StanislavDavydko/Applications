using System;

namespace Application.Web.DTO
{
    public class PriceInformationDTO
    {
        public int Id { get; set; }
        public string CurrencyPair { get; set; }
        public double Price { get; set; }
        public DateTime Timestamp { get; set; }

        public PriceInformationDTO(DomainModel.PriceInformation priceInformation) 
        {
            Id = priceInformation.Id;
            CurrencyPair = priceInformation.CurrencyPair;
            Price = priceInformation.Price;
            Timestamp = priceInformation.Timestamp;
        }
    }
}
