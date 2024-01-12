using AntiDrone.Services.Interfaces;

namespace AntiDrone.Services;

public class WhitelistService : IWhitelistService
{
    public string getName()
    {
        return "yun";
    }

    public string[] changeFirstName(string[] list)
    {
        list[0] = "admin";
        return list;
    }
}