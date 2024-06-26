using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimplifiedPicPay.Dtos;
using SimplifiedPicPay.Models;
using SimplifiedPicPay.Services;

namespace SimplifiedPicPay.Controllers;

/// <summary>
/// Controller for authentication-related actions.
/// </summary>
[Controller]
[Route("v1/matheuspay")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    private readonly IMapper _mapper;
    public AuthController(UserManager<User> userManager, IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Registers a new user.
    /// </summary>
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<UserRegisterResponseDto>> SignUp([FromBody] UserRegisterRequestDto userRegisterRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }

        var userCreated = await _authService.Register(userRegisterRequestDto);
        var responseDto = _mapper.Map<UserRegisterResponseDto>(userCreated);
        
        return CreatedAtAction(nameof(SignUp), "register", responseDto);
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<UserLoginResponseDto>> LogIn([FromBody] UserLoginRequestDto userLoginRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }

        var userLogged = await _authService.LogIn(userLoginRequestDto);

        return CreatedAtAction(nameof(LogIn), "login", userLogged);
    }

    /// <summary>
    /// Creates a new role. [Admin Only]
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "Admin")]
    [Route("create-role")]
    public async Task<ActionResult> CreateRole([FromBody] CreateRoleRequestDto createRole)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }
        
        var response = await _authService.CreateRole(createRole.RoleName);

        return CreatedAtAction(nameof(CreateRole), "create-role", response);

    }

    /// <summary>
    /// Assigns a role to a user. [Admin Only]
    /// </summary>
    [HttpPost]
    [Authorize(Policy = "Admin")]
    [Route("assign-role")]
    public async Task<ActionResult<AssignRoleResponseDto>> AssignRole([FromBody] AssignRoleRequestDto assignRoleRequestDto)
    {
        if (!ModelState.IsValid)
        {
            var errorMessage = ModelState.FirstOrDefault().Value?.Errors.FirstOrDefault()?.ErrorMessage;
            throw new BadHttpRequestException(errorMessage);
        }
        
        var response = await _authService.AssignRole(assignRoleRequestDto);

        return CreatedAtAction(nameof(AssignRole), "assign-role", response);
    }
    
    
    
}