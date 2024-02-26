using System.Net.Mime;
using AntiDrone.Data;
using AntiDrone.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AntiDrone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class MemberLogController : Controller
    {
        private readonly AntiDroneContext _context;
        private IMemberLogService _memberLogService;

        public MemberLogController(AntiDroneContext context, IMemberLogService memberLogService)
        {
            _context = context;
            _memberLogService = memberLogService;
        }
        

        private bool MemberLogExists(long id)
        {
            return (_context.MemberLog?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
