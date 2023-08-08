using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TileMeUpDomain.Models;
using TileMeUpWebApi;
using TileMeUpWebApi.DAL;

namespace TileMeUpWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WallLayoutController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public WallLayoutController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }



        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<WallLayout>>> GetAll()
        {

            var WallLayouts = await _unitOfWork.WallLayoutRepository.GetAsync();
            if (WallLayouts == null )
            {
                return NotFound();
            }

            return WallLayouts.ToList();
        }

        [HttpGet("Get/{wallLayoutId}")]
        public async Task<ActionResult<WallLayout>> Get(int wallLayoutId)
        {
            var wallLayout = await _unitOfWork.WallLayoutRepository.GetByIDAsync(wallLayoutId);
            if (wallLayout == null)
            {
                return NotFound();
            }

            return wallLayout;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(WallLayout wallLayout)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.WallLayoutRepository.Update(wallLayout);
                    _unitOfWork.Save();
                    return NoContent();
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to save changes. Try again, and if the problem persists contact your system administrator.");
                return BadRequest();
            }

            return NoContent();
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Create")]
        public async Task<ActionResult<WallLayout>> Create(WallLayout wallLayout)
        {
            await _unitOfWork.WallLayoutRepository.Insert(wallLayout);
            _unitOfWork.Save();

            return wallLayout;
        }

        [HttpDelete("Delete/{wallLayoutId}")]
        public async Task<IActionResult> Delete(int wallLayoutId)
        {
           
            var wallLayout = await _unitOfWork.WallLayoutRepository.GetByIDAsync(wallLayoutId);
            if (wallLayout == null)
            {
                return NotFound();
            }

            _unitOfWork.WallLayoutRepository.Delete(wallLayout);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
