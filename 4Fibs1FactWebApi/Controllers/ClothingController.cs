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
    public class ClothingController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public ClothingController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpGet("GetByItem/{itemId}")]
        public async Task<ActionResult<IEnumerable<Clothing>>> GetByItem(int itemId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Clothing));

            Expression property = Expression.Property(parameter, "ItemId");
            Expression constant = Expression.Constant(itemId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Clothing, bool>> lambdaExpression = Expression.Lambda<Func<Clothing, bool>>(condition, parameter);

            var Clothings = await _unitOfWork.ClothingRepository.GetAsync(lambdaExpression);

            if (Clothings == null)
            {
                return NotFound();
            }

            return Clothings.ToList();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Clothing>>> GetAll()
        {

            var Clothings = await _unitOfWork.ClothingRepository.GetAsync();
            if (Clothings == null )
            {
                return NotFound();
            }

            return Clothings.ToList();
        }

        [HttpGet("Get/{clothingId}")]
        public async Task<ActionResult<Clothing>> Get(int clothingId)
        {
            var clothing = await _unitOfWork.ClothingRepository.GetByIDAsync(clothingId);
            if (clothing == null)
            {
                return NotFound();
            }

            return clothing;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Clothing clothing)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ClothingRepository.Update(clothing);
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
        public async Task<ActionResult<Clothing>> Create(Clothing clothing)
        {
            await _unitOfWork.ClothingRepository.Insert(clothing);
            _unitOfWork.Save();

            return clothing;
        }

        [HttpDelete("Delete/{clothingId}")]
        public async Task<IActionResult> Delete(int clothingId)
        {
           
            var clothing = await _unitOfWork.ClothingRepository.GetByIDAsync(clothingId);
            if (clothing == null)
            {
                return NotFound();
            }

            _unitOfWork.ClothingRepository.Delete(clothing);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
