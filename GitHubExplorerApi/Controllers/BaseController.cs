using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GitHubExplorerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {     
        protected int? GetIdFromToken()
        {
            string userId = ((ClaimsIdentity)HttpContext.User.Identity).FindFirst("accountId").Value;
            if (string.IsNullOrEmpty(userId))
                return null;
            int.TryParse(userId, out int intId);
            return intId;
        }
    }
}
