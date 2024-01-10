using Application.DomainModel.Services;
using B_Application.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace B_Application.Web.Controllers
{
    [ApiController]
    public class HomeController
    {
        private readonly IApplicationService _applicationService;

        public HomeController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public HttpStatusCode Get(ApplicationModel model)
        {
            var app = _applicationService.Add(model.CurrencyPair, model.Price, model.Timestamp);

            if(app.Status == TaskStatus.RanToCompletion)
            {
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}
