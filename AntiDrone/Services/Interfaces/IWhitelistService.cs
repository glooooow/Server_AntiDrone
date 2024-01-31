using AntiDrone.Data;
using AntiDrone.Models.Systems.DroneControl;

namespace AntiDrone.Services.Interfaces;

public interface IWhitelistService
{
    public List<Whitelist> whitelists(List<Whitelist> lists);
    Task<object> CreateWhitelist(Whitelist? whitelist, AntiDroneContext context);
    Task<object> GetWhitelists(AntiDroneContext context);
    Task<object> GetWhiteDrone(long id, AntiDroneContext context);
    Task<object> DeleteWhiteDrone(long id, AntiDroneContext context);
}