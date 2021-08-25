﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiniUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string StudentName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}