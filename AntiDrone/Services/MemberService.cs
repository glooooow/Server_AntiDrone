using System.Media;
using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Parameters;

namespace AntiDrone.Services;

public class MemberService : IMemberService
{
    private IMemberService _memberService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public MemberService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<object> LoginCheck(LoginModel loginModel, AntiDroneContext context)
    {
        string result = loginModel.member_id;
       //var session = _httpContextAccessor.HttpContext.Session;
        
        var checkMember = (context.Member.FirstOrDefault(member => member.member_pw == loginModel.member_pw));
        if (checkMember != null)
        {
            /* 회원 권한 : 0=슈퍼, 1=관리, 2=일반 */
            switch (checkMember.authority)
            {
                case 0 :
                    _httpContextAccessor.HttpContext.Session.SetString("member_id", loginModel.member_id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("authority", 0);
                    break;
                
                case 1 :
                    _httpContextAccessor.HttpContext.Session.SetString("member_id", loginModel.member_id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("authority", 1);
                    break;
                
                case 2 :
                    _httpContextAccessor.HttpContext.Session.SetString("member_id", loginModel.member_id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("authority", 0);
                    break;
            }
        }
        else
        {
            return ResponseGlobal<string>.Fail(ErrorCode.NeedToLogin);
        }
        return ResponseGlobal<string>.Success(result);
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