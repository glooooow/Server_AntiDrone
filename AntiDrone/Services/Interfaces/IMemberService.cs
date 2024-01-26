using AntiDrone.Data;
using AntiDrone.Models.Systems.Member;

namespace AntiDrone.Services.Interfaces;

public interface IMemberService
{
    Task<object> LoginCheck(LoginModel loginModel, AntiDroneContext context);
    Task<object> GetMemberInfo(long id, AntiDroneContext context);
    Task<object> DeleteMember(long id, AntiDroneContext context);
    Task<object> Register(Member member, AntiDroneContext context);
}