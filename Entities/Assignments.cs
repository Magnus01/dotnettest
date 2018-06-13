using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Entities
{
    public class Assignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentID { get; set; }

        //public LearnerDetail LearnerDetails { get; set; }
        public Classroom Classroom { get; set; }

        public int ClassroomID { get; set; }
        //public int LearnerDetailsId { get; set; }

        public int BookId { get; set; }

        public int ChapterId { get; set; }

        public string Date {get; set; }

        public string Title { get; set; }




        //public Grade? Grade { get; set; }


    }
}
 