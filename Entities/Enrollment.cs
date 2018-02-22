using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Entities
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
     
        public LearnerDetail LearnerDetails { get; set; }
        public Classroom Classroom { get; set; }

        public int ClassroomID { get; set; }
        public int LearnerDetailsId { get; set; }

        //public Grade? Grade { get; set; }


    }
}
 