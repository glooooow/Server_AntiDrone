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
    
    public async Task<object> CreateWhitelist(Whitelist? whitelist, AntiDroneContext context)
    {
        if (context.Whitelist == null || whitelist?.affiliation == null) 
        {
            return ResponseGlobal<Whitelist>.Fail(ErrorCode.CanNotWrite);
        }
        context.Whitelist.Add(whitelist);
        await context.SaveChangesAsync();
        
        return ResponseGlobal<Whitelist>.Success(whitelist);
    }

    public async Task<object> GetWhitelists(AntiDroneContext context)
    {
        if (context.Whitelist == null)
        {
            return ResponseGlobal<List<Whitelist>>.Fail(ErrorCode.CanNotWrite);
        }
        return ResponseGlobal<List<Whitelist>>.Success(await context.Whitelist.ToListAsync());
    }
    
}