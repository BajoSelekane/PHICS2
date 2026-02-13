using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ITwilioService
    {
        Task SendAppointmentConfirmationAsync(
      string phoneNumber,
      string message);

    }
}
