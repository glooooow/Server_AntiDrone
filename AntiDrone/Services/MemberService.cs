using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AntiDrone.Services;

public class MemberService : IMemberService
{
    private IMemberService _memberService;
    
    public async Task<object> LoginCheck(LoginModel loginModel, AntiDroneContext context)
    {
        string result = loginModel.member_id;
        HttpContext httpContext = new DefaultHttpContext();
        
        if (loginModel.member_id.Equals(context.Member.FindAsync(loginModel.member_id)) && loginModel.member_pw.Equals(context.Member.Find(loginModel.member_pw)))
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

    public async Task<object> GetMemberInfo(long id, AntiDroneContext context)
    {
        if (await context.Member.FindAsync(id) == null)
        {
            return ResponseGlobal<Member>.Fail(ErrorCode.NotFound);
        }
        return ResponseGlobal<Member>.Success(await context.Member.FindAsync(id));
    }

    public async Task<object> DeleteMember(long id, AntiDroneContext context)
    {
        if (await context.Member.FindAsync(id) == null)
        {
            return ResponseGlobal<Member>.Fail(ErrorCode.NotFound);
        }
        context.Member.Remove(context.Member.Find(id));
        await context.SaveChangesAsync();
        return ResponseGlobal<string>.Success("삭제 성공");
    }

    public async Task<object> Register(Member member, AntiDroneContext context)
    {
        ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
        if (modelStateDictionary.IsValid)
        {
            using (context)
            {
                context.Member.Add(member);
                await context.SaveChangesAsync();
            }
        }
        return ResponseGlobal<Member>.Success(member);
    }
}