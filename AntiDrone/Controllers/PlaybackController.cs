using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AntiDrone.Data;
using AntiDrone.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AntiDrone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class PlaybackController : Controller
    {
        private readonly AntiDroneContext _context;
        private IPlaybackService _playbackService;

        public PlaybackController(AntiDroneContext context, IPlaybackService playbackService)
        {
            _context = context;
            _playbackService = playbackService;
        }
        
        // 임시 리스트 조회
        [HttpGet("AllDet", Name = "GetAllDet")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAllDet()
        {
            return Json(await _playbackService.GetAllDet(_context));
        }
    }
}
