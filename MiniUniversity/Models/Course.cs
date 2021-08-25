using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiniUniversity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        // 강의는 한 학과에 배정
        public virtual Department Department { get; set; }
        // 강의를 수강할 수 있는 학생들의 수에는 제한이 없기 때문에 컬렉션
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        // 강의는 여러 강사에 의해서 진행될 수 있으므로 컬렉션
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}