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
            var educatorusers = _context.Users.Include(c => c.EducatorDetails)
                .Where(c => c.Id == userId);

           return educatorusers.Include(c => c.EducatorDetails.Classrooms).FirstOrDefault();
            //return _context.Users.Include(c => c.EducatorDetails)
            //    .Where(c => c.Id == userId).FirstOrDefault();
        }
        


        public void AddUser(User user)
        {
            _context.Users.Add(user);
        } 
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.Name).ToList();
        }

        public User GetUser(string userId)
        {

            if (_context.Users.FirstOrDefault(c => c.Id == userId) != null)
            {
                return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
            }
            else
            {

                _context.Users.Add(new User
                {
                    Id = userId
                    //Name = name
                    //Description = country
                });
                _context.SaveChanges();
                return _context.Users.Where(c => c.Id == userId).FirstOrDefault();
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

        User IUserInfoRepository.GetEducatorDetailsForUser(string userId)
        {


            return _context.Users.Where(x => x.Id == userId).Include(x => x.EducatorDetails).FirstOrDefault();

        }




 
    }
}
