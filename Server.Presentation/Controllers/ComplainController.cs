using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/controller")]
    [Authorize]
    public class ComplainController : ControllerBase
    {
        public ComplainController()
        {

        }


    }
}