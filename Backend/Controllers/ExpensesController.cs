namespace Backend.Controllers
{
    using Backend;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/expenses")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpGet]
        [Route("getExpensesPeriod")]
        public async Task<ActionResult<bool>> ExpensesPeriod()
        {
            return true;
        }

        [HttpPost]
        [Route("putExpenses")]
        public async Task<ActionResult<bool>> PutExpenses()
        {
            return true;
        }
    }
}
