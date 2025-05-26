using System.ComponentModel.DataAnnotations;
using MimeKit;

namespace MyApp.Application.DTOs
{
    public class MailDto
    {
        public string To { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
