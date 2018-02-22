using Manager.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class LearnerDetailsDto
    {

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        //public int LearnerId { get; set; }

        public string Name { get; set; }

        
        public bool Subscribed { get; set; }

        //public User User { get; set; }

        //public int UserId { get; set; }


        //public ICollection<InterestDto> InterestDtos { get; set; }
        //    = new List<InterestDto>();


        //public int Calculated
        //{
        //    get
        //    {
        //        return InterestDtos.Count;
        //    }
        //}
    }
}
