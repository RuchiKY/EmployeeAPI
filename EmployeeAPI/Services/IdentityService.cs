using EmployeeAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public IdentityService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuartion)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            Configuartion = configuartion;
        }

        public IConfiguration Configuartion { get; }

        public async Task SignInAsync(UserModel user)
        {
            var isExist = await userManager.FindByNameAsync(user.UserName);
            if (isExist == null)
            {
                user.Errors.Add("User does not Exists");
                return;
            }

            try
            {
                var isCredsValid = await signInManager.PasswordSignInAsync(isExist, user.Password, false, false);

                if (!isCredsValid.Succeeded)
                {
                    user.Errors.Add("Creds Invalid!!");
                    return;
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Configuartion["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                user.Errors.Add(ex.Message);
            }

            return;


        }

        public async Task SignUpAsync(UserModel user)
        {
            var isExist = await userManager.FindByNameAsync(user.UserName);
            if (isExist != null)
            {
                user.Errors.Add("User Already Exists");
                return;
            }

            try
            {
                var _usr = user.BindModel();
                var res = await userManager.CreateAsync(_usr, user.Password);

                if (!res.Succeeded)
                {
                    foreach (var err in res.Errors)
                    {
                        user.Errors.Add(err.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                user.Errors.Add(ex.Message);
            }

            return;
        }
    }

}
