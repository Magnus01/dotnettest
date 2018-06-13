using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Entities
{
    public class StudentInvitation
    {

        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { get; set; }


        //public int StudentInvitationID { get; set; }

        //public Classroom Classroom { get; set; }
        
        //public string Email { get; set; }

        //public int ClassroomID { get; set; }


        public int StudentInvitationID { get; set; }

        //public LearnerDetail LearnerDetails { get; set; }
        public string Email { get; set; }
        public Classroom Classroom { get; set; }

        public int ClassroomID { get; set; }
        //public int LearnerDetailsId { get; set; }



    }
}