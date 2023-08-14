using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusPractice.Domain.Models;
using OtusPractice.WebApiCore.Models;
using OtusPractice.WebApiCore.Services;
using System.Threading.Tasks;

namespace OtusPractice.WebApiCore.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public UserController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Получиь анкету пользователя по его id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("user/get/{id}")]
        public async Task<ActionResult<int>> GetById(string id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }


        /// <summary>
        /// Аутентифицировать пользователя
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("user/register")]
        public async Task<ActionResult<UserId>> Login(User userRegister)
        {
            if (string.IsNullOrWhiteSpace(userRegister.Login)
                || string.IsNullOrWhiteSpace(userRegister.Password)) 
                return BadRequest();

            var user_id = await _userService.Register(userRegister);
            if (string.IsNullOrWhiteSpace(user_id)) 
                return BadRequest("ошибка регистрации");

            return Ok(new UserId(user_id));
        }


        /// <summary>
        /// Аутентифицировать пользователя
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(LoginRequest loginRequest)
        {
            if (!loginRequest.IsValid()) return BadRequest();

            var user = await _userService.GetByLoginAsync(loginRequest.UserLogin);
            if (user == null) return NotFound();

            if (loginRequest.IsChecked(user))
                return Ok(new UserToken(_jwtService.GenerateSecurityToken(user.Login)));

            return BadRequest("Wrong cred"); 

        }
    }
}
