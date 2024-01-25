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
        private IMemberService _member_service;

        public MemberController(AntiDroneContext context, IMemberService memberService)
        {
            _context = context;
            _member_service = memberService;
        }

        [HttpGet("/LoginCheck", Name = "LoginCheck")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> LoginCheck(LoginModel loginModel)
        {
            return Json(await _member_service.LoginCheck(loginModel, _context));
        }
        
        [HttpPost("Join", Name = "RegisterMember")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Register(Member member)
        {	
            if (ModelState.IsValid) 
            {
                using (_context)
                {
                    _context.Member.Add(member);
                    await _context.SaveChangesAsync();
                }
            }
            return Json(member);
        }
        
        private bool MemberExists(long id)
        {
            return (_context.Member?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
