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

namespace ComplaintsSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {

        private IUserServices _userservice;
        private IComplaintService _complaintService;

        public ComplaintController(IUserServices userservice, IComplaintService complaintService)
        {
            _userservice = userservice;
            _complaintService = complaintService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            var user = _userservice.Authenticate(model.Username, model.Password);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }


        // GET: api/Complaint
        [HttpGet("get")]
        public List<Complaint> Get()
        {
            return _complaintService.Get();
        }

        // POST: api/Complaint
        [HttpPost("save")]
        public IActionResult Save([FromBody]ComplaintModel model)
        {
            _complaintService.Save(model);
            return Ok();
        }

    }
}
