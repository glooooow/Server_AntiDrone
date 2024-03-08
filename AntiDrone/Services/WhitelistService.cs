using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.DroneControl;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
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
            return ResponseGlobal<Whitelist>.Fail(ErrorCode.NotWriteValue);
        }
        DateTime now = DateTime.Today;
        DateOnly today = DateOnly.FromDateTime(now);
        whitelist.now_date = today;
        context.Whitelist.Add(whitelist);
        await context.SaveChangesAsync();
        
        return ResponseGlobal<Whitelist>.Success(whitelist);
    }

    public async Task<object> GetWhitelists(AntiDroneContext context)
    {
        if (context.Whitelist == null)
        {
            return ResponseGlobal<List<Whitelist>>.Fail(ErrorCode.NotWriteValue);
        }
        return ResponseGlobal<List<Whitelist>>.Success(await context.Whitelist.ToListAsync());
    }

    public async Task<object> GetWhiteDrone(long id, AntiDroneContext context)
    {
        if (await context.Whitelist.FindAsync(id) == null)
        {
            return ResponseGlobal<Whitelist>.Fail(ErrorCode.NotFound);
        }
        return ResponseGlobal<Whitelist>.Success(await context.Whitelist.FindAsync(id));
    }

    public async Task<object> DeleteWhiteDrone(long id, AntiDroneContext context)
    {
        if (await context.Whitelist.FindAsync(id) == null)
        {
            return ResponseGlobal<Whitelist>.Fail(ErrorCode.NotFound);
        }
        context.Whitelist.Remove(context.Whitelist.Find(id));
        await context.SaveChangesAsync();
        return ResponseGlobal<string>.Success("삭제 성공");
    }
}