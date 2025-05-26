
using MyApp.Api.Helpers;
using MyApp.Application.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface IUserManagement
    {
        Task<ApiResponseUser<CreateUserResponse>> CreateUserWithTokenAsync(RegisterUser registerUser);
        Task<ApiResponseUser<LoginOtpResponse>> GetOtpByLoginAsync(LoginModel loginModel);
        Task<ApiResponseUser<LoginResponse>> LoginUserWithJWTokenAsync(string otp, string userName);
        Task<ApiResponseUser<LoginResponse>> GetJwtTokenAsync(ApplicationUser user);
        Task<ApiResponseUser<LoginResponse>> RenewAccessTokenAsync(LoginResponse tokens);



    }
}
