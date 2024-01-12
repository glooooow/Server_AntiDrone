using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.DroneControl;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType(201)]
    public async Task<ResponseDTO<Whitelist>> CreateWhitelist([FromBody] Whitelist whitelist)
    {
       
        if (_context.Whitelist == null)
        {
            return ResponseGlobal<Whitelist>.Fail(ErrorCode.CanNotWrite);
        }
        await _context.SaveChangesAsync();
        var r = ResponseGlobal<Whitelist>.Success(whitelist);
        
        return r;
    }
}