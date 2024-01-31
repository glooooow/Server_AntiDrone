using System.Collections;
using System.Linq.Expressions;
using System.Media;
using System.Net;
using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
    
    public async Task<object> Login(LoginModel loginModel, AntiDroneContext context)
    {
        string memberId = loginModel.member_id;
        Member.MemberInfo memberInfo = new Member.MemberInfo();
        
        /* 로그인 정보로 회원 정보 확인 */
        var checkMember = (context.Member.FirstOrDefault(member => member.member_pw == loginModel.member_pw));
        memberInfo.authority = checkMember.authority;
        memberInfo.member_name = checkMember.member_name;
        
        /* 회원정보에 있는 권한 확인 후 세션 추가 */
        if (checkMember != null) /* >> FirstOrDefault 사용시 null 검사 필요 */
        {
            /* 회원 권한 : 0=관리, 1=운영, 2=일반 */
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
            return ResponseGlobal<Member.MemberInfo>.Fail(ErrorCode.NeedToLogin);
        }
        return ResponseGlobal<Member.MemberInfo>.Success(memberInfo);
    }

    public async Task<object> Logout()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cookieReq = _httpContextAccessor.HttpContext.Request.Cookies;
        var cookieRes = _httpContextAccessor.HttpContext.Response.Cookies;
        
        session.Remove("member_id");
        session.Remove("authority");
        
        /* 쿠키가 있으면 제거 */
        if (cookieReq.ContainsKey("AntiDroneSession"))
        {
            cookieRes.Delete("AntiDroneSession");
        }

        return ResponseGlobal<string>.Success("성공적으로 로그아웃 하였습니다.");
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