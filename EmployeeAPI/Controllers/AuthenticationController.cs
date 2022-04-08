using EmployeeAPI.Models;
using EmployeeAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public AuthenticationController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserModel user)
        {
            try
            {
                await identityService.SignUpAsync(user);

                if (user.Errors.Count >= 1)
                {
                    var errors = new ModelStateDictionary();
                    foreach (var error in user.Errors)
                    {
                        errors.AddModelError("User Exists", error);
                    }
                    return ValidationProblem(errors);
                }
                return Ok(new
                {
                    message = "Account Created"
                });

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] UserModel user)
        {
            try
            {
                await identityService.SignInAsync(user);

                if (user.Errors.Count >= 1)
                {
                    var errors = new ModelStateDictionary();
                    foreach (var error in user.Errors)
                    {
                        errors.AddModelError("User does not Exists", error);
                    }
                    return ValidationProblem(errors);
                }
                return Ok(new
                {
                    message = "Sign In successfull",
                    token = user.Token
                });

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
