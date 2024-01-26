using Microsoft.AspNetCore.Mvc;
using MultiTenant.Middleware.Api.Abstractions;
using MultiTenant.Middleware.Api.Repository;

namespace MultiTenant.Middleware.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly MultiTenantContext context;

        public HomeController(MultiTenantContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAsync()
        {
            return Ok(context.Products.ToList());
        }
    }
}
