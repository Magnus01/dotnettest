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
            //string userId = User.Claims.FirstOrDefault(c => c. == ClaimTypes.NameIdentifier).Value;
            //var objectclaims = User.Claims.ToList();
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            //string country = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country).Value;
            //string surname = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname).Value;
            //string name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            var user = _userInfoRepository.GetUser(userId );

            if (userId == null)
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



        [HttpPost("")]
        //[Authorize]
        
        public IActionResult dskjflj([FromBody] UserDtoCreation uservariable)
        {
            //string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var newuserinfo = _userInfoRepository.GetUser(uservariable.Id);

            var finalUser = Mapper.Map<User>(uservariable);

            if (newuserinfo.EducatorDetails == null && finalUser.EducatorDetails != null) { 
          

            newuserinfo.Description = finalUser.Description;
            newuserinfo.Id = finalUser.Id;
                //newuserinfo.EducatorDetails = new EducatorDetail() {
                //    Name = finalUser.EducatorDetails.Name
                //};

                newuserinfo.EducatorDetails = finalUser.EducatorDetails;
                    newuserinfo.EducatorDetails.Id = null;
                newuserinfo.LearnerDetails = finalUser.LearnerDetails;
            newuserinfo.Name = finalUser.Name;
                if (!_userInfoRepository.Save())
                {
                    return StatusCode(500, "problem");
                }
            }
            else if (newuserinfo.LearnerDetails == null && finalUser.LearnerDetails != null)
            {
              

                newuserinfo.Description = finalUser.Description;
                newuserinfo.Id = finalUser.Id; 
                newuserinfo.LearnerDetails = finalUser.LearnerDetails;
                newuserinfo.Name = finalUser.Name;
                if (!_userInfoRepository.Save())
                {
                    return StatusCode(500, "problem");
                }
            }
            return Ok();
            //return CreatedAtRoute("GetUser", new
            //{
            //    id = createdUserToReturn.Id
            //}, createdUserToReturn);
        }




        //CREATE CLASSROOM
        [HttpPost("{userId}/classrooms")]
        public IActionResult CreateClass(int userId, [FromBody]ClassroomDto classroomDetails)
        {
            User user = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();
            //Check for null etC
            user.EducatorDetails.Classrooms.Add(
                new Classroom()
                {
                    Name = classroomDetails.Name,
                    Description = classroomDetails.Description,
                }
                );
            //user.EducatorDetails.Enrollments.Add(newEnrollment);
            _context.SaveChanges();
            return Ok();
        }

        //[HttpPost("")]
        //[Authorize]
        //public IActionResult CreateUser()
        ////public IActionResult CreateUser([FromBody] UserDtoCreation uservariable)
        //        //{
        //        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //        var finalUser = Mapper.Map<User>(uservariable);

        //        _userInfoRepository.AddUser(finalUser);



        //            if (!_userInfoRepository.Save())
        //            {
        //                return StatusCode(500, "problem");
        //    }

        //    var createdUserToReturn = Mapper.Map<Models.UserDto>(finalUser);
        //            return Ok();

        //        return CreatedAtRoute("GetUser", new
        //        {
        //            id = createdUserToReturn.Id
        //}, createdUserToReturn);
        //}























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

