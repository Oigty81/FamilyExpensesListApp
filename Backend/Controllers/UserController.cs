namespace Backend.Controllers
{
    using Backend;
    using Backend.DataServices;
    using Microsoft.AspNetCore.Mvc;
    using System.IdentityModel.Tokens.Jwt;

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICategoryDataService categoryDataService;

        public UserController(CategoryDataService categoryDataService)
        {
            this.categoryDataService = categoryDataService;
        }

        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult<bool>> Auth()
        {
            ////TODO: implement it
            return true;
        }
    }
}
