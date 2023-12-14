namespace Backend.Controllers
{
    using Backend;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("getCategories")]
        public async Task<ActionResult<bool>> GetCategories()
        {
            return true;
        }

        [HttpPost]
        [Route("putCategory")]
        public async Task<ActionResult<bool>> PutCategory()
        {
            return true;
        }

        [HttpPost]
        [Route("putCategoryComposition")]
        public async Task<ActionResult<bool>> PutCategoryComposition()
        {
            return true;
        }
    }
}
