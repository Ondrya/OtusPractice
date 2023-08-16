using Microsoft.AspNetCore.Mvc;
using OtusPractice.Domain.Models;
using OtusPractice.WebApiCore.Services;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OtusPractice.WebApiCore.Controllers
{
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly Generator _generator;

        public TestController(IUserService userService)
        {
            _userService = userService;
            _generator = new Generator();
        }

        /// <summary>
        /// Создать пользователей
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("test/generate/users/{count}")]
        public async Task<ActionResult<int>> CreateTestUsers(int count)
        {
            if (count <= 0)
                return BadRequest("Неверный параметр, должен быть больше 0");

            var createdCounter = 0;
            var partition = 100;

            while (count > 0)
            {
                var forCreate = partition > count ? count : partition;

                var users = await _generator.CreateUsers(forCreate);
                if (users != null && users.Any())
                {
                    Parallel.ForEach(users, async user =>
                    {
                        try
                        {
                            var res = await _userService.Register(user); //id будет переопределён
                            if (!string.IsNullOrWhiteSpace(res))
                                Interlocked.Increment(ref createdCounter);
                        }
                        catch (Exception ex)
                        {
                            await Console.Out.WriteLineAsync(ex.Message);
                        }
                    });

                    
                }

                count -= partition;
            }

            return Ok(createdCounter);
        }
    }
}
