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

    [HttpPost(Name = "CreateWhitelist"), Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(201)]
    public async Task<IActionResult> CreateWhitelist([FromBody] Whitelist? whitelist)
    {
        return Json(await _whitelistService.CreateWhitelist(whitelist, _context));
    }
    
    //승인 드론 리스트 조회
     [HttpGet(Name = "GetWhitelist")]
     [ProducesResponseType(200)]
     public async Task<IActionResult> GetWhiteList()
     {
         return Json(await _whitelistService.GetWhitelists(_context));
     }
     
     //승인 드론 개별 조회
     [HttpGet("{id}")]
     [ProducesResponseType(200)]
     public async Task<IActionResult> GetWhiteDrone(long id)
     {
             return Json(await _whitelistService.GetWhiteDrone(id, _context));
     }
     
     //승인 드론 개별 삭제
     [HttpDelete("{id}")]
     [ProducesResponseType(200)]
     public async Task<IActionResult> DeleteWhiteDrone(long id)
     {
         return Json(await _whitelistService.DeleteWhiteDrone(id, _context));
     }
     
}