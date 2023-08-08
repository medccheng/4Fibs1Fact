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
    public class TileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public TileController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpGet("GetByWall/{wallId}")]
        public async Task<ActionResult<IEnumerable<Tile>>> GetByWall(int wallId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Tile));

            Expression property = Expression.Property(parameter, "WallId");
            Expression constant = Expression.Constant(wallId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Tile, bool>> lambdaExpression = Expression.Lambda<Func<Tile, bool>>(condition, parameter);

            var Tiles = await _unitOfWork.TileRepository.GetAsync(lambdaExpression);

            if (Tiles == null)
            {
                return NotFound();
            }

            return Tiles.ToList();
        }


        [HttpGet("GetByItem/{itemId}")]
        public async Task<ActionResult<IEnumerable<Tile>>> GetByItem(int itemId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Tile));

            Expression property = Expression.Property(parameter, "ItemId");
            Expression constant = Expression.Constant(itemId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Tile, bool>> lambdaExpression = Expression.Lambda<Func<Tile, bool>>(condition, parameter);

            var Tiles = await _unitOfWork.TileRepository.GetAsync(lambdaExpression);

            if (Tiles == null)
            {
                return NotFound();
            }

            return Tiles.ToList();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Tile>>> GetAll()
        {

            var Tiles = await _unitOfWork.TileRepository.GetAsync();
            if (Tiles == null )
            {
                return NotFound();
            }

            return Tiles.ToList();
        }

        [HttpGet("Get/{tileId}")]
        public async Task<ActionResult<Tile>> Get(int tileId)
        {
            var tile = await _unitOfWork.TileRepository.GetByIDAsync(tileId);
            if (tile == null)
            {
                return NotFound();
            }

            return tile;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Tile tile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.TileRepository.Update(tile);
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
        public async Task<ActionResult<Tile>> Create(Tile tile)
        {
            await _unitOfWork.TileRepository.Insert(tile);
            _unitOfWork.Save();

            return tile;
        }

        [HttpDelete("Delete/{tileId}")]
        public async Task<IActionResult> Delete(int tileId)
        {
           
            var tile = await _unitOfWork.TileRepository.GetByIDAsync(tileId);
            if (tile == null)
            {
                return NotFound();
            }

            _unitOfWork.TileRepository.Delete(tile);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
