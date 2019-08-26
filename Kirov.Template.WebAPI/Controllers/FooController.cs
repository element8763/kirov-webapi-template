using BLL.Template.Services;
using Microsoft.AspNetCore.Mvc;
using NAutowired.Core.Attributes;

namespace WebAPI.Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooController : BaseController
    {
        [Autowired]
        private readonly FooService fooService;

    }
}