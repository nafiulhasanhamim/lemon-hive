using System.ComponentModel.DataAnnotations;
using MimeKit;

namespace dotnet_mvc.DTOs
{
    public class MailDto
    {
        public string To { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
