using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Utility
{
    public class BaseResponse : BaseApiController
    {
        protected IActionResult Error(object errorMessage)
        {
            return new BadRequestObjectResult(Envelope.ErrorMessage(errorMessage));
        }
    }
}
