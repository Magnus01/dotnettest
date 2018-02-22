//using System;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Manager.Entities;
//using Microsoft.Extensions.Logging;
//using Manager.Services;
//using Manager.Models;
//using Microsoft.EntityFrameworkCore;
//using AutoMapper;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authorization;

//namespace Manager.Controllers

//{
//    [Route("api/users")]
//    public class UsersController : Controller
//    {

//        //private ILogger<UsersController> _logger;
//        //private UserInfoContext _context;
//        private UserInfoContext _context;

//        private IUserInfoRepository _userInfoRepository;

 

   
//        //public UsersController(ILogger<UsersController> logger, UserInfoContext context)

//        public UsersController(IUserInfoRepository userInfoRepository, UserInfoContext context)
//             {
//            _context = context;
//            _userInfoRepository = userInfoRepository;
//            }

  

//        [HttpGet(), Authorize]
        
//        public IActionResult GetUsers()
//        {
//            var userEntities = _userInfoRepository.GetUsers();
    
//            var userResults = new List<UserDto>();
        

//            foreach (var userEntity in userEntities)
//            {
//                userResults.Add(new UserDto
//                {
//                    Id = userEntity.Id,
//                    Description = userEntity.Description,
//                    Name = userEntity.Name
                    

//                });
 
//            }

//            return Ok(userResults);
 
//        }
        






//        [HttpPost("")]
//        public IActionResult CreateUser([FromBody] UserDtoCreation uservariable)
//        {
//            var finalUser = Mapper.Map< User>(uservariable);

//            _userInfoRepository.AddUser(finalUser);



//            if (!_userInfoRepository.Save())
//            {
//                return StatusCode(500, "problem");
//            }
            
//            var createdUserToReturn = Mapper.Map<Models.UserDto>(finalUser);


//            return CreatedAtRoute("GetUser", new
//            {
//                id = createdUserToReturn.Id
//            }, createdUserToReturn);
//        }



//        [HttpGet("{id}", Name = "GetUser")]
//        public IActionResult GetUser(int id)
//        {
//            var user = _userInfoRepository.GetUser(id);
             
//            if (user == null)
//            {
//                return NotFound();
//            }
                
//            //if (includeTeachers)
//            //{

//            //    var userResult = Mapper.Map<UserDto>(user);
//            //    return Ok(userResult);


//            //}
//            var userWithoutTeachersResult = Mapper.Map<UserDto>(user);
//            return Ok(userWithoutTeachersResult);

//        }

       
        


















        
//    }
//}
