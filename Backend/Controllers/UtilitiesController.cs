namespace Backend.Controllers
{
    using Backend;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/utilities")]
    [ApiController]
    public class UtilitiesController : ControllerBase
    {
        public UtilitiesController()
        {
        }

        [HttpGet]
        [Route("quit")]
        public async Task<ActionResult<bool>> Quit()
        {
            if(BackendMain.SystemUtilitiesInstance != null)
            {
               BackendMain.SystemUtilitiesInstance.QuitApplication();
            }

            return true;
        }

        [HttpGet]
        [Route("minimize")]
        public async Task<ActionResult<bool>> Minimize()
        {
            if(BackendMain.SystemUtilitiesInstance != null)
            {
                BackendMain.SystemUtilitiesInstance.MinimizeApplication();
            }

            return true;
        }

        [HttpGet]
        [Route("batterystate")]
        public async Task<ActionResult<int>> BatteryState()
        {
            int batteryStateInt = 0;

            if(BackendMain.SystemUtilitiesInstance != null)
            {
                batteryStateInt = BackendMain.SystemUtilitiesInstance.GetBatteryState();
            }

            return batteryStateInt;
        }
    }
}
