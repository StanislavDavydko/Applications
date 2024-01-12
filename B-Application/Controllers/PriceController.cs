using Application.DomainModel.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace B_Application.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        [HttpGet("action/{id}")]
        public string Get(int id)
        {
            var price = _priceService.Get(id);

            Console.WriteLine("The data is given");

            return JsonSerializer.Serialize(price);
        }
    }
}
