using System.Net;
using E_Ticaret.Application.Abstracts.Services;
using E_Ticaret.Application.DTOs.UserDtos;
using E_Ticaret.Application.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_Ticaret.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private IUserService _userService { get; }
    public AccountsController(IUserService userService)
    {
        _userService = userService;

    }
    // GET: api/<AccountsController>
    [HttpGet("test-values")]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<AccountsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<AccountsController>
    [HttpPost("register")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var result = await _userService.Register(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var result = await _userService.Login(dto);
        return StatusCode((int)result.StatusCode, result);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest dto)
    {
        var result = await _userService.RefreshTokenAsync(dto);
        return StatusCode((int)result.StatusCode, result);
    }


    [HttpGet("confirm-email")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string token)
    {
        var result = await _userService.ConfirmEmail(userId, token);
        return StatusCode((int)result.StatusCode, result);

    }
    // PUT api/<AccountsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<AccountsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

    [HttpPost("send-reset-password-email")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> SendResetPasswordEmail([FromBody] string email)
    {
        var result = await _userService.SendResetPasswordEmailAsync(email);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            return StatusCode((int)result.StatusCode, result);
        }
        return Ok(result.Message);
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordDto dto)
    {
        var result = await _userService.ResetPasswordAsync(dto);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            return StatusCode((int)result.StatusCode, result);
        }
        return Ok(result.Message);
    }
}
