using AntiDrone.Data;
using AntiDrone.Models.Systems.Member;

namespace AntiDrone.Services.Interfaces;

public interface IMemberService
{
    Task<object> Login(LoginModel loginModel, AntiDroneContext context);
    Task<object> Logout();
    Task<object> GetMemberInfo(long id, AntiDroneContext context);
    Task<object> UpdateMemberInfo(long id, UpdateMemberInfo request, AntiDroneContext context);
    Task<object> DeleteMember(long id, AntiDroneContext context);
    Task<object> Register(Member member, AntiDroneContext context);
    Task<object> ResetPassword(long id, string resetPassword, AntiDroneContext context);
}