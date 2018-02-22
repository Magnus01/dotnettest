using Manager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class EducatorDetailsDto
    {
        public int Id { get; set; }

        //public int EducatorDetailsId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ClassroomDto> Classrooms { get; set; }
            = new List<ClassroomDto>();



    }
}
