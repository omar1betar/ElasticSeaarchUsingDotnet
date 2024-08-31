using ElasticSeaarchUsingDotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSeaarchUsingDotnet.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IElasticService _elasticService;

        public UsersController(IElasticService elasticService)
        {
            _elasticService = elasticService;
        }

        [HttpPost("create-index")]
        public async Task<IActionResult> CreateIndex(string indexName)
        {
            await _elasticService.CreateIndexifNotExistAsync(indexName);
            return Ok($"index {indexName} created or already exists");
        }

        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser([FromBody] Models.User user)
        {
            var result = await _elasticService.AddOrUpdate(user);
            return result ? Ok("User Added Or Updated") : StatusCode(500, "Error");
        }

        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser([FromBody] Models.User user)
        {
            var result = await _elasticService.AddOrUpdate(user);
            return result ? Ok("User Added Or Updated") : StatusCode(500, "Error");
        }

        [HttpGet("get-user/{key}")]
        public async Task<IActionResult> GetUser(string key)
        {
            var user = await _elasticService.Get(key);
            return user != null ? Ok(user) : NotFound("User Not Found");
        }
        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _elasticService.GetAll();
            return users != null ? Ok(users) : NotFound("Users Not Found");
        }
        [HttpDelete("delete-user/{key}")]
        public async Task<IActionResult> DeleteUser(string key)
        {
            var result = await _elasticService.Remove(key);
            return result ? Ok("User Deleted") : StatusCode(500, "Error");

        }








    }
}
