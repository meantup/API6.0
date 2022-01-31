using ASP.NET_CORE_API.Model;
using ASP.NET_CORE_API.Repository.IUnity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ASP.NET_CORE_API.Controllers
{
    [Route("webapi/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAdapterRepository _repo;
        public readonly object sec = new { first = "sasas", lastname = "sasa", middlename = "sasasa" }; 
        public AccountController(IAdapterRepository repo)
        {
            _repo = repo;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> SelectAllUser([FromBody] AccountModel acc)
        {
            var res = await _repo.account.register(acc);
            if (res.code.Equals(200)) { return Ok(res); } else if (res.code.Equals(500)) { return BadRequest(res); } else { return UnprocessableEntity(res); }

        }
        [HttpGet]
        [Route("SelectUser")]
        [SwaggerResponse(200, "Success", typeof(ServiceResponse<List<AccountModel>>))]
        [SwaggerResponse(422, "Model Validation Error", typeof(AccountModel))]
        [SwaggerResponse(500, "Internal Server Error", typeof(AccountModel))]
        [SwaggerResponse(400, "Bad Request", typeof(AccountModel))]
        public async Task<IActionResult> SelectAllUser()
        {
            var res = await _repo.account.selectALLregister();
            if (res.code.Equals(200)) { return Ok(res); } else if (res.code.Equals(500)) { return BadRequest(res); } else { return UnprocessableEntity(res); }
        }
        [HttpPost]
        [Route("GetUserLogin")]
        public async Task<IActionResult> SelectAllUser([FromBody]object credential)
        {
            var res = await _repo.account.getUserLogin(credential);
            if (res.code.Equals(200)) { return Ok(res); } else if (res.code.Equals(500)) { return BadRequest(res); } else { return UnprocessableEntity(res); }
        }
    }
}
