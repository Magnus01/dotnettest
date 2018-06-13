using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Entities
{
    public class Classroom
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

 


        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        public ICollection<Enrollment> Enrollment { get; set; }
            = new List<Enrollment>();


        public ICollection<StudentInvitation> StudentInvitation { get; set; }
            = new List<StudentInvitation>();

        public ICollection<Assignment> Assignment { get; set; }
      = new List<Assignment>();

        public EducatorDetail EducatorDetail { get; set; }

        public int EducatorDetailId { get; set; }
    }
}
