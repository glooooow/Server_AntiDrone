using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;

namespace AntiDrone.Services;

public class MemberService : IMemberService
{
    private IMemberService _memberService;

    // public List<Member> memberlists(List<Member> members)
    // {
    //     throw new NotImplementedException();
    // }

    public async Task<object> LoginCheck(LoginModel loginModel, AntiDroneContext context)
    {
        string result = loginModel.member_id;
        HttpContext httpContext = new DefaultHttpContext();
        
        if (loginModel.member_id.Equals(context.Member.FindAsync(loginModel.member_id)) && loginModel.member_id.Equals(context.Member.Find(loginModel.member_id)))
        {
            if(context.Member.Find().authority == 0)
            {
                httpContext.Session.SetString("member_id", loginModel.member_id);
                httpContext.Session.SetInt32("authority", 0);
            }
            else if(context.Member.Find().authority == 1)
            {
                httpContext.Session.SetString("member_id", loginModel.member_id);
                httpContext.Session.SetInt32("authority", 1);
            }
            else
            {
                httpContext.Session.SetString("member_id", loginModel.member_id);
                httpContext.Session.SetInt32("authority", 2);
            }
            return ResponseGlobal<string>.Success(result);
        }
        else
        {
            return ResponseGlobal<string>.Fail(ErrorCode.NeedToLogin);
        }
    }
}