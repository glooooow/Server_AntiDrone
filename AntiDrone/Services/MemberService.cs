using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        Member.MemberBasicInfo memberBasicInfo= new Member.MemberBasicInfo();
        
        /* 로그인 정보로 회원 정보 확인 */
        var checkMember = (context.Member.FirstOrDefault(member => member.member_id == loginModel.member_id));

        if (checkMember == null)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAccount);
        }
        
        var validateMember = PasswordHasher.VerifyHashedPassword(loginModel.member_pw, checkMember.member_pw);
        
        /* 응답값 표출을 위한 것으로, DB에 저장된 값을 모델에 담아준다 */
        memberBasicInfo.authority = checkMember.authority;
        memberBasicInfo.member_name = checkMember.member_name;
        
        /* 회원정보에 있는 권한 확인 후 세션 추가 */
        if (checkMember != null &&  validateMember == true) /* >> FirstOrDefault 사용시 null 검사 필요 */
        {
            /* 회원 권한 : 1=관리, 2=운영, 3=일반 */
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
        var mem_index = checkMember.id;
        LatestLogin(mem_index, context);
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
       
        if (request.authority == 0) /* 요청 전부터 모델에서 넘어오는 프레임워크 단의 디폴트 처리 값(0 = 미할당)을 기존 DB값으로 유지하기 위함 */
            request.authority = memberNow.authority;
        
        if (request.permission_state == 0) /* 위의 authority와 마찬가지의 방식으로 permission_state 처리 */
            request.permission_state = memberNow.permission_state;
        
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

        var signupId = member.member_id;
        var checkAccount = (context.Member.FirstOrDefault(member => member.member_id == signupId)); /* 기존 가입된 ID와 똑같은 값 있는지 탐색 */

        if (checkAccount != null) /* 위에서 탐색한 값이 있으면 에러코드를 반환 */
        {
            return ResponseGlobal<string>.Fail(ErrorCode.ExistedAccount);
        }

        else if (modelStateDictionary.IsValid)
        {
            var encryptPw = PasswordHasher.HashPassword(member.member_pw);
            member.member_pw = encryptPw;
            
            using (context)
            {
                joinDate(member);
                context.Member.Add(member);
                await context.SaveChangesAsync();
            }
        }
        return ResponseGlobal<Member>.Success(member);
    }

    
    
    
    //------------------------------ void 함수 ------------------------------
    
    // 가입 일시 기록
    public void joinDate(Member member)
    {
        DateTime registerTime = DateTime.Now;
        
        if (member != null)
        {
            member.join_datetime = registerTime;
        }
    }
    
    // 최근 로그인 일시 기록
    public void LatestLogin(long id, AntiDroneContext context)
    {
        DateTime latestTime = DateTime.Now;
        
        var loginMember = context.Member.Find(id);
        if (loginMember != null)
        {
            loginMember.latest_access_datetime = latestTime;
            context.SaveChangesAsync();
        }
    }
}