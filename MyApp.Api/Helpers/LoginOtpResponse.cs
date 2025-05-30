using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyApp.Domain.Entities;

namespace MyApp.Api.Helpers
{
    public class LoginOtpResponse
    {
        public string Token { get; set; } = null!;
        public bool IsTwoFactorEnable { get; set; }
        // public IdentityUser User { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
}