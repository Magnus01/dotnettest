using Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class StudentInvitationDto
    {
 
 

        public int StudentInvitationID { get; set; }
 
        //public Classroom Classroom { get; set; }
        
        public string Email { get; set; }

        public int ClassroomID { get; set; }
    

 

    }
}
