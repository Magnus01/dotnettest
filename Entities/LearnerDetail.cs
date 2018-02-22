using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Manager.Entities;


namespace Manager.Entities
{
    public class LearnerDetail 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(10)]
        public bool Subscribed { get; set; }

        //public User User { get; set; }

        //public int UserId { get; set; }

        //public User User { get; set; }

        //public int UserId { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
            = new List<Enrollment>();

    }
}
