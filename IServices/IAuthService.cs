using Microsoft.AspNetCore.Mvc;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.IServices
{
    public interface IAuthService
    {
        Task<AuthResult> Register(UserRegistrationRequestDTO requestDTO);
    }
}
