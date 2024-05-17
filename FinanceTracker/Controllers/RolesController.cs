using FinanceTracker.BLL.Interface;
using FinanceTracker.Utility;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers
{
    [ApiController]
    public class RolesController : BaseResponse
    {
        private readonly IRoleService _roleServices;
        private readonly ILogger<RolesController> _logger;
        public RolesController(IRoleService roleServices,ILogger<RolesController> logger)
        {
            _roleServices = roleServices;
            _logger = logger;
        }

        [Route("roles")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _roleServices.GetRoles();
                return Ok(response);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Error(ex.Message);
            }


        }
    }
}

