using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Manager.Entities;


namespace Manager.Entities
{
    public class EducatorDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }


        public ICollection<Classroom> Classrooms { get; set; }
        = new List<Classroom>();
   
        //[Required]
        //[MaxLength(50)]
        //public List<Classroom> Classroom { get; set; }
            //= new List<Classroom>();

        //public User User { get; set; }

        //public int UserId { get; set; }

    }
}
