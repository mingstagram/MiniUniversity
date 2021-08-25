using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MiniUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }

        // 한 학과에는 관리자가 존재할 수도 또는 존재하지 않을 수도 있으며, 관리자는 항상 강사
        public virtual Instructor Administrator { get; set; }
        // 한 학과에는 여러 가지 종류의 강의가 존재할 수 있으므로, 컬렉션 형태
        public virtual ICollection<Course> Courses { get; set; }
    }
}