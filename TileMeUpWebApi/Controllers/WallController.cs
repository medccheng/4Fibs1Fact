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
    public class WallController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WallController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet("GetByCreator/{userId}")]
        public async Task<ActionResult<IEnumerable<Wall>>> GetByCreator(int userId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Wall));

            Expression property = Expression.Property(parameter, "CreatedById");
            Expression constant = Expression.Constant(userId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Wall, bool>> lambdaExpression = Expression.Lambda<Func<Wall, bool>>(condition, parameter);

            var Walls = await _unitOfWork.WallRepository.GetAsync(lambdaExpression);

            if (Walls == null)
            {
                return NotFound();
            }

            return Walls.ToList();
        }


        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<Wall>>> GetAll(int? page = null)
        {

            var Walls = await _unitOfWork.WallRepository.GetAsync(null, null, page, 5, "");
            if (Walls == null )
            {
                return NotFound();
            }

            return Walls.ToList();
        }

        [HttpGet("Get/{wallId}")]
        public async Task<ActionResult<Wall>> Get(int wallId)
        {
            var wall = await _unitOfWork.WallRepository.GetByIDAsync(wallId);
            if (wall == null)
            {
                return NotFound();
            }

            return wall;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Wall wall)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.WallRepository.Update(wall);
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
        public async Task<ActionResult<Wall>> Create(Wall wall)
        {
            try
            {
                await _unitOfWork.WallRepository.Insert(wall);
                _unitOfWork.Save();
                return wall;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    wall.ErrorMessage = ex.InnerException.Message;
                else
                    wall.ErrorMessage = ex.Message;
                return wall;
            }
        }

        [HttpDelete("Delete/{wallId}")]
        public async Task<IActionResult> Delete(int wallId)
        {
           
            var wall = await _unitOfWork.WallRepository.GetByIDAsync(wallId);
            if (wall == null)
            {
                return NotFound();
            }

            _unitOfWork.WallRepository.Delete(wall);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
