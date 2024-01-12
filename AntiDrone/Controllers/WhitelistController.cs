using AntiDrone.Data;
using AntiDrone.Models;
using AntiDrone.Models.Systems.DroneControl;
using AntiDrone.Services;
using AntiDrone.Services.Interfaces;
using AntiDrone.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AntiDrone.Controllers;

// [ApiController]
// [Route("[controller]")]
// //[Produces("application/json")]
// public class WhitelistController : Controller
// {
//     private readonly AntiDroneContext _context;
//     private IWhitelistService _whitelistService;
//     
//     public WhitelistController(AntiDroneContext context, IWhitelistService whitelistService)
//     {
//         _context = context;
//         _whitelistService = whitelistService;
//     }
//     
//     //승인 드론 등록
//     [HttpPost, ActionName("CreateWhitelist")]
//     [ProducesResponseType(201)]
//     public async Task<ActionResult<ResponseDTO<ActionResult<Whitelist>>>> CreateWhitelist([FromBody] Whitelist whitelist)
//     {
//         if (_context.Whitelist == null)
//         {
//             return ResponseGlobal<ActionResult<Whitelist>>.Fail(ErrorCode.CanNotWrite);
//         }
//         
//         _context.Whitelist.Add(whitelist);
//         await _context.SaveChangesAsync();
//         
//         var success = CreatedAtAction("GetWhitelistDrone", new { id = whitelist.id }, whitelist);
//         
//         return ResponseGlobal<ActionResult<Whitelist>>.Success(success);
//     }
//     
//     //승인 드론 리스트 조회
//     [HttpGet(Name = "GetBoardList")]
//     [ProducesResponseType(200)]
//     public async Task<ActionResult<IEnumerable<Whitelist>>> GetWhiteList()
//     {
//         if (_context.Whitelist == null)
//         {   
//             return null;
//         }
//         return await _context.Whitelist.ToListAsync();
//     }
//     
//     //승인 드론 개별 조회
//     [HttpGet("{id}"), ActionName("GetWhitelistDrone")]
//     [ProducesResponseType(200)]
//     public async Task<ActionResult<Whitelist>> GetWhitelistDrone(long id)
//     {
//         var whitelist = await _context.Whitelist.FindAsync(id);
//         if (_context.Whitelist == null || whitelist == null)
//         {
//             return NotFound();
//         }
//         return whitelist;
//     }
//     
//     //승인 드론 개별 조회
//     [HttpGet("drone/{id}"), ActionName("GetWhitelistDrone")]
//     [ProducesResponseType(200)]
//     public async Task<ResponseDTO<Whitelist>> GetWhiteDrone(long id)
//     {
//         var whitelist = await _context.Whitelist.FindAsync(id);
//         if (_context.Whitelist == null || whitelist == null)
//         {
//             return ResponseGlobal<Whitelist>.Fail(ErrorCode.InvalidError);
//         }
//         return ResponseGlobal<Whitelist>.Success(whitelist);
//     }
//     
//     
//     //해당 승인드론 존재 여부 확인
//     private bool WhitelistExists(long id)
//     {
//         return (_context.Whitelist?.Any(e => e.id == id)).GetValueOrDefault();
//     }
// }