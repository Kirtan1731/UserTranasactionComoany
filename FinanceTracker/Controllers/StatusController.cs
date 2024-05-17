using FinanceTracker.BLL.Interface;
using FinanceTracker.Utility;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [ApiController]
    public class StatusController : BaseResponse
    {
        private readonly IStatusServices _statusServices;
        private readonly ILogger<StatusController> _logger;
        public StatusController(IStatusServices statusServices,ILogger<StatusController> logger)
        {
            _statusServices = statusServices;
            _logger = logger;
        }

        [Route("status")]
        [HttpGet]
        public async Task<IActionResult> GetUserStatus()
        {
            try{
            var response = await _statusServices.GetStatuses();
            return Ok(response);
            }catch(Exception ex){
                _logger.LogError(ex.ToString());
                return Error(ex.Message);
            }
            
        }
    }
}
