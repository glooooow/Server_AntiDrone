using AntiDrone.Data;
using AntiDrone.Models.Systems.DroneControl;
using Microsoft.AspNetCore.Mvc;

namespace AntiDrone.Services.Interfaces;

public interface IWhitelistService
{
    public List<Whitelist> whitelists(List<Whitelist> lists);
    Task<object> CreateWhitelist(Whitelist? whitelist, AntiDroneContext context);
    Task<ActionResult<IEnumerable<Whitelist>>> GetWhitelist(AntiDroneContext context);
}