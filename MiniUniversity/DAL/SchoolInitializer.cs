using MiniUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiniUniversity.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            // 입력 매개변수로 전달받은 데이터베이스 컨텍스트 개체를 이용해서 데이터베이스에 새로운 엔터티들을 추가
            
            var students = new List<Student>
            {
                new Student{StudentName="유재석",EnrollmentDate=DateTime.Parse("2020-09-01")},
                new Student{StudentName="박명수",EnrollmentDate=DateTime.Parse("2020-09-01")},
                new Student{StudentName="정형돈",EnrollmentDate=DateTime.Parse("2018-09-01")},
                new Student{StudentName="정준하",EnrollmentDate=DateTime.Parse("2020-09-01")},
                new Student{StudentName="하동훈",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{StudentName="노홍철",EnrollmentDate=DateTime.Parse("2019-09-01")},
                new Student{StudentName="길성준",EnrollmentDate=DateTime.Parse("2020-09-01")},
                new Student{StudentName="양세형",EnrollmentDate=DateTime.Parse("2019-09-01")}
            };
            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{CourseID=1050,Title="화학",Credits=3,},
                new Course{CourseID=4022,Title="미시경제학",Credits=3,},
                new Course{CourseID=4041,Title="거시경제학",Credits=3,},
                new Course{CourseID=1045,Title="미적분학 ",Credits=4,},
                new Course{CourseID=3141,Title="통계학",Credits=4,},
                new Course{CourseID=2021,Title="작곡학",Credits=3,},
                new Course{CourseID=2042,Title="문학",Credits=4,}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
                new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
                new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
                new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
                new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
                new Enrollment{StudentID=3,CourseID=1050},
                new Enrollment{StudentID=4,CourseID=1050,},
                new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
                new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
                new Enrollment{StudentID=6,CourseID=1045},
                new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }

    }
}