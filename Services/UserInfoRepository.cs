using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manager.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manager.Services
{
    public class UserInfoRepository : IUserInfoRepository
    {

        private UserInfoContext _context;

        public UserInfoRepository(UserInfoContext context)
        {
            _context = context;
        }

        public LearnerDetail GetLearnerDetailEnrollmentForUser(int cityId, int EnrollmentId)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(string userId)
        {
            return _context.Users.Any(c => c.Id == userId);
        }



        public User GetLearnerDetailsForUser(string userId)
        {

            return _context.Users.Include(c => c.LearnerDetails)
                .Where(c => c.Id == userId).FirstOrDefault();
        }


        public User GetEducatorDetailsForUser(string userId)
        {
            var educatorusers = _context.Users.Where(c => c.Id == userId)
                .Include("EducatorDetails.Classrooms");
            //.Include("EducatorDetails.Classrooms.StudentInvitation");
 
            return educatorusers.FirstOrDefault();
           //return edukcatorusers.Include(c => c.EducatorDetails.Classrooms).FirstOrDefault();
            //return _context.Users.Include(c => c.EducatorDetails)
            //    .Where(c => c.Id == userId).FirstOrDefault();
        }

        public User GetStudentInvitationDetailsForUser(string userId)
        {
            var educatorusers = _context.Users.Where(c => c.Id == userId)
            .Include("EducatorDetails.Classrooms.StudentInvitation");

            return educatorusers.FirstOrDefault();
            //return edukcatorusers.Include(c => c.EducatorDetails.Classrooms).FirstOrDefault();
            //return _context.Users.Include(c => c.EducatorDetails)
            //    .Where(c => c.Id == userId).FirstOrDefault();
        }


        //public User GetClassroomDetailsInvitation(string userId)
        //{
        //    var educatorusers = _context.Users.Include(c => c.EducatorDetails)
        //        .FirstOrDefault(c => c.Id == userId).EducatorDetails;

        //    return educatorusers.Classrooms.;
        //    //return _context.Users.Include(c => c.EducatorDetails)
        //    //    .Where(c => c.Id == userId).FirstOrDefault();
        //}


        public void AddUser(Entities.User user)
        {
            _context.Users.Add(user);
        }

        //public void CreateClassroomFunction(string userId, Classroom classroomDetails)
        //{
        //    User user = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();
        //     user.EducatorDetails.Classrooms.Add(
        //    new Classroom()
        //{
        //    Name = classroomDetails.Name,
        //        Description = classroomDetails.Description,
        //    }
        //    );
        //}

        public void CreateClassroomfunction(string userId, Classroom classroom)
        {
            User user = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();
            user.EducatorDetails.Classrooms.Add(classroom);
            //_context.SaveChanges();
            //user.EducatorDetails.Enrollments.Add(newEnrollment);

        }

        //public void AddUser(User user)
        //{
        //    _context.Users.Add(user);
        //}

        //Classroom IUserInfoRepository.CreateClassroomfunction(string userId, Classroom classroom)
        //Classroom IUserInfoRepository.CreateClassroomfunction(string userId, Classroom classroomDetails)
        //{
        //    User user = _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();
        //    return user.EducatorDetails.Classrooms.Add(
        //    new Classroom()
        //    {
        //        Name = classroomDetails.Name,
        //        Description = classroomDetails.Description,
        //    }
        //    );

        //}

        //         {
        //        var finalUser = Mapper.Map<User>(uservariable);

        //    _userInfoRepository.AddUser(finalUser);



        //        if (!_userInfoRepository.Save())
        //        {
        //            return StatusCode(500, "problem");
        //}

        //var createdUserToReturn = Mapper.Map<Models.UserDto>(finalUser);




        //Check for null etC















        public IEnumerable<Entities.User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.Name).ToList();
        }

        //public IEnumerable<StudentInvitation> GetClassrooms(string userId, int classroomid)
        //{
        //    _context.StudentInvitation.Where(c => c.ClassroomID == classroomid);
            
        //    if (_context.Users.FirstOrDefault(c => c.Id == userId).EducatorDetails.Classrooms != null)
        //    {
        //        return _context.Users.FirstOrDefault(c => c.Id == userId).EducatorDetails.Classrooms.GetEnumerator().Current.StudentInvitation;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //    //return _context.Users.OrderBy(c => c.Name).ToList();
        //}


        ////try2
        //public IEnumerable<Classroom> GetClassrooms(string userId)
        //{
        //    //if (_context.Users.FirstOrDefault(c => c.Id == userId).EducatorDetails.Id != null)
        //    //{
        //        return _context.Users.FirstOrDefault(c => c.Id == userId).EducatorDetails.Classrooms.ToList();
        //    //}
        //    //else return null;

        //    //return _context.Users.Include(c => c.EducatorDetails.Classrooms)
        //    //    .Where(c => c.Id == userId).FirstOrDefault();


        //    //return _context.EducatorDetails.Where(x => x.Id == userId).Include(x => x.Classroom).FirstOrDefault();
        //    //.Where(x => x.Id == userId).Include(x => x.Classroom).FirstOrDefault();
        //}
        //public IEnumerable<Classroom> GetClassrooms(int educatorId)
        //{
        //    return _context.EducatorDetails.Include(c => c.Classrooms)
        //        .SingleOrDefault(c => c.Id == educatorId)?.Classrooms;


        //    //null propagation "?" if it returns null if the userId doesnt exist
        //    //if whats before the questions mark is null, return null (object exists checking)
        //}

        public IEnumerable<Classroom> GetStudentInvitations(int educatorId, int classroomid)
        {

            return _context.EducatorDetails.Include(c => c.Classrooms)
                .SingleOrDefault(c => c.Id == educatorId)?.Classrooms;
                
                

            //null propagation "?" if it returns null if the userId doesnt exist
            //if whats before the questions mark is null, return null (object exists checking)
        }




        //User IUserInfoRepository.GetEducatorDetailsForUser(string userId)
        //{


        //    return _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();

        //}



        //public IEnumerable<Classroom> GetClassrooms()
        //{
        //    throw new NotImplementedException();
        //}


        public User GetUser(string userId)
        {

            if (_context.Users.FirstOrDefault(c => c.Id == userId) != null)
            {
                return _context.Users.Where(c => c.Id == userId).Include(c => c.EducatorDetails).FirstOrDefault();


            }
            else
            {
                return null;
                //this._context.Users.Add(new Entities.User
                //{
                //    Id = userId
                //    //Name = name
                //    //Description = country
                //});
                //_context.SaveChanges();
                //return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
            }
            //return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }



        public User GetorCreateUser(string userId, string email)
        {

            if (_context.Users.FirstOrDefault(c => c.Id == userId) != null)
            {
                return _context.Users.Where(c => c.Id == userId).Include(c => c.EducatorDetails).FirstOrDefault();


            }
            else {
                this._context.Users.Add(new Entities.User
                {
                    Id = userId,
                    Description = email
                    //Name = name
                    //Description = country
                });
                _context.SaveChanges();
                return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
            }
        }


        public User CreateUser(string userId, string email, string authorization)
        {
            //not sure if thats the email its actualy the description on the sending post
                this._context.Users.Add(new Entities.User
                {
                    Id = userId,
                    Description = email,
                    Authorization = authorization
                    //Name = name
                    //Description = country
                });
                _context.SaveChanges();
                return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }























        public User CreateUserLearnerAndEnroll(string userId, string email, int classroomid)
        {
            this._context.Users.Add(new Entities.User
            {
                Id = userId,
                Description = email,
                LearnerDetails = new LearnerDetail()
                {
                    Name = email,



                }

                //Name = name
                //Description = country
            });
            _context.SaveChanges();
            User user = _context.Users.Where(x => x.Id == userId).Include(x => x.LearnerDetails).FirstOrDefault();
            //Check for null etc
            Enrollment newEnrollment = new Enrollment { ClassroomID = classroomid };
            _context.SaveChanges();
            return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
          
         
        }
        //return _context.Users.Where(c => c.Id == userId).FirstOrDefault();



        //check to see if user is in the database
        public User ReturnUserorNull(string userId)
        {

            if (_context.Users.FirstOrDefault(c => c.Id == userId) != null)
            {
                return _context.Users.Where(c => c.Id == userId).Include(c => c.EducatorDetails).FirstOrDefault();


            }
            else
            {

                return null;
            }
            //return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }

        public User ReturnEmailorNull(string email)
        {

            if (_context.Users.FirstOrDefault(c => c.Description == email) != null)
            {
                return _context.Users.Where(c => c.Description == email).Include(c => c.LearnerDetails).FirstOrDefault();


            }
            else
            {

                return null;
            }
            //return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }
        


        //check to see if user is in the invite table
        public StudentInvitation ReturnInviteorNull(string email)
        {

            if (_context.StudentInvitation.FirstOrDefault(c => c.Email == email) != null)
            {
                return _context.StudentInvitation.Where(c => c.Email == email).FirstOrDefault();


            }
            else
            {

                return null;
            }
            //return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
        }


   










        public bool Save()
        {
            //want to return true when 0 or more entities have succes saved
            return (_context.SaveChanges() >= 0);
        }


        User IUserInfoRepository.GetLearnerDetailsForUser(string userId)
        {


           return _context.Users.Where(x => x.Id == userId).Include(x => x.LearnerDetails).FirstOrDefault();

        }
        
        //public IEnumerable<Classroom> GetClassrooms(int educatorId)
        //{
        //    throw new NotImplementedException();
        //}

        //User IUserInfoRepository.GetEducatorDetailsForUser(string userId)
        //{


        //    return _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();

        //}


    }
}
