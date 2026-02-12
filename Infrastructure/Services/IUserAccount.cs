using Domain;
using Infrastructure.DTOs;
using Infrastructure.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public interface IUserAccount
    {
        Task<GeneralResponse> CreateAsync(Register user);
        Task<LoginResponse> SignInAsync(Domain.Login user);
        Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
    }
}
