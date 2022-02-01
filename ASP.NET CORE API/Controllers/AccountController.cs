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
        [SwaggerResponse(200, "Success", typeof(ServiceResponse<List<AccountModel>>)),SwaggerOperation(Summary="For Selection")]
        [SwaggerResponse(422,"Unproccessable Entity",typeof(ServiceResponse<List<AccountModel>>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ServiceResponse<List<AccountModel>>))]
        [SwaggerResponse(400, "Bad Request", typeof(ServiceResponse<List<AccountModel>>))]
        public async Task<IActionResult> SelectAllUser()
        {
            var res = await _repo.account.selectALLregister();
            if (res.code.Equals(200)) { return Ok(res); } else if (res.code.Equals(500)) { return BadRequest(res); } else { return UnprocessableEntity(res); }
        }
        [HttpPost]
        [Route("GetUserLogin")]
        [SwaggerResponse(200, "Success", typeof(ServiceResponse<AccountModel>))]
        [SwaggerResponse(422, "Unproccessable Entity", typeof(ServiceResponse<ModelStateError[]>))]
        [SwaggerResponse(500, "Internal Server Error", typeof(ServiceResponse<error>))]
        [SwaggerResponse(400, "Bad Request", typeof(ServiceResponse<error>))]
        public async Task<IActionResult> SelectAllUser([FromBody]Credential credential)
        {
            if (!ModelState.IsValid)
            {
                var reqError = new ServiceResponse<object>();
                var allErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e=> new { details = e.ErrorMessage });
                reqError.code = 422;
                reqError.message = "Unprocessable Entity Error!";
                reqError.Data = allErrors;
                return UnprocessableEntity(reqError);
            }
            var res = await _repo.account.getUserLogin(credential);
            if (res.code.Equals(200)) { return Ok(res); } else if (res.code.Equals(500)) { return BadRequest(res); } else { return UnprocessableEntity(res); } 
        }
    }
}
