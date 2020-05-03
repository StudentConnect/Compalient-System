using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplaintsSystem.Models;
using ComplaintsSystem.Services;
using ComplaintsSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ComplaintsSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        //db handler to save data
        private IUserServices _userservice;
        private IComplaintService _complaintService;
        //MS logger to log informations
        private readonly ILogger<ComplaintController> _logger;

        //DI constructor to Initialize objects
        public ComplaintController(IUserServices userservice, IComplaintService complaintService, ILogger<ComplaintController> logger)
        {
            _userservice = userservice;
            _complaintService = complaintService;
            _logger = logger;
        }


        /// <summary>
        /// Anonymous post function
        /// to provide loggin functionalities
        /// </summary>
        /// <param name="model">User Login Model combination of Email & Password</param>
        /// <returns>User Object or Bad request if user not found in system</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            try
            {
                var user = _userservice.Authenticate(model.Username, model.Password);
                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError("error occrured: {0}", ex.ToString());
                return BadRequest(new { message = "Server Exception" });
            }
            
        }


        // GET: api/Complaint
        /// <summary>
        /// Get list of complaints
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public IActionResult Get()
        {
            try
            {
                return Ok(_complaintService.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError("error occrured: {0}", ex.ToString());
                return BadRequest(new { message = "Server Exception" });
            }
            
        }

        /// <summary>
        /// Save Compalint of logged in user
        /// </summary>
        /// <param name="model">Complaint model required</param>
        /// <returns></returns>
        // POST: api/Complaint
        [HttpPost("save")]
        public IActionResult Save([FromBody]ComplaintModel model)
        {
            try
            {
                _complaintService.Save(model);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("error occrured: {0}", ex.ToString());
                return BadRequest(new { message = "Server Exception" });
            }
        }

    }
}
