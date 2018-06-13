using Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Services
{
    public interface IUserInfoRepository
    {


        IEnumerable<Entities.User> GetUsers();

        //IEnumerable<Entities.StudentInvitation> GetClassrooms(string userId);


        User GetEducatorDetailsForUser(string userId);
        User GetStudentInvitationDetailsForUser(string userId); 



        User GetLearnerDetailsForUser(string userId);



        LearnerDetail GetLearnerDetailEnrollmentForUser(int cityId, int EnrollmentId);


        //EducatorDetail GetEducatorClassForUser(string userId);

        void AddUser(Entities.User user);

        bool Save();

        bool UserExists(string userId);

        User GetUser(string userId);

        User GetorCreateUser(string userId, string email);

        User ReturnUserorNull(string userId);

        StudentInvitation ReturnInviteorNull(string userId);

        User CreateUser(string userId, string email, string Authorization);

        User ReturnEmailorNull(string email);

        void CreateClassroomfunction(string userId, Classroom classroom);


        User CreateUserLearnerAndEnroll(string userId, string email, int classroomid);



        //IEnumerable<Classroom> GetClassroomsForUser(string userId);


    }
}
