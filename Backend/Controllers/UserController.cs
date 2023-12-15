namespace Backend.Controllers
{
    using Backend.Dtos;
    using Microsoft.AspNetCore.Mvc;
    ////using System.IdentityModel.Tokens.Jwt;

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult<TokenDto>> Auth()
        {
            ////TODO: implement it and remove dummy token
            string dummyJwt = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJpYXQiOjE3MDI1OTg3ODAsImp0aSI6Im1rdUtBWTdLRXBmWUkxQ3lNSFgza0E9PSIsImlzcyI6InlvdXIuZG9tYWluLm5hbWUiLCJuYmYiOjE3MDI1OTg3ODAsImV4cCI6MTcwMjU5OTM4MCwiZGF0YSI6eyJ1c2VybmFtZSI6Im1heDEiLCJ1c2VySWQiOjEsImRpc3BsYXluYW1lIjoiTWF4IE11c3RlciJ9fQ.GM1W4Cs4ig4LENMfxY20IfrpE2jV9be_N8jVpY9SQIbJrmtMQlx45jMFhwS3xB_3r56BBYPKz5bOLUda5RWpr8pSu3iSgV3N2NhRXF9TWoNCf-yxM0uq_JLie872P9hg5hTI1dfm7H1t2weGfN9x8hRegOa2bf_yEgOs7LrooYsOclvgkchYMPlmtnzATrSFBk3GsNx3iOfIPkQDFck4i1pNq2Xi0vwRh9ANGRjnee4MD8LSEDSxzbAoaTBSLIYVfGYxfLfp8yYy-DV-DLtVuI0o4yNh7Oy9Y534Akq5_JgqOHlLsOyR8x8GVPPYVDSQ7rlwTE-bHq1T20GPpqP5kQ";

            return new TokenDto() { Token = dummyJwt };
        }
    }
}
