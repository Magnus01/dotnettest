using Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager
{
    public static class UserInfoExtensions

    {
        public static void EnsureSeedDataForContext(this UserInfoContext context)
        {
            SeedUserData(context);
            //SeedClassroomData(context);

            context.SaveChanges();
        }
        private static void SeedUserData(UserInfoContext context)
        {
            //if (context.Users.Any())
            ////if (context.Users.Any())
            //{
            //    return;
            //}
            var users = new List<User>()
            {
                new User()
                {
                    Name = "New York User",
                    Description = "The one with the big park",
                    EducatorDetails= new EducatorDetail()
                    {
                        Name = "ed1",
                        Description= "ed2",
                        Classrooms = new List<Classroom>()
                        {
                         new Classroom()
                         {
                             Name= "clas1",
                             Description = "class description"
                         },
                            new Classroom()
                         {
                             Name= "clas1",
                             Description = "class description"
                         },
                               new Classroom()
                         {
                             Name= "clas1",
                             Description = "class description"
                         },
                                  new Classroom()
                         {
                             Name= "clas1",
                             Description = "class description"
                         },
                                     new Classroom()
                         {
                             Name= "clas1",
                             Description = "class description"
                         },
                    }
                    },
                   LearnerDetails = new LearnerDetail()
                    {

                        Name = "Learner1",
                        Subscribed= false,
                        Enrollments= new List<Enrollment>()
                        {
                            //new Enrollment(){
                            //Name = "interest1",
                            //Description = "learner2"
                            //  }

                    }
                    },
                },
               
                   

            };

            context.Users.AddRange(users);
        }
        //private static void SeedClassroomData(UserInfoContext context)
        //{
          
        //    if (context.Classroom.Any())
        //        return;
        //    context.Classroom.Add(new Classroom { Name = "FirstClassroom" });
        //}
    }

    
}
