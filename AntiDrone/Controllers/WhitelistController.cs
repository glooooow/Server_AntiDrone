using System.Net.Mime;
using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.DroneControl;
using AntiDrone.Services;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace AntiDrone.Controllers;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class WhitelistController : Controller
{
    private readonly AntiDroneContext _context;
    private IWhitelistService _whitelistService;
    
    public WhitelistController(AntiDroneContext context, IWhitelistService whitelistService)
    {
        _context = context;
        _whitelistService = whitelistService;
    }

    [HttpPost(Name = "CreateWhitelist")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateWhitelist([FromBody] Whitelist? whitelist)
    {
        return await _whitelistService.CreateWhitelist(whitelist, _context);
    }
    
    //승인 드론 리스트 조회
     [HttpGet(Name = "GetWhitelist")]
     [ProducesResponseType(200)]
     public async Task<ActionResult<IEnumerable<Whitelist>>> GetWhiteList()
     {
         return await _whitelistService.GetWhitelist(_context);
     }
    
     //     //승인 드론 개별 조회
//     [HttpGet("{id}"), ActionName("GetWhitelistDrone")]
//     [ProducesResponseType(200)]
//     public async Task<ActionResult<Whitelist>> GetWhitelistDrone(long id)
//     {
//         var whitelist = await _context.Whitelist.FindAsync(id);
//         if (_context.Whitelist == null || whitelist == null)
//         {
//             return NotFound();
//         }
//         return whitelist;
//     }
//     
//     //승인 드론 개별 조회
//     [HttpGet("drone/{id}"), ActionName("GetWhitelistDrone")]
//     [ProducesResponseType(200)]
//     public async Task<ResponseDTO<Whitelist>> GetWhiteDrone(long id)
//     {
//         var whitelist = await _context.Whitelist.FindAsync(id);
//         if (_context.Whitelist == null || whitelist == null)
//         {
//             return ResponseGlobal<Whitelist>.Fail(ErrorCode.InvalidError);
//         }
//         return ResponseGlobal<Whitelist>.Success(whitelist);
//     }
     
}