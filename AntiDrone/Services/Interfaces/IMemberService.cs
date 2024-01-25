using AntiDrone.Data;
using AntiDrone.Models.Systems.Member;

namespace AntiDrone.Services.Interfaces;

public interface IMemberService
{
    Task<object> LoginCheck(LoginModel loginModel, AntiDroneContext context);
}