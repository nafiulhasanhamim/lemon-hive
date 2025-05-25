using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Controllers;
using dotnet_mvc.DTOs;
using dotnet_mvc.Interfaces;
using dotnet_mvc.Models;
using dotnet_mvc.Models.Authentication.Login;
using dotnet_mvc.Models.Authentication.User;
using dotnet_mvc.RabbitMQ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using User.Management.Service.Models.Authentication.SignUp;

namespace dotnet_mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUserManagement _user;
        private readonly IRabbmitMQCartMessageSender _messageBus;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            IEmailService emailService, IUserManagement user,
            IRabbmitMQCartMessageSender messageBus
            )
        {
            _userManager = userManager;
            _user = user;
            _emailService = emailService;
            _messageBus = messageBus;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var tokenResponse = await _user.CreateUserWithTokenAsync(registerUser);
            Console.WriteLine(tokenResponse);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { tokenResponse.Response!.Token, email = registerUser.Email }, Request.Scheme);
            _messageBus.SendMessage(new { To = registerUser.Email, Subject = "Confirmation email link", Content = confirmationLink! }, "sentEmail", "queue");
            return ApiResponse.Success($"User created & Email Sent to {registerUser.Email} SuccessFully");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return ApiResponse.Success("Email Verified Successfully");
                }
            }
            return ApiResponse.BadRequest("This User Doesnot exist!");
        }

    }
}