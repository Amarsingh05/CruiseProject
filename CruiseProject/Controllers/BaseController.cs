using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CruiseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                var response = await action();
                return response;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new
                {
                    Success = false,
                    Message = $"An error occurred while processing your request: {ex.Message}",
                    StackTrace = ex.StackTrace
                })
                {
                    StatusCode = 500
                };
            }
        }

        protected abstract Task<IActionResult> ExecuteActionAsync(Func<Task<IActionResult>> action);
    }
}
