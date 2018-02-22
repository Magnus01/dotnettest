using Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services
{
    public interface IUserInfoRepository
    {


        IEnumerable<User> GetUsers();

    


        User GetEducatorDetailsForUser(string userId);

 

        User GetLearnerDetailsForUser(string userId);



        LearnerDetail GetLearnerDetailEnrollmentForUser(int cityId, int EnrollmentId);
        

        //EducatorDetail GetEducatorClassForUser(string userId);

        void AddUser(User user);

        bool Save();

        bool UserExists(string userId);

        User GetUser(string userId);


        //IEnumerable<Classroom> GetClassroomsForUser(string userId);
        

    }
}
