using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExTools.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDbConnection _connection;

        public UserController(IDbConnection connection)
        {
            _connection = connection;
        }

        [HttpGet("GetScode")]
        public async Task<ActionResult> GetScode(string username)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Username", username);

            var result = await _connection.QueryAsync<string>("Tools_User_GetScode", parameters, commandType: CommandType.StoredProcedure);
            return Ok($"{result.FirstOrDefault()}");
        }
    }
}
