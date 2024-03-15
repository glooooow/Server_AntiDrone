using AntiDrone.Data;

namespace AntiDrone.Services.Interfaces;

public interface IPlaybackService
{
    Task<object> GetAllDet(AntiDroneContext? context);
}