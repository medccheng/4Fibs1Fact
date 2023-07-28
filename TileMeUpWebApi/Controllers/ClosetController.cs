using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class ClosetController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClosetController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetByCreator/{userId}")]
        public async Task<ActionResult<IEnumerable<Closet>>> GetByCreator(int userId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Closet));

            Expression property = Expression.Property(parameter, "CreatedById");
            Expression constant = Expression.Constant(userId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Closet, bool>> lambdaExpression = Expression.Lambda<Func<Closet, bool>>(condition, parameter);

            var Closets = await _unitOfWork.ClosetRepository.GetAsync(lambdaExpression);

            if (Closets == null)
            {
                return NotFound();
            }

            return Closets.ToList();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Closet>>> GetAll()
        {

            var Closets = await _unitOfWork.ClosetRepository.GetAsync();
            if (Closets == null )
            {
                return NotFound();
            }

            return Closets.ToList();
        }

        [HttpGet("Get/{closetId}")]
        public async Task<ActionResult<Closet>> Get(int closetId)
        {
            var closet = await _unitOfWork.ClosetRepository.GetByIDAsync(closetId);
            if (closet == null)
            {
                return NotFound();
            }

            return closet;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Closet closet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ClosetRepository.Update(closet);
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
        public async Task<ActionResult<Closet>> Create(Closet closet)
        {
            await _unitOfWork.ClosetRepository.Insert(closet);
            _unitOfWork.Save();

            return closet;
        }

        [HttpDelete("Delete/{closetId}")]
        public async Task<IActionResult> Delete(int closetId)
        {
           
            var closet = await _unitOfWork.ClosetRepository.GetByIDAsync(closetId);
            if (closet == null)
            {
                return NotFound();
            }

            _unitOfWork.ClosetRepository.Delete(closet);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
