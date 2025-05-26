using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Domain.Entities
{
    public class TokenType
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiryTokenDate { get; set; }
    }
}