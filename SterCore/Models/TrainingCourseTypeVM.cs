﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class TrainingCourseTypeVM
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa:")]
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
