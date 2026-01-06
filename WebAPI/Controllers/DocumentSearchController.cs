using DocumentSearch;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentSearchController(ILogger<DocumentSearchController> logger, ISearch search) : ControllerBase
    {

        [HttpGet(Name = "DocumentsSeacrh")]
        public async Task<string> SearchAsync([FromQuery] string searchText)
        {
            var result = await search.Process(searchText);

            return result;
        }
    }
}
