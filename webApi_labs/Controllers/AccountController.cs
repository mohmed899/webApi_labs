using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using webApi_labs.DTO;
using webApi_labs.Models;

namespace webApi_labs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserAplication> usermanger;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<UserAplication> usermanger , IConfiguration config)
        {
            this.usermanger = usermanger;
            configuration = config;
        }
        [HttpPost("Register")]
        
         public async Task<IActionResult> Register( RegisterUserDTO registerUser)
        {
            if(ModelState.IsValid)
            {

                UserAplication user= new UserAplication()
                {
                    UserName = registerUser.userName,
                    Email = registerUser.Email,
                    
                };
                IdentityResult result = await usermanger.CreateAsync(user, registerUser.Password);
                if (result.Succeeded)
                {
                    return Ok("Account Add Success");
                }
                return BadRequest(result.Errors);
            }
            else
                return BadRequest(ModelState);

        }
        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(LoginUserDTO registerUser)
        {

            if (ModelState.IsValid)
            {
                UserAplication user = await usermanger.FindByNameAsync(registerUser.UserName);
                if (user != null)
                {
                   bool rightPassword = await usermanger.CheckPasswordAsync(user, registerUser.Password);
                    if(rightPassword)
                    {
                        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                        SigningCredentials signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        // claims 
                        var Claims = new List<Claim>();
                        Claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        // role 
                        var roles = await usermanger.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            Claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }

                        //gen token 
                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configuration["JWT:ValidIssuer"],
                            audience: configuration["JWT:ValidAudience"],
                            claims: Claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signing
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                    else
                       return Unauthorized();
                }
                else
                      return Unauthorized();
            }
            return BadRequest(ModelState);
        }
    }
}
