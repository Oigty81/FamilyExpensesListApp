using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/utility")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        public UtilityController()
        {
        }

        [HttpGet]
        [Route("quit")]
        public async Task<ActionResult<bool>> Quit()
        {
            //CommunicationServices.SystemUtilitiesInstance.Quit();
            return true;
        }

        [HttpGet]
        [Route("minimize")]
        public async Task<ActionResult<bool>> Minimize()
        {
            ////CommunicationServices.SystemUtilitiesInstance.MinimizeApplication();
            return true;
        }

        [HttpGet]
        [Route("batterystate")]
        public async Task<ActionResult<int>> BatteryState()
        {
            int batteryStateInt = 50; ////CommunicationServices.SystemUtilitiesInstance.GetBatteryState();

            return batteryStateInt;
        }
    }
}
