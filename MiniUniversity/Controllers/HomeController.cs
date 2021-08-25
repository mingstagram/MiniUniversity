using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniUniversity.DAL;
using MiniUniversity.ViewModels;

namespace MiniUniversity.Controllers
{
    public class HomeController : Controller
    {
        // 데이터베이스 컨텍스트를 담기 위한 클래스 변수 추가
        private SchoolContext db = new SchoolContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // 수강일자를 기준으로 Student 엔터티들을 그룹핑하고, 각 그룹들에 포함된 엔터티들의 개수를 계산한 다음,
            // 그 결과를 EnrollmentDateGroup 뷰 모델 개체들의 컬랙션에 담는다
            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                   group student by student.EnrollmentDate into dateGroup
                                                   select new EnrollmentDateGroup()
                                                   {
                                                       EnrollmentDate = dateGroup.Key,
                                                       StudentCount = dateGroup.Count()
                                                   };

            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}