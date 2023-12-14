namespace Backend.Controllers
{
    using Backend;
    using Backend.DataServices;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryDataService categoryDataService;

        public CategoryController(CategoryDataService categoryDataService)
        {
            this.categoryDataService = categoryDataService;
        }

        [HttpGet]
        [Route("getCategories")]
        public async Task<ActionResult<bool>> GetCategories()
        {
            ////TODO: implement it
            return true;
        }

        [HttpPost]
        [Route("putCategory")]
        public async Task<ActionResult<bool>> PutCategory()
        {
            ////TODO: implement it
            return true;
        }

        [HttpPost]
        [Route("putCategoryComposition")]
        public async Task<ActionResult<bool>> PutCategoryComposition()
        {
            ////TODO: implement it
            return true;
        }
    }
}
