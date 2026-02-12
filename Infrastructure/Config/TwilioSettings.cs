using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Config
{
    public sealed class TwilioSettings
    {
        public string AccountSid { get; init; } = string.Empty;
        public string AuthToken { get; init; } = string.Empty;
        public string FromNumber { get; init; } = string.Empty;
    }
}
