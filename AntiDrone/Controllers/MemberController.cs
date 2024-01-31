using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AntiDrone.Data;
using AntiDrone.Models.Systems.Member;
using AntiDrone.Services;
using AntiDrone.Services.Interfaces;

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
        
        private bool MemberExists(long id)
        {
            return (_context.Member?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
