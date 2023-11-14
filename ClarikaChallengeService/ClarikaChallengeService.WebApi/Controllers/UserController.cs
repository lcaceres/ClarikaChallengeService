using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using Microsoft.AspNetCore.Mvc;


namespace ClarikaChallengeService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<UserResult> Get()
        {
            return null;
        }
    }
}
