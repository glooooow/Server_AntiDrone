using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Detections;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.EntityFrameworkCore;

namespace AntiDrone.Services;

public class PlaybackService : IPlaybackService
{
    private IPlaybackService _playbackService;
    public async Task<object> GetAllDet(AntiDroneContext? context)
    {
        if (context.Whitelist == null)
        {
            return ResponseGlobal<List<ScannerDetections>>.Fail(ErrorCode.NoData);
        }
        return ResponseGlobal<List<ScannerDetections>>.Success(await context.ScannerDetections.ToListAsync());
    }
}