using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync(string to, string message);
    }
}
