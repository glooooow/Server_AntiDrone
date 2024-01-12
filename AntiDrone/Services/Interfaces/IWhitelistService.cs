using AntiDrone.Models.Systems.DroneControl;

namespace AntiDrone.Services.Interfaces;

public interface IWhitelistService
{
    public string getName();
    public string[] changeFirstName(string[] list);
}