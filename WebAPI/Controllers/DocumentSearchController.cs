using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentSearchController : ControllerBase
    {
        private readonly ILogger<DocumentSearchController> _logger;

        public DocumentSearchController(ILogger<DocumentSearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "DocumentsSeacrh")]
        public async Task<List<string>> Get()
        {
            return [];
        }
    }
}
