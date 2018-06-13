using Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class ClassroomDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public ICollection<Enrollment> Enrollment { get; set; }
            = new List<Enrollment>();

        public List<StudentInvitationDto> StudentInvitation { get; set; }
           = new List<StudentInvitationDto>();


        public EducatorDetail EducatorDetail { get; set; }

        public int EducatorDetailId { get; set; }



    }
}
