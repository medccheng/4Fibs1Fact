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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IConfiguration _configuration;

        public UserController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }


        [HttpGet("{page}")]
        public async Task<ActionResult<IEnumerable<User>>> GetAll(int? page = null)
        {

            var Users = await _unitOfWork.UserRepository.GetAsync(null, null, page, 5, "");
            if (Users == null)
            {
                return NotFound();
            }

            return Users.ToList();
        }

        [HttpGet("Get/{userId}")]
        public async Task<ActionResult<User>> Get(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIDAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Update")]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _unitOfWork.UserRepository.Update(user);
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
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                await _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.Save();
                return user;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    user.ErrorMessage = ex.InnerException.Message;
                else
                    user.ErrorMessage = ex.Message;
                return user;
            }            
        }

        [HttpDelete("Delete/{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {

            var user = await _unitOfWork.UserRepository.GetByIDAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            _unitOfWork.UserRepository.Delete(user);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}
