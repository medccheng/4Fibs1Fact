using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TileMeUpDomain.Enums;
using TileMeUpDomain.Models;
using TileMeUpWebApi;
using TileMeUpWebApi.DAL;

namespace TileMeUpWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public AccessoryController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpGet("GetByItem/{itemId}")]
        public async Task<ActionResult<IEnumerable<Accessory>>> GetByItem(int itemId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Accessory));

            Expression property = Expression.Property(parameter, "ItemId");
            Expression constant = Expression.Constant(itemId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Accessory, bool>> lambdaExpression = Expression.Lambda<Func<Accessory, bool>>(condition, parameter);

            var Accessories = await _unitOfWork.AccessoryRepository.GetAsync(lambdaExpression);

            if (Accessories == null)
            {
                return NotFound();
            }

            return Accessories.ToList();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Accessory>>> GetAll()
        {

            var Accessories = await _unitOfWork.AccessoryRepository.GetAsync();
            if (Accessories == null )
            {
                return NotFound();
            }

            return Accessories.ToList();
        }

        [HttpGet("Get/{accessoryId}")]
        public async Task<ActionResult<Accessory>> Get(int accessoryId)
        {
            var accessory = await _unitOfWork.AccessoryRepository.GetByIDAsync(accessoryId);
            if (accessory == null)
            {
                return NotFound();
            }

            return accessory;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Accessory accessory)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.AccessoryRepository.Update(accessory);
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
        public async Task<ActionResult<Accessory>> Create(Accessory accessory)
        {
            await _unitOfWork.AccessoryRepository.Insert(accessory);
            _unitOfWork.Save();

            return accessory;
        }

        [HttpDelete("Delete/{accessoryId}")]
        public async Task<IActionResult> Delete(int accessoryId)
        {
           
            var accessory = await _unitOfWork.AccessoryRepository.GetByIDAsync(accessoryId);
            if (accessory == null)
            {
                return NotFound();
            }

            _unitOfWork.ClosetRepository.Delete(accessory);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
