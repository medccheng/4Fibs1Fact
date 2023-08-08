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
    public class JewelryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public JewelryController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpGet("GetByItem/{itemId}")]
        public async Task<ActionResult<IEnumerable<Jewelry>>> GetByItem(int itemId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Jewelry));

            Expression property = Expression.Property(parameter, "ItemId");
            Expression constant = Expression.Constant(itemId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Jewelry, bool>> lambdaExpression = Expression.Lambda<Func<Jewelry, bool>>(condition, parameter);

            var Jewelrys = await _unitOfWork.JewelryRepository.GetAsync(lambdaExpression);

            if (Jewelrys == null)
            {
                return NotFound();
            }

            return Jewelrys.ToList();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Jewelry>>> GetAll()
        {

            var Jewelrys = await _unitOfWork.JewelryRepository.GetAsync();
            if (Jewelrys == null )
            {
                return NotFound();
            }

            return Jewelrys.ToList();
        }

        [HttpGet("Get/{jewelryId}")]
        public async Task<ActionResult<Jewelry>> Get(int jewelryId)
        {
            var jewelry = await _unitOfWork.JewelryRepository.GetByIDAsync(jewelryId);
            if (jewelry == null)
            {
                return NotFound();
            }

            return jewelry;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Jewelry jewelry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.JewelryRepository.Update(jewelry);
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
        public async Task<ActionResult<Jewelry>> Create(Jewelry jewelry)
        {
            await _unitOfWork.JewelryRepository.Insert(jewelry);
            _unitOfWork.Save();

            return jewelry;
        }

        [HttpDelete("Delete/{jewelryId}")]
        public async Task<IActionResult> Delete(int jewelryId)
        {
           
            var jewelry = await _unitOfWork.JewelryRepository.GetByIDAsync(jewelryId);
            if (jewelry == null)
            {
                return NotFound();
            }

            _unitOfWork.JewelryRepository.Delete(jewelry);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
