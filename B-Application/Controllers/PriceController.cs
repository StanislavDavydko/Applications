using Application.DomainModel.Services;
using Microsoft.AspNetCore.Mvc;
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

            return JsonSerializer.Serialize(price);
        }
    }
}
