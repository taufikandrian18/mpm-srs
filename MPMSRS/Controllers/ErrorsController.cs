using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MPMSRS.Models.VM;
using MPMSRS.Services.Repositories.Command;

namespace MPMSRS.Controllers
{
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly HttpStatusException _httpException;

        public ErrorsController(HttpStatusException httpService)
        {
            _httpException = httpService;
        }

        [Route("error")]
        public MyErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error; // Your exception

            return new MyErrorResponse(context,exception); // Your error model
        }
    }
}
