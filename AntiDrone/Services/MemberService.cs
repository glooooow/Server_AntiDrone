using System.Reflection;
using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Build.Framework;

namespace AntiDrone.Services;

public class MemberService : IMemberService
{
    private IMemberService _memberService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IActionContextAccessor _actionContextAccessor;
    
    public MemberService(IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _actionContextAccessor = actionContextAccessor;
    }
    
    public async Task<object> Login(LoginModel loginModel, AntiDroneContext context)
    {
        string memberId = loginModel.member_id;
        Member.MemberBasicInfo memberBasicInfo= new Member.MemberBasicInfo();
        
        /* 로그인 정보로 회원 정보 확인 */
        var checkMember = (context.Member.FirstOrDefault(member => member.member_pw == loginModel.member_pw));
        memberBasicInfo.authority = checkMember.authority;
        memberBasicInfo.member_name = checkMember.member_name;
        
        /* 회원정보에 있는 권한 확인 후 세션 추가 */
        if (checkMember != null) /* >> FirstOrDefault 사용시 null 검사 필요 */
        {
            /* 회원 권한 : 0=관리, 1=운영, 2=일반 */
            switch (checkMember.authority)
            {
                case 0:
                    return ResponseGlobal<string>.Success("관리자에게 가입 승인을 요청하세요.");
                
                case 1 :
                    _httpContextAccessor.HttpContext.Session.SetString("member_id", loginModel.member_id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("authority", 1);
                    break;
                
                case 2 :
                    _httpContextAccessor.HttpContext.Session.SetString("member_id", loginModel.member_id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("authority", 2);
                    break;
                
                case 3 :
                    _httpContextAccessor.HttpContext.Session.SetString("member_id", loginModel.member_id);
                    _httpContextAccessor.HttpContext.Session.SetInt32("authority", 3);
                    break;
            }
        }
        else
        {
            return ResponseGlobal<Member.MemberBasicInfo>.Fail(ErrorCode.NeedToLogin);
        }
        return ResponseGlobal<Member.MemberBasicInfo>.Success(memberBasicInfo);
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
    
    public async Task<object> UpdateMemberInfo(long id, UpdateMemberInfo request, AntiDroneContext context)
    {
        /* 회원 세션 유무 체크 : 세션의 권한별로 수정할 수 있는 범위의 분류 필요 */
        var session = _httpContextAccessor.HttpContext.Session;
        var LoginSession = session.GetInt32("authority");
        if (LoginSession == null)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        }
        
        /* 해당 회원 데이터가 있는지 탐색 */
        var memberNow = await context.Member.FindAsync(id);
        if (memberNow == null)
        {
            return ResponseGlobal<UpdateMemberInfo>.Fail(ErrorCode.NotFound);
        }
        
        var modelState = _actionContextAccessor.ActionContext.ModelState; /* 모델의 상태를 호출하여 커스텀하기 위함 */
       if (modelState.ContainsKey("member_id") && modelState.ContainsKey("member_name")) /* 모델의 Required 속성을 일시적으로 무시 */
        {
            modelState["member_id"].Errors.Clear();
            modelState["member_name"].Errors.Clear();
        }
       
        var properties = request.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.GetValue(request) != null)
            {
                memberNow.GetType().GetProperty(property.Name).SetValue(memberNow, property.GetValue(request));
            }
        }
        await context.SaveChangesAsync();
        return ResponseGlobal<Member>.Success(memberNow);
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