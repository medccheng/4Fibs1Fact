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
    public class FootwearController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FootwearController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetByItem/{itemId}")]
        public async Task<ActionResult<IEnumerable<Footwear>>> GetByUser(int itemId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Footwear));

            Expression property = Expression.Property(parameter, "ItemId");
            Expression constant = Expression.Constant(itemId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Footwear, bool>> lambdaExpression = Expression.Lambda<Func<Footwear, bool>>(condition, parameter);

            var Footwears = await _unitOfWork.FootwearRepository.GetAsync(lambdaExpression);

            if (Footwears == null)
            {
                return NotFound();
            }

            return Footwears.ToList();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Footwear>>> GetAll()
        {

            var Footwears = await _unitOfWork.FootwearRepository.GetAsync();
            if (Footwears == null )
            {
                return NotFound();
            }

            return Footwears.ToList();
        }

        [HttpGet("Get/{footwearId}")]
        public async Task<ActionResult<Footwear>> Get(int footwearId)
        {
            var footwear = await _unitOfWork.FootwearRepository.GetByIDAsync(footwearId);
            if (footwear == null)
            {
                return NotFound();
            }

            return footwear;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Footwear footwear)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.FootwearRepository.Update(footwear);
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
        public async Task<ActionResult<Footwear>> Create(Footwear footwear)
        {
            await _unitOfWork.FootwearRepository.Insert(footwear);
            _unitOfWork.Save();

            return footwear;
        }

        [HttpDelete("Delete/{footwearId}")]
        public async Task<IActionResult> Delete(int footwearId)
        {
           
            var footwear = await _unitOfWork.FootwearRepository.GetByIDAsync(footwearId);
            if (footwear == null)
            {
                return NotFound();
            }

            _unitOfWork.FootwearRepository.Delete(footwear);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
