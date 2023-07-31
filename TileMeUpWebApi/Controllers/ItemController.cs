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
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public ItemController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpGet("GetByCloset/{closetId}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetByCloset(int closetId)
        {
            // Define the parameter for the lambda expression: TEntity
            ParameterExpression parameter = Expression.Parameter(typeof(Item));

            Expression property = Expression.Property(parameter, "ClosetId");
            Expression constant = Expression.Constant(closetId, typeof(int));
            Expression condition = Expression.Equal(property, constant);
            Expression<Func<Item, bool>> lambdaExpression = Expression.Lambda<Func<Item, bool>>(condition, parameter);

            var Items = await _unitOfWork.ItemRepository.GetAsync(lambdaExpression);

            if (Items == null)
            {
                return NotFound();
            }

            return Items.ToList();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {

            var Items = await _unitOfWork.ItemRepository.GetAsync();
            if (Items == null )
            {
                return NotFound();
            }

            return Items.ToList();
        }

        [HttpGet("Get/{itemId}")]
        public async Task<ActionResult<Item>> Get(int itemId)
        {
            var item = await _unitOfWork.ItemRepository.GetByIDAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.ItemRepository.Update(item);
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
        public async Task<ActionResult<Item>> Create(Item item)
        {
            await _unitOfWork.ItemRepository.Insert(item);
            _unitOfWork.Save();

            return item;
        }

        [HttpDelete("Delete/{itemId}")]
        public async Task<IActionResult> Delete(int itemId)
        {
           
            var item = await _unitOfWork.ItemRepository.GetByIDAsync(itemId);
            if (item == null)
            {
                return NotFound();
            }

            _unitOfWork.ItemRepository.Delete(item);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
