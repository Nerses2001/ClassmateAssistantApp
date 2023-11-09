using BusinessLayer.IService;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ClassmateAssistantApp.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        public AccountsController(IApplicationUserService applicationUser)
        {
            this._userService = applicationUser;
        }

        /// <summary>
        /// Login to Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/Auth/Login")]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<UserManagerResponse>> LoginAsync([FromBody] LoginModel model) 
        {
            try
            {
                ActionResult<UserManagerResponse> result;

                var response = await _userService.LoginUserAsync(model);

                if (response.IsSuccess)
                    result = Ok(response);
                else
                    result = BadRequest(response);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Register New Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/Auth/Register")]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserManagerResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult<UserManagerResponse>> RegisterAsync([FromBody] RegisterModel model)
        {
            try
            {
                ActionResult<UserManagerResponse> result;

                if (!RegexUtilities.IsValidEmail(model.Email))
                {
                    ModelState.AddModelError("Email", "EmailInvalid");

                    result = UnprocessableEntity(ModelState.SelectMany(x => x.Value!.Errors).ToList());
                }
                else
                {
                    var response = await _userService.RegisterUserAsync(model);

                    if (response.IsSuccess)
                    {
                        result = Ok(response);
                    }
                    else
                        result = BadRequest(response);
                }

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
