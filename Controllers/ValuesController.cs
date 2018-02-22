using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Microsoft.Extensions.Configuration;
using Manager.Services;
using Manager.Models;
using System;
using Manager.Entities;
using AutoMapper;

namespace Manager.Controllers
{
    /*
     * This sample demonstrates how to access the user's information from inside a Web API controller.
     * 
     * 1. The user's ID, which is available on the sub claim of the access_token, can be retrieved using the NameIdentifier claim type.
     * 2. If you need full user information you can call the Auth0 /userinfo endpoint, passing the access_token.
     * 3. Lastly, you can view all available claims on the token by iterating through the list of claims on the User property.
     * 
     */

    [Route("api")]
    public class ValuesController : Controller
    {
        private readonly IConfiguration _configuration;
        private UserInfoContext _context;

        private IUserInfoRepository _userInfoRepository;


        //public ValuesController(IConfiguration configuration)
        //{
            
        //}

        public ValuesController(IConfiguration configuration, IUserInfoRepository userInfoRepository, UserInfoContext context)
        {
            _configuration = configuration;
            _context = context;
            _userInfoRepository = userInfoRepository;
        }



        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            return Json(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpGet]
        [Route("private")]
        [Authorize]
        public IActionResult Private()
        {
            return Json(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated to see this."
            });
        }   

        //[Authorize]
        //[HttpGet]
        //[Route("userid")]
        //public object UserId()
        //{
        //    // The user's ID is available in the NameIdentifier claim
        //    string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        //    return new
        //    {
        //        UserId = userId
        //    };
        //}


        [Authorize]
        [HttpGet(Name = "GetUser")]
        [Route("userid")]
        public IActionResult GetUser()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            //string country = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value;
            //string surname = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value;
            //string name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            var user = _userInfoRepository.GetUser(userId );

            if (user == null)
            {
                return NotFound();
            }

            //if (includeTeachers)
            //{

            //    var userResult = Mapper.Map<UserDto>(user);
            //    return Ok(userResult);


            //}
            var userWithoutTeachersResult = Mapper.Map<UserDto>(user);
            return Ok(userWithoutTeachersResult);

        }
















        [Authorize]
        [HttpGet]
        [Route("userinfo")]
        public async Task<object> UserInformation()
        {
            // Retrieve the access_token claim which we saved in the OnTokenValidated event
            string accessToken = User.Claims.FirstOrDefault(c => c.Type == "access_token").Value;

            // If we have an access_token, then retrieve the user's information
            if (!string.IsNullOrEmpty(accessToken))
            {
                var apiClient = new AuthenticationApiClient(_configuration["auth0:domain"]);
                var userInfo = await apiClient.GetUserInfoAsync(accessToken);

                return userInfo;
            }

            return null;
        }

        [Authorize]
        [HttpGet]
        [Route("claims")]
        public object Claims()
        {
            return User.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }
    }
}
