namespace MiniUniversity.Migrations
{
    using MiniUniversity.DAL;
    using MiniUniversity.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MiniUniversity.DAL.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        // Code First가 데이터베이스를 생성하거나 갱신한 뒤에 데스트 데이터를 입력하거나 갱신하기 위함
        // 데이터베이스 컨텍스트 개체를 입력 매개변수로 받은 다음, 이 개체를 이용해서 데이터베이스에 새로운 엔터티들을 추가
        protected override void Seed(MiniUniversity.DAL.SchoolContext context)
        {
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
            students.ForEach(s => context.Students.AddOrUpdate(p => p.StudentName, s));
            context.SaveChanges();

            var instructors = new List<Instructor>
            {
                new Instructor { InstructorName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { InstructorName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { InstructorName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { InstructorName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { InstructorName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            instructors.ForEach(s => context.Instructors.AddOrUpdate(p => p.InstructorName, s));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.InstructorName == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.InstructorName == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.InstructorName == "Harui").ID },
                new Department { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    InstructorID = instructors.Single(i => i.InstructorName == "Kapoor").ID }
            };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
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
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
            {
                new OfficeAssignment {
                    InstructorID = instructors.Single(i => i.InstructorName == "Fakhouri").ID,
                    Location = "Smith 17" },
                new OfficeAssignment {
                    InstructorID = instructors.Single(i => i.InstructorName == "Harui").ID,
                    Location = "Gowan 27" },
                new OfficeAssignment {
                    InstructorID = instructors.Single(i => i.InstructorName == "Kapoor").ID,
                    Location = "Thompson 304" },
            };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.InstructorID, s));
            context.SaveChanges();

            AddOrUpdateInstructor(context, "Chemistry", "Kapoor");
            AddOrUpdateInstructor(context, "Chemistry", "Harui");
            AddOrUpdateInstructor(context, "Microeconomics", "Zheng");
            AddOrUpdateInstructor(context, "Macroeconomics", "Zheng");

            AddOrUpdateInstructor(context, "Calculus", "Fakhouri");
            AddOrUpdateInstructor(context, "Trigonometry", "Harui");
            AddOrUpdateInstructor(context, "Composition", "Abercrombie");
            AddOrUpdateInstructor(context, "Literature", "Abercrombie");

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

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s => s.Student.ID == e.StudentID &&
                         s.Course.CourseID == e.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }

        void AddOrUpdateInstructor(SchoolContext context, string courseTitle, string instructorName)
        {
            var crs = context.Courses.SingleOrDefault(c => c.Title == courseTitle);
            var inst = crs.Instructors.SingleOrDefault(i => i.InstructorName == instructorName);
            if (inst == null)
                crs.Instructors.Add(context.Instructors.Single(i => i.InstructorName == instructorName));
        }
    }
}
