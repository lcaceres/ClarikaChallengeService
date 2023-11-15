using ClarikaChallengeService.Application.Interfaces;
using ClarikaChallengeService.Application.Models;
using ClarikaChallengeService.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace ClarikaChallengeService.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserDto userDto, [FromQuery] string password)
        {
            try
            {
                UserDto addedUser = userService.Add(userDto, password);
                return Ok(addedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetById(int userId)
        {
            UserDto userDto = userService.GetById(userId);

            if (userDto != null)
            {
                return Ok(userDto);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GetAll(int page, int pageSize, int? age, int? countryId)
        {
            List<UserDto> userDtos = userService.GetAll(page, pageSize, age, countryId);

            return Ok(userDtos);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequest)
        {
            var userDto = userService.ValidateLogin(loginRequest.UserName, loginRequest.Password);

            if (userDto == null)
            {
                return Unauthorized("Invalid credentials");
            }

            return Ok(userDto);
        }

        [HttpPost("generateRandomUsers")]
        public IActionResult GenerateRandomUsers()
        {
            try
            {
                userService.GenerateRandomUsers(1000);
                return Ok("Random users generated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
