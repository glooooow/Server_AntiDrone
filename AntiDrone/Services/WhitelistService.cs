using System.Net.Mime;
using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.DroneControl;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AntiDrone.Services;

public class WhitelistService : IWhitelistService
{
    private IWhitelistService _whitelistService;
    
    public List<Whitelist> whitelists(List<Whitelist> lists)
    {
        throw new NotImplementedException();
    }
    
    public async Task<IActionResult> CreateWhitelist(Whitelist? whitelist, AntiDroneContext context)
    {
        object? r;
        if (context.Whitelist == null || whitelist?.affiliation == null) 
        {
            r = ResponseGlobal<Whitelist>.Fail(ErrorCode.CanNotWrite);
            return (IActionResult)r;
        }
        context.Whitelist.Add(whitelist);
        await context.SaveChangesAsync();
        
        r = ResponseGlobal<Whitelist>.Success(whitelist);
        return (IActionResult)r;
    }

    public async Task<ActionResult<IEnumerable<Whitelist>>> GetWhitelist(AntiDroneContext context)
    {
        return await context.Whitelist.ToListAsync();
    }
    
}