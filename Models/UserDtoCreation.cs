﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Models
{
    public class UserDtoCreation
    {
        public string Id { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }

        public string Authorization { get; set;  }


        public LearnerDetailsDto LearnerDetails { get; set; }

        public EducatorDetailsDto EducatorDetails { get; set; }

    }
}
