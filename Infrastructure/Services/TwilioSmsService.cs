using Application.Interfaces;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;
using Twilio.Types;


namespace Infrastructure.Services
{
    public sealed class TwilioSmsService : ITwilioService
    {
        private readonly TwilioSettings _settings;

        public TwilioSmsService(IOptions<TwilioSettings> settings)
        {
            _settings = settings.Value;
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
        }

        //public async Task SendAppointmentConfirmationAsync(string toPhoneNumber, string patientName, string doctorName, DateTime appointmentDate, TimeSpan startTime, string message)
        //{
           
        //}

        public async Task SendAppointmentConfirmationAsync(string phoneNumber, string message)
        {
            await MessageResource.CreateAsync(
                 body: message,
                 from: new PhoneNumber(_settings.FromNumber),
                 to: new PhoneNumber(phoneNumber)

             );
        }

        //public async Task SendSmsAsync(string to, string message)
        //{
        //    await MessageResource.CreateAsync(
        //        body: message,
        //        from: new PhoneNumber(_settings.FromNumber),
        //        to: new PhoneNumber(to)
        //    );
        //}
    }
}
