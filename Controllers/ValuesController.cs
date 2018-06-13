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
using Microsoft.EntityFrameworkCore;

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

            var user = _userInfoRepository.GetUser(userId);

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



        ////[HttpPost("")]
        //////[Authorize]

        ////public IActionResult dskjflj([FromBody] UserDtoCreation uservariable)
        ////{
        ////    //string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

        ////    var newuserinfo = _userInfoRepository.GetUser(uservariable.Id);

        ////    var finalUser = Mapper.Map<User>(uservariable);

        ////    if (newuserinfo.EducatorDetails == null && finalUser.EducatorDetails != null) { 


        ////    newuserinfo.Description = finalUser.Description;
        ////    newuserinfo.Id = finalUser.Id;
        ////        //newuserinfo.EducatorDetails = new EducatorDetail() {
        ////        //    Name = finalUser.EducatorDetails.Name
        ////        //};

        ////        newuserinfo.EducatorDetails = finalUser.EducatorDetails;
        ////            newuserinfo.EducatorDetails.Id = null;
        ////        newuserinfo.LearnerDetails = finalUser.LearnerDetails;
        ////    newuserinfo.Name = finalUser.Name;
        ////        if (!_userInfoRepository.Save())
        ////        {
        ////            return StatusCode(500, "problem");
        ////        }
        ////    }
        ////    else if (newuserinfo.LearnerDetails == null && finalUser.LearnerDetails != null)
        ////    {


        ////        newuserinfo.Description = finalUser.Description;
        ////        newuserinfo.Id = finalUser.Id; 
        ////        newuserinfo.LearnerDetails = finalUser.LearnerDetails;
        ////        newuserinfo.Name = finalUser.Name;
        ////        if (!_userInfoRepository.Save())
        ////        {
        ////            return StatusCode(500, "problem");
        ////        }
        ////    }
        ////    return Ok();
        ////    //return CreatedAtRoute("GetUser", new
        ////    //{
        ////    //    id = createdUserToReturn.Id
        ////    //}, createdUserToReturn);
        ////}


























        //first implementation without sending back a request that tells the UI to skip the registration
        [HttpPost("userloginscenario")]
        //[Authorize]

        public IActionResult getorcreateuser([FromBody] UserDtoCreation uservariable)
        {
            //string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

 
          

        

            var userOrnull = _userInfoRepository.ReturnUserorNull(uservariable.Id);
            var inviteOrnulll = _userInfoRepository.ReturnInviteorNull(uservariable.Id);
            //check to see if user is in the database
            //check to see if user is in the invite table
            if (userOrnull != null)
            {
                //ready to patch
                userOrnull.Authorization = uservariable.Authorization;
                _context.SaveChanges();

            }
            //if neither 
            //add user to database
            if (userOrnull == null && inviteOrnulll == null)
            {
                var createdUser = _userInfoRepository.CreateUser(uservariable.Id, uservariable.Description, uservariable.Authorization);



                return Ok(uservariable.Id);
            }

            // if in the database and not in the invite table 
            //do nothing

            // if not in the database and in the invite table 
            // add the user, add learner details, and enroll with the classroomID in the invite table
            if (userOrnull == null && inviteOrnulll != null)
            {
                var createdUser = _userInfoRepository.CreateUserLearnerAndEnroll(uservariable.Id, inviteOrnulll.Email, inviteOrnulll.ClassroomID);
                return Ok(uservariable.Id);
            }
            // if in the database and in the invite table
            //do nothing

            return Ok(uservariable.Id);
            //return CreatedAtRoute("GetUser", new
            //{
            //    id = createdUserToReturn.Id
            //}, createdUserToReturn);
        }



        [HttpPost("")]
        //[Authorize]

        public IActionResult createEducatorDetail([FromBody] UserDtoCreation uservariable)
        {
            //string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var getorcreateduser = _userInfoRepository.GetorCreateUser(uservariable.Id, uservariable.Description);


            var finalUser = Mapper.Map<User>(uservariable);

            if (getorcreateduser.EducatorDetails == null && finalUser.EducatorDetails != null)
            {


                getorcreateduser.Description = finalUser.Description;
                getorcreateduser.Id = finalUser.Id;
                //newuserinfo.EducatorDetails = new EducatorDetail() {
                //    Name = finalUser.EducatorDetails.Name
                //};

                getorcreateduser.EducatorDetails = finalUser.EducatorDetails;
                getorcreateduser.EducatorDetails.Id = null;
                getorcreateduser.LearnerDetails = finalUser.LearnerDetails;
                getorcreateduser.Name = finalUser.Name;
                getorcreateduser.EducatorDetails.Classrooms = finalUser.EducatorDetails.Classrooms.ToList();
                if (!_userInfoRepository.Save())
                {
                    return StatusCode(500, "problem");
                }
            }

            else if (getorcreateduser.LearnerDetails == null && finalUser.LearnerDetails != null)
            {


                getorcreateduser.Description = finalUser.Description;
                getorcreateduser.Id = finalUser.Id;
                getorcreateduser.LearnerDetails = finalUser.LearnerDetails;
                getorcreateduser.Name = finalUser.Name;
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






























        ////GETUSER
        //[HttpGet("{userId}/getclassrooms")]

        //public IActionResult GetAllClassrooms(string userId)
        //{
        //    var classroomEntities = _userInfoRepository.GetClassrooms(userId);

        //    var classroomResults = new List<ClassroomDto>();


        //    foreach (var classroomEntity in classroomEntities)
        //    {
        //        classroomEntity(new ClassroomDto
        //        {
        //            Id = classroomEntity.Id,
        //            Description = classroomEntity.Description,
        //            Name = classroomEntity.Name


        //        });

        //    }

        //    return Ok(classroomResults);

        //}



        [HttpGet("{userId}/classrooms")]
        public IActionResult GetEducatorClass(string userId)
        {

            var classrooms = _userInfoRepository.GetEducatorDetailsForUser(userId);
            if (classrooms == null)
            {
                return Ok(null);
            }
            //var studentinvitationsincluded = _userInfoRepository.GetClassrooms(userId);

            //var studentinvitationsincluded = classrooms.EducatorDetails.Classrooms;

            //var teacherResult = Mapper.Map<List<EducatorDetailsDto>>(educator
            //return Ok(classrooms.EducatorDetails.Classrooms);
            return Ok(Mapper.Map<List<ClassroomDto>>(classrooms.EducatorDetails.Classrooms));
            //return Ok(classrooms.EducatorDetails.Classrooms);

        }


        [HttpGet("{userId}/GetInvitations")]
        public IActionResult GetInvitations(string userId)
        {

            var classrooms = _userInfoRepository.GetStudentInvitationDetailsForUser(userId);
            if (classrooms.EducatorDetails == null)
            {
                return Ok(null);
            }

            //var studentinvitationsincluded = _userInfoRepository.GetClassrooms(userId);

            //var studentinvitationsincluded = classrooms.EducatorDetails.Classrooms;

            var teacherResult = Mapper.Map<List<ClassroomDto>>(classrooms.EducatorDetails.Classrooms);
            //return Ok(classrooms.EducatorDetails.Classrooms);
            return Ok(teacherResult);
            //return Ok(classrooms.EducatorDetails.Classrooms);

        }








        //CREATE CLASSROOM
        [HttpPost("{userId}/classrooms")]
        public IActionResult CreateClass(string userId, [FromBody]ClassroomDto classroomDetails)
        {
            //        string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var finalclassroom = Mapper.Map<Classroom>(classroomDetails);

            //        _userInfoRepository.AddUser(finalUser);






            _userInfoRepository.CreateClassroomfunction(userId, finalclassroom);
            if (!_userInfoRepository.Save())
            {
                return StatusCode(500, "problem");
            }
            var createdClassroomToReturn = Mapper.Map<Models.ClassroomDto>(finalclassroom);
            // User user = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();
            // //Check for null etC
            //user.EducatorDetails.Classrooms.Add(
            //     new Classroom()
            //     {
            //         Name = classroomDetails.Name,
            //         Description = classroomDetails.Description,
            //     }
            //     );
            //user.EducatorDetails.Enrollments.Add(newEnrollment);
            _context.SaveChanges();
            return Ok(createdClassroomToReturn);
        }









        //UPDATE CLASSROOM
        [HttpPatch("{userId}/classrooms/{classroomId}")]
        public IActionResult UpdateClass(string userId, int classroomId, [FromBody]ClassroomDto classDto)
        {
            User usersvariableforclassroom = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails.Classrooms).FirstOrDefault();
            var classroom = usersvariableforclassroom.EducatorDetails.Classrooms.Where(x => x.Id == classroomId).FirstOrDefault();

            //ready to patch
            classroom.Name = classDto.Name;
            classroom.Description = classDto.Description;


            _context.SaveChanges();
            return Ok();
        }


        //DELETE CLASSROOM
        [HttpDelete("{userId}/classrooms/{classroomId}")]
        public IActionResult DeleteClass(string userId, int classroomId)
        {
            //User user = _context.Users.Where(x => x.Id == userId).Include(x => x.LearnerDetails).FirstOrDefault();
            //Check for null etc
            Classroom ClassroomToRemove = new Classroom { Id = classroomId };
            //user.LearnerDetails.Enrollments.Add(newEnrollment);
            _context.Attach(ClassroomToRemove);
            _context.Classroom.Remove(ClassroomToRemove);
            _context.SaveChanges();
            return Ok();
        }




        //ENROLL IN CLASS
        [HttpPost("{userId}/{classroomId}/enrollments")]
        public IActionResult EnrollLearnerInClass(string userId, int classroomId, [FromBody]Classroom classrooms)
        {

            //check if email is in the database
            var EmailOrNull = _userInfoRepository.ReturnEmailorNull(userId);


            if (EmailOrNull != null)
            {
                //User user = _context.Users.Where(x => x.Id == userId).Include(x => x.LearnerDetails).FirstOrDefault();
                //if (EmailOrNull.LearnerDetails == null)
                //{
                //    EmailOrNull.LearnerDetails.Name = EmailOrNull.Name;
                //    EmailOrNull.LearnerDetails.Subscribed = true;
                //}
                //else if (EmailOrNull.LearnerDetails != null)
                //{
                //    EmailOrNull.LearnerDetails.Name = EmailOrNull.Name;
                //    EmailOrNull.LearnerDetails.Subscribed = true;
                //}
                //Check for null etc
                Enrollment newEnrollment = new Enrollment
                {
                    ClassroomID = classroomId
                    //LearnerDetailsId = EmailOrNull.LearnerDetails.Id,
                };

                //add user invitation table


                EmailOrNull.LearnerDetails.Enrollments.Add(newEnrollment);
                _context.SaveChanges();

                //need to return student with enrollment
                //or add an attribute to enrollment that says PendingInvitationStatus
                return Ok();
            }
            else

            {
                
                 
                List<object> termsList = new List<object>();
                for (int i = 0; i < classrooms.StudentInvitation.Count(); i++)
                {
                    StudentInvitation newinvite = new StudentInvitation
                    {
                        //ClassroomID = classroomId,
                        Email = classrooms.StudentInvitation.ToList()[i].Email
                        //LearnerDetailsId = EmailOrNull.LearnerDetails.Id,
                    };

                    User usersvariableforclassroom = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails.Classrooms).FirstOrDefault();
                    var classroom = usersvariableforclassroom.EducatorDetails.Classrooms.Where(x => x.Id == classroomId).FirstOrDefault();
                    classroom.StudentInvitation.Add(newinvite);
                    //_context.Users.FirstOrDefault(x => x.Id == userId).EducatorDetails.Classrooms.FirstOrDefault(x => x.Id == classroomId).StudentInvitation.Add(newinvite);
                    termsList.Add(newinvite);
                    //_context.StudentInvitation.Add(newinvite);
                    //_context.SaveChanges();

                    //new added this return
                    //return Ok(classroom);





                }

                //_context.StudentInvitation.Add( new StudentInvitation
                //{
                _context.SaveChanges();
                //})
                return Ok();
            }
        }







        ////ENROLL IN CLASS
        //[HttpPost("{userId}/{classroomId}/enrollments")]
        //public IActionResult EnrollLearnerInClass(string userId, int classroomId, [FromBody]Classroom classrooms)
        //{

        //    //check if email is in the database
        //    var EmailOrNull = _userInfoRepository.ReturnEmailorNull(userId);


        //    if (EmailOrNull != null)
        //    {
        //        //User user = _context.Users.Where(x => x.Id == userId).Include(x => x.LearnerDetails).FirstOrDefault();
        //        //if (EmailOrNull.LearnerDetails == null)
        //        //{
        //        //    EmailOrNull.LearnerDetails.Name = EmailOrNull.Name;
        //        //    EmailOrNull.LearnerDetails.Subscribed = true;
        //        //}
        //        //else if (EmailOrNull.LearnerDetails != null)
        //        //{
        //        //    EmailOrNull.LearnerDetails.Name = EmailOrNull.Name;
        //        //    EmailOrNull.LearnerDetails.Subscribed = true;
        //        //}
        //        //Check for null etc
        //        Enrollment newEnrollment = new Enrollment
        //        {
        //            ClassroomID = classroomId
        //            //LearnerDetailsId = EmailOrNull.LearnerDetails.Id,
        //        };

        //        //add user invitation table


        //        EmailOrNull.LearnerDetails.Enrollments.Add(newEnrollment);
        //        _context.SaveChanges();

        //        //need to return student with enrollment
        //        //or add an attribute to enrollment that says PendingInvitationStatus
        //        return Ok();
        //    }
        //    else

        //    {


        //        List<object> termsList = new List<object>();
        //        for (int i = 0; i < classrooms.StudentInvitation.Count(); i++)
        //        {
        //            StudentInvitation newinvite = new StudentInvitation
        //            {
        //                //ClassroomID = classroomId,
        //                Email = classrooms.StudentInvitation.ToList()[i].Email
        //                //LearnerDetailsId = EmailOrNull.LearnerDetails.Id,
        //            };

        //            User usersvariableforclassroom = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails.Classrooms).FirstOrDefault();
        //            var classroom = usersvariableforclassroom.EducatorDetails.Classrooms.Where(x => x.Id == classroomId).FirstOrDefault();
        //            classroom.StudentInvitation.Add(newinvite);
        //            //_context.Users.FirstOrDefault(x => x.Id == userId).EducatorDetails.Classrooms.FirstOrDefault(x => x.Id == classroomId).StudentInvitation.Add(newinvite);
        //            termsList.Add(newinvite);
        //            //_context.StudentInvitation.Add(newinvite);
        //            _context.SaveChanges();

        //            //new added this return
        //            return Ok(classroom);





        //        }

        //        //_context.StudentInvitation.Add( new StudentInvitation
        //        //{
        //        _context.SaveChanges();
        //        //})
        //        return Ok(termsList);
        //    }
        //}





















        public Classroom Classroom { get; set; }

        public int ClassroomID { get; set; }
 

        public int BookId { get; set; }

        public int ChapterId { get; set; }

        public string Date { get; set; }

        public string Title { get; set; }

        //Create Assignment
        [HttpPost("{userId}/{classroomId}/assignment")]
        public IActionResult CreateAssignmentInClass(string userId, int classroomId, [FromBody]Assignment assignments)
        {
 
                    Assignment newassignment = new Assignment
                    {
                     
                        ClassroomID = classroomId,
                        BookId = assignments.BookId,
                        ChapterId = assignments.ChapterId,
                        Date = assignments.Date,
                        Title = assignments.Title

                      
                    };

                    User usersvariableforclassroom = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails.Classrooms).FirstOrDefault();
                    var classroom = usersvariableforclassroom.EducatorDetails.Classrooms.Where(x => x.Id == classroomId).FirstOrDefault();
                    classroom.Assignment.Add(newassignment);
 
 
                _context.SaveChanges();
                return Ok(newassignment);
            
        }















        //public User CreateUserLearnerAndEnroll(string userId, string email, int classroomid)
        //{
        //    this._context.Users.Add(new Entities.User
        //    {
        //        Id = userId,
        //        Description = email,
        //        LearnerDetails = new LearnerDetail()
        //        {
        //            Name = email,



        //        }

        //        //Name = name
        //        //Description = country
        //    });
        //    _context.SaveChanges();
        //    User user = _context.Users.Where(x => x.Id == userId).Include(x => x.LearnerDetails).FirstOrDefault();
        //    //Check for null etc
        //    Enrollment newEnrollment = new Enrollment { ClassroomID = classroomid };
        //    _context.SaveChanges();
        //    return _context.Users.Where(c => c.Id == userId).FirstOrDefault();


        //}

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

