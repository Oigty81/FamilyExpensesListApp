namespace Backend.Controllers
{
    using Backend;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult<Dictionary<string, string>>> Auth()
        {
            var response = new Dictionary<string, string>();
            response["token"] = "AAAABBBBB";
            return response;
        }
    }
}
