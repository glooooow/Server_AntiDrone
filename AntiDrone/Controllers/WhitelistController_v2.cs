using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.DroneControl;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace AntiDrone.Controllers;

[ApiController]
[Route("[controller]")]
//[Produces("application/json")]
public class WhitelistController_v2 : Controller
{
    private readonly AntiDroneContext _context;
    private IWhitelistService _whitelistService;
    
    public WhitelistController_v2(AntiDroneContext context, IWhitelistService whitelistService)
    {
        _context = context;
        _whitelistService = whitelistService;
    }

    [HttpPost, ActionName("CreateWhitelist")]
    [Produces("multipart/form-data")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateWhitelist([FromForm] Whitelist? whitelist)
    {
        object? r;
        if (_context.Whitelist == null || whitelist.affiliation == null) 
        {
            r = ResponseGlobal<Whitelist>.Fail(ErrorCode.CanNotWrite);
            return Json(r);
        }
        _context.Whitelist.Add(whitelist);
        await _context.SaveChangesAsync();
        
        r = ResponseGlobal<Whitelist>.Success(whitelist);
        return Json(r);
    }
    
    
    //승인 드론 리스트 조회
     [HttpGet(Name = "GetBoardList")]
     [ProducesResponseType(200)]
     public async Task<ActionResult<IEnumerable<Whitelist>>> GetWhiteList()
     {
         if (_context.Whitelist == null)
         {   
             return null;
         } 
         return await _context.Whitelist.ToListAsync();
     }
    
}