using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Domain.Entities;

namespace MyApp.Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);

    }
}