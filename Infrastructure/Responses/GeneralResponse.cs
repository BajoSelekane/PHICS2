using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Responses
{
    public record GeneralResponse(bool Flag, string Message = null!);
}
