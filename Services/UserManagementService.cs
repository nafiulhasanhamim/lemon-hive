using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using api.Controllers;
using dotnet_mvc.DTOs;
using dotnet_mvc.Interfaces;
using dotnet_mvc.Models;
using dotnet_mvc.Models.Authentication.Login;
using dotnet_mvc.Models.Authentication.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using User.Management.Service.Models;
using User.Management.Service.Models.Authentication.SignUp;

public class UserManagementService : IUserManagement
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    public UserManagementService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    public async Task<ApiResponseUser<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUser registerUser)
    {
        // Check if the user already exists
        //Check User Exist 
        if (registerUser.Password != registerUser.ConfirmPassword)
        {
            return new ApiResponseUser<CreateUserResponse> { IsSuccess = false, StatusCode = 403, Message = "Password Mismatch!" };
        }
        var userExist = await _userManager.FindByEmailAsync(registerUser.Email!);
        var userNameExist = await _userManager.FindByNameAsync(registerUser.Username!);

        if (userExist != null)
        {
            return new ApiResponseUser<CreateUserResponse> { IsSuccess = false, StatusCode = 403, Message = "User already exists!" };
        }
        else if (userNameExist != null)
        {
            return new ApiResponseUser<CreateUserResponse> { IsSuccess = false, StatusCode = 403, Message = "User with this userName already exists!" };
        }
        //Add the User in the database
        ApplicationUser user = new()
        {
            Email = registerUser.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerUser.Username,
            TwoFactorEnabled = false,
            CreatedAt = DateTime.UtcNow

        };

        if (await _roleManager.RoleExistsAsync(registerUser.Role!))
        {
            var result = await _userManager.CreateAsync(user, registerUser.Password!);
            foreach (var error in result.Errors)
            {
                Console.WriteLine("after entering errors");

                if (error.Code.Contains("Password"))
                {
                    var message = $"Weak password: {error.Description}";
                    return new ApiResponseUser<CreateUserResponse> { IsSuccess = false, StatusCode = 403, Message = message };
                }
            }
            if (!result.Succeeded)
            {
                return new ApiResponseUser<CreateUserResponse> { IsSuccess = false, StatusCode = 403, Message = "User failed to create" };
            }
            //Add role to the user....
            await _userManager.AddToRoleAsync(user, registerUser.Role!);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return new ApiResponseUser<CreateUserResponse> { IsSuccess = true, StatusCode = 201, Message = "User is created", Response = new CreateUserResponse() { Token = token } };
        }
        else
        {
            return new ApiResponseUser<CreateUserResponse> { IsSuccess = false, StatusCode = 403, Message = "This role doesnot exists" };
        }

    }

}