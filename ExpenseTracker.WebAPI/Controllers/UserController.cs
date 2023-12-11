using ExpenseTracker.Core.DTOs.UserDto;
using ExpenseTracker.Core.IUC;
using ExpenseTracker.WebAPI.Extensions;
using ExpenseTracker.WebAPI.Utils;
using ExpenseTracker.WebAPI.Utils.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.WebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _service;
        private readonly ILogger<IUser> _logger;

        public UserController(IUser service, ILogger<IUser> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("login")]
        // PostLogin(string userName, string password)
        public IActionResult PostLogin(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessagesDicLst());

                var result = _service.Login(loginDto.userName, loginDto.password);
                return ActionResultDRY.Instance.GetAction(result, OperationTypeResponse.add);
            }
            catch (Exception ex)
            {
                //return BadRequest($"Exception : {ex.Message}");
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.add);
            }
        }

        [HttpPost("signUp")]
        public IActionResult PostSignUp(UserRequestDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.GetErrorMessagesDicLst());
                var result = _service.SignUp(request);
                return ActionResultDRY.Instance.GetAction(result, OperationTypeResponse.add);
            }
            catch (Exception ex)
            {
                return ActionResultDRY.Instance.GetActionException(ex, OperationTypeResponse.add);
            }
        }

    }
}
