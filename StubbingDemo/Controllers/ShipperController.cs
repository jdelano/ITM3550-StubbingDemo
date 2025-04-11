using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StubbingDemo.Services;

namespace StubbingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShipperController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_shipperService.GetShippers());
        }
    }
}
