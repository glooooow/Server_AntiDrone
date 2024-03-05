using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;
using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
        var validateMember = PasswordHasher.VerifyHashedPassword(loginModel.member_pw, checkMember.member_pw);

        var session = _httpContextAccessor.HttpContext.Session;

        if (checkMember == null || validateMember == false)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.InvalidAccount);
        }
        
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
                    session.SetString("member_id", loginModel.member_id);
                    session.SetInt32("authority", 1);
                    break;
                
                case 2 :
                    session.SetString("member_id", loginModel.member_id);
                    session.SetInt32("authority", 2);
                    break;
                
                case 3 :
                    session.SetString("member_id", loginModel.member_id);
                    session.SetInt32("authority", 3);
                    break;
            }
        }
        else
        {
            return ResponseGlobal<Member.MemberBasicInfo>.Fail(ErrorCode.NeedToLogin);
        }
        var mem_index = checkMember.id;
        LatestLogin(mem_index, context);
        RecordMemberLog(mem_index, "로그인", context);
        await context.SaveChangesAsync();
        
        return ResponseGlobal<Member.MemberBasicInfo>.Success(memberBasicInfo);
    }

    public async Task<object> Logout(AntiDroneContext context)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var cookieReq = _httpContextAccessor.HttpContext.Request.Cookies;
        var cookieRes = _httpContextAccessor.HttpContext.Response.Cookies;
        
        if (cookieReq.ContainsKey("AntiDroneSession") == false) /* 로그인 정보가 없을 때 */
        {
            return ResponseGlobal<object>.Fail(ErrorCode.BadRequest);
        }
        
        RecordMemberLog(0, "로그아웃", context); /* 세션에서 id 정보를 받아서 저장해야하므로 제거 전에 실행 */
        session.Remove("member_id");
        session.Remove("authority");
        
        cookieRes.Delete("AntiDroneSession"); /* 쿠키까지 제거 */
        await context.SaveChangesAsync();
        
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
            return ResponseGlobal<object>.Fail(ErrorCode.NeedToLogin);
        }

        /* 해당 회원 데이터가 있는지 탐색 */
        var memberNow = await context.Member.FindAsync(id);
        if (memberNow == null)
        {
            return ResponseGlobal<UpdateMemberInfo>.Fail(ErrorCode.NotFound);
        }
        
        /* request 모델로 넘어오는 null 값 처리 */
        if (memberNow.member_id == session.GetString("member_id") && (request.member_pw != null)) /* 비밀번호 변경은 본인일 경우만 가능, 관리자가 변경시 초기화 메소드 사용 */
        {
            var encryptPw = PasswordHasher.HashPassword(request.member_pw);
            request.member_pw = encryptPw; /* context.Member.Find(id).member_pw를 변수로 담아 사용하면 값을 복사하여 원래의 객체에 영향을 줄 수 X, 따라서 직접 선언 */
        }
        else if (memberNow.member_id != session.GetString("member_id") && (request.member_pw != null))
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        }
        else
        {
            request.member_pw = memberNow.member_pw;
        }

        /* 권한 검사 및 요청 전부터 모델에서 넘어오는 프레임워크 단의 디폴트 처리 값(0 = 미할당)을 기존 DB값으로 유지하기 위함 */
        if (request.authority == 0)
        {
            request.authority = memberNow.authority;
        }
        else if ((session.GetInt32("authority") == 1) && (request.authority == 1 | request.authority == 2 | request.authority == 3))
        {
            RecordMemberLog(id, "권한 변경", context);
        }
        else if ((session.GetInt32("authority") != 1) && (request.authority == 1 | request.authority == 2 | request.authority == 3))
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        }
        else
        {
            return ResponseGlobal<object>.Fail(ErrorCode.BadRequest);
        }

        /* authority와 마찬가지의 방식으로 permission_state 처리 */
        if (request.permission_state == 0)
        {
            request.permission_state = memberNow.permission_state;
        }
        else if ((session.GetInt32("authority") == 1) && request.permission_state == 1)
        {
            RecordMemberLog(id, "가입 승인", context);
        }
        else if ((session.GetInt32("authority") != 1) && request.permission_state == 1)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        }
        else
        {
            return ResponseGlobal<object>.Fail(ErrorCode.BadRequest);
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
        memberNow.member_pw = "비밀번호";
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
        if (IdValidCheck(signupId) == false)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NotAllowedId);
        }

        var name = member.member_name;
        if (NameValidCheck(name) == false)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NotAllowedName);
        }
        
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
                context.Member.Add(member);
                JoinDate(member, context);
                context.SaveChanges();
                RecordMemberLog(member.id, "회원가입", context);
                await context.SaveChangesAsync();
            }
        }
        member.member_pw = "비밀번호"; /* 응답 값에 비밀번호 숨김 위함, DB에 영향 없음 */
        return ResponseGlobal<Member>.Success(member);
    }

    public async Task<object> FindAccount(string name, AntiDroneContext context)
    {
        if (name != null)
        {
            var checkMembers = context.Member.Where(member => member.member_name == name);
            switch (checkMembers.Count())
            {
                case 1:
                    string member_id = "";
                    foreach (var member in checkMembers)
                    {
                        member_id = member.member_id;
                    }
                    return ResponseGlobal<string>.Success(member_id);
                
                case int n when n >= 2: /* 검색한 이름에 따른 아이디 값이 2개 이상일 때 */
                    string[] member_ids = checkMembers.Select(member => member.member_id).ToArray(); /* 아이디 값들을 배열로 리턴하기 위함 */
                    return ResponseGlobal<string[]>.Success(member_ids);
                
                default: 
                    return ResponseGlobal<object>.Fail(ErrorCode.NotFound);
            }
        }
        else
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NotWriteValue);
        }
    }

    public async Task<object> ResetPassword(long id, string resetPassword, AntiDroneContext context)
    {
        /* 회원 세션 체크 : 관리자 권한이 있는지 확인 */
        var session = _httpContextAccessor.HttpContext.Session;
        var LoginSession = session.GetInt32("authority");
        if (LoginSession == null || LoginSession == (0) || LoginSession == (2) || LoginSession == (3))
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        }
        
        if (await context.Member.FindAsync(id) == null)
        {
            return ResponseGlobal<Member>.Fail(ErrorCode.NotFound);
        }
        
        var encryptPw = PasswordHasher.HashPassword(resetPassword);
        context.Member.Find(id).member_pw = encryptPw; /* context.Member.Find(id).member_pw를 변수로 담아 사용하면 값을 복사하여 원래의 객체에 영향을 줄 수 X, 따라서 직접 선언 */
        
        RecordMemberLog(id, "비밀번호 초기화", context);
        await context.SaveChangesAsync();
        return ResponseGlobal<string>.Success("비밀번호 초기화 완료");
    }

    
    
    
    
    //------------------------------ 사용자 관리 메뉴 기능 ------------------------------

    public async Task<object> FindAllowedMember(Expression<Func<Member, bool>> condition, AntiDroneContext context)
    {
        using (context)
        {
            var members = context.Member
                .Where(condition)
                .Select(r => new
                    { r.id, r.authority, r.permission_state, r.member_id, r.member_name })
                .Where(r=> r.permission_state == 1)
                .ToListAsync();
            
            return await members;
        }
    }
    
    
    public async Task<object> GetAllMemberList(string? searchType, string? searchKeyword, AntiDroneContext context)
    {
        /* 관리자 권한이 있는지 세션 확인 위함 */
        var session = _httpContextAccessor.HttpContext.Session;
        
        if (session.GetInt32("authority") != 1 && context.Member != null)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        }
        
        if (context.Member == null)
        {
            return ResponseGlobal<List<Member>>.Fail(ErrorCode.NoData);
        }
        
        if (searchKeyword != null)
        {
            Expression<Func<Member, bool>> condition = null;
            switch (searchType)
            {
                case "ID":
                    condition = r => r.member_id.Contains(searchKeyword);
                    break;

                case "이름":
                    condition = r => r.member_name.Contains(searchKeyword);
                    break;
                
                case "권한":
                    switch (searchKeyword)
                    {
                        case "관리자": case "관리":
                            condition = r => r.authority == 1;
                            break;
                        case "운영자": case "운영":
                            condition = r => r.authority == 2;
                            break;
                        case "일반":
                            condition = r => r.authority == 3;
                            break;
                    }
                    break;
            }

            if (condition != null)
            {
                using (context)
                {
                    var members = await FindAllowedMember(condition, context);
                    return ResponseGlobal<object>.Success(members);
                }
            }
        }

        // if (searchKeyword != null)
        // {
        //     switch (searchType)
        //     {
        //         case "ID" :
        //             using (context)
        //             {
        //                 var members = await context.Member
        //                     .Select(r => new 
        //                         { r.id, r.authority, r.permission_state, r.member_id, r.member_name }).Where(r => r.permission_state == 1 && r.member_id.Contains(searchKeyword)).ToListAsync();
        //                 
        //                 return ResponseGlobal<object>.Success(members);
        //             }
        //
        //         case "이름" :
        //             using (context)
        //             {
        //                 var members = await context.Member
        //                     .Select(r => new 
        //                         { r.id, r.authority, r.permission_state, r.member_id, r.member_name }).Where(r => r.permission_state == 1 && r.member_name.Contains(searchKeyword)).ToListAsync();
        //     
        //                 return ResponseGlobal<object>.Success(members);
        //             }
        //             
        //         case "권한" :
        //             switch (searchKeyword)
        //             {
        //                 case "관리자" : case "관리" :
        //                     using (context)
        //                     {
        //                         var members = await context.Member
        //                             .Select(r => new 
        //                                 { r.id, r.authority, r.permission_state, r.member_id, r.member_name }).Where(r => r.permission_state == 1 && r.authority == 1).ToListAsync();
        //     
        //                         return ResponseGlobal<object>.Success(members);
        //                     }
        //                 case "운영자" : case "운영" :
        //                     using (context)
        //                     {
        //                         var members = await context.Member
        //                             .Select(r => new 
        //                                 { r.id, r.authority, r.permission_state, r.member_id, r.member_name }).Where(r => r.permission_state == 1 && r.authority == 2).ToListAsync();
        //     
        //                         return ResponseGlobal<object>.Success(members);
        //                     }
        //                 case "일반" :
        //                     using (context)
        //                     {
        //                         var members = await context.Member
        //                             .Select(r => new 
        //                                 { r.id, r.authority, r.permission_state, r.member_id, r.member_name }).Where(r => r.permission_state == 1 && r.authority == 3).ToListAsync();
        //     
        //                         return ResponseGlobal<object>.Success(members);
        //                     }
        //             }
        //             break;
        //     }
        // }
        //
        using (context) /* 응답값 커스텀하기 위함 */
        {
            var members = await context.Member
                .Select(r => new 
                { r.id, r.authority, r.permission_state, r.member_id, r.member_name }).Where(r => r.permission_state == 1).ToListAsync();
            
            return ResponseGlobal<object>.Success(members);
        }
    }

    public async Task<object> GetSigninoutLogs(AntiDroneContext context)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        
        if (context.MemberLog == null)
        {
            return ResponseGlobal<List<Member>>.Fail(ErrorCode.NoData);
        }

        if (session.GetInt32("authority") != 1 && context.MemberLog != null)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        } 
        
        using (context)
        {
            var logs = await context.MemberLog
                .Where(r => r.memlog_type == "로그인" || r.memlog_type == "로그아웃").ToListAsync();
            
            return ResponseGlobal<object>.Success(logs);
        }
    }

    public async Task<object> GetMemberChangedLogs(AntiDroneContext context)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        
        if (context.MemberLog == null)
        {
            return ResponseGlobal<List<Member>>.Fail(ErrorCode.NoData);
        }

        if (session.GetInt32("authority") != 1 && context.MemberLog != null)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        } 
        
        using (context)
        {
            var logs = await context.MemberLog
                .Where(r => r.memlog_type == "회원가입" || r.memlog_type == "권한 변경" || r.memlog_type == "가입 승인" || r.memlog_type == "비밀번호 초기화").ToListAsync();
            
            return ResponseGlobal<object>.Success(logs);
        }
    }

    public async Task<object> GetPendingApprovalList(AntiDroneContext context)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        
        if (context.Member == null)
        {
            return ResponseGlobal<List<Member>>.Fail(ErrorCode.NoData);
        }

        if (session.GetInt32("authority") != 1 && context.Member != null)
        {
            return ResponseGlobal<object>.Fail(ErrorCode.NoAuthority);
        } 

        using (context)
        {
            var members = await context.Member
                .Select(r => new 
                    { r.id, r.authority, r.permission_state, r.member_id, r.member_name }).Where(r => r.permission_state == -1).ToListAsync();
            
            return ResponseGlobal<object>.Success(members);
        }
    }

    

    //------------------------------ void 및 클래스 내 util 함수 ------------------------------
    
    // 가입 일시 기록 및 이력 저장
    public void JoinDate(Member member, AntiDroneContext context)
    {
        DateTime registerTime = DateTime.Now;
        
        if (member != null)
        {
            member.join_datetime = registerTime;
        }
    }
    
    // 아이디 정규식
    public bool IdValidCheck(string input)
    {
        return Regex.IsMatch(input, @"^[0-9a-z]{1,20}$");
    }
    
    // 이름 정규식
    public bool NameValidCheck(string input)
    {
        return Regex.IsMatch(input, @"^[가-힣]{2,7}$");
    }
    
    // 최근 로그인 일시 기록
    public void LatestLogin(long id, AntiDroneContext context)
    {
        DateTime latestTime = DateTime.Now;
        
        var loginMember = context.Member.Find(id);
        if (loginMember != null)
        {
            loginMember.latest_access_datetime = latestTime;
        }
    }
    
    // 사용자 이력 저장
    public void RecordMemberLog(long id, string type, AntiDroneContext context)
    {
        MemberLog memberLog = new MemberLog();
        DateTime logOccurTime = DateTime.Now;
        
        var session = _httpContextAccessor.HttpContext.Session;
        var nowLogin = session.GetString("member_id");

        memberLog.memlog_level = "INFO";
        memberLog.memlog_datetime = logOccurTime;
        
        switch (type)
        {
            case "회원가입" :
                memberLog.memlog_type = "회원가입";
                memberLog.memlog_from = context.Member.Find(id).member_id; /* 변수로 선언해서 index를 직접 넣어 찾지 않으면 오류 발생 */
                memberLog.memlog_to = context.Member.Find(id).member_id;
                break;
            case "가입 승인" :
                memberLog.memlog_type = "가입 승인";
                memberLog.memlog_from = nowLogin;
                memberLog.memlog_to = context.Member.Find(id).member_id;
                break;
            case "로그인" :
                memberLog.memlog_type = "로그인";
                memberLog.memlog_from = context.Member.Find(id).member_id;
                memberLog.memlog_to = context.Member.Find(id).member_id;
                break;
            case "로그아웃" :
                memberLog.memlog_type = "로그아웃";
                memberLog.memlog_from = nowLogin;
                memberLog.memlog_to = nowLogin;
                break;
            case "비밀번호 초기화" :
                memberLog.memlog_type = "비밀번호 초기화";
                memberLog.memlog_from = nowLogin;
                memberLog.memlog_to = context.Member.Find(id).member_id;
                break;
            case "권한 변경" :
                memberLog.memlog_type = "권한 변경";
                memberLog.memlog_from = nowLogin;
                memberLog.memlog_to = context.Member.Find(id).member_id;
                break;
        }
        if(nowLogin != null)
            context.MemberLog.Add(memberLog); /* 저장은 호출 함수에서 실행(이중으로 저장처리 할 경우 비동기 오류가 발생하기 때문) */
    }
}