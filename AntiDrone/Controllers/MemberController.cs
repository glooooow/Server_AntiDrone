using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using AntiDrone.Data;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntiDrone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class MemberController : Controller
    {
        private readonly AntiDroneContext _context;
        private IMemberService _memberService;

        public MemberController(AntiDroneContext context, IMemberService memberService)
        {
            _context = context;
            _memberService = memberService;
        }
        
        // 로그인
        [HttpPost("/Login", Name = "Login")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            return Json(await _memberService.Login(loginModel, _context));
        }
        
        // 로그아웃
        [HttpPost("/Logout", Name = "Logout")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Logout()
        {
            return Json(await _memberService.Logout(_context));
        }
        
        // 회원 정보 개별 조회
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetMemberInfo(long id)
        {
            return Json(await _memberService.GetMemberInfo(id, _context));
        }
        
        // 회원 정보 수정
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateMemberInfo(long id, UpdateMemberInfo request)
        {
            return Json(await _memberService.UpdateMemberInfo(id, request, _context));
        }
     
        // 회원 개별 삭제
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteMember(long id)
        {
            return Json(await _memberService.DeleteMember(id, _context));
        }
        
        // 회원 가입
        [HttpPost("Join", Name = "RegisterMember")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Register(Member member)
        {	
            return Json(await _memberService.Register(member, _context));
        }
        
        // 아이디 찾기
        [HttpGet("/FindAccount", Name = "FindMemberId")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> FindAccount(string name)
        {
            return Json(await _memberService.FindAccount(name, _context));
        }
        
        // 비밀번호 초기화 (관리자용 기능)
        [HttpPost("{id}", Name = "ResetPassword")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ResetPassword(long id, string resetPassword)
        {
            return Json(await _memberService.ResetPassword(id, resetPassword, _context));
        }
        
        private bool MemberExists(long id)
        {
            return (_context.Member?.Any(e => e.id == id)).GetValueOrDefault();
        }
        
        
        
        
        //------------------------------ 사용자 관리 메뉴 기능 ------------------------------
        
        // 사용자 전체 목록 조회
        [HttpGet("/AllMembers", Name = "GetAllMemberList")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllMemberList(string? searchType, string? searchKeyword)
        {
            return Json(await _memberService.GetAllMemberList(searchType, searchKeyword, _context));
        }
        
        // 로그인/로그아웃 이력 목록 조회
        [HttpGet("/Signinouts", Name = "GetSigninoutLogs")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetSigninoutLogs()
        {
            return Json(await _memberService.GetSigninoutLogs(_context));
        }
        
        // 그 외 작업(사용자 정보 변경) 이력 목록 조회
        [HttpGet("/MemberChanges", Name = "GetMemberChangedLogs")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetMemberChangedLogs()
        {
            return Json(await _memberService.GetMemberChangedLogs(_context));
        }
        
        // 가입 승인 대기 목록 조회
        [HttpGet("/NeedApprovals", Name = "GetPendingApprovalList")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetPendingApprovalList(string? searchType, string? searchKeyword)
        {
            return Json(await _memberService.GetPendingApprovalList(searchType, searchKeyword, _context));
        }
        
    }
}
