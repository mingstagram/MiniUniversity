using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MiniUniversity.DAL;
using MiniUniversity.Models;
using PagedList;

namespace MiniUniversity.Controllers
{
    public class StudentController : Controller
    {
        // 데이터베이스 컨텍스트 개체의 인스턴스를 담고 있는 클래스 변수
        private SchoolContext db = new SchoolContext();

        // GET: Student
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        // 페이지가 최초로 출력되거나 사용자가 페이징 링크나 정렬 링크를 출력하지 않은 경우에는 모든 매개변수들의 값이 null
        // 페이징 링크가 클릭되면 page 변수에 출력될 페이지의 번호가 담겨진다.
        {
            // 페이징 처리를 위해 PagedList.MVC NuGet 패키지 설치
            // 페이징 중에도 동일한 정렬 순서를 유지하기 위해서는 페이징 링크에 정렬 순서 정보가 함께 포함
            ViewBag.CurrentSort = sortOrder;
            // sortOrder의 값이 null이거나 빈 문자열이면 ViewBag.NameSortParm 변수를 "name_desc"로 설정하고, 아니면 빈 문자열로 설정
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString; // 현재 지정된 필터 문자열 값을 뷰에 제공


            var students = from s in db.Students
                           select s;
            // 검색할 값이 존재하는 경우에만 실행
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.StudentName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.StudentName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.StudentName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1); // page 변수의 값이 null이 아니면 그 값을 반환하고, 그 값이 null이면 1을 반환한다는 뜻
            return View(students.ToPagedList(pageNumber, pageSize));

        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken] // 크로스 사이트 요청 위조(Cross-Site Request Forgery) 공격을 방지
        public ActionResult Create([Bind(Include = "StudentName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                // 변경 사항들이 저장되는 도중에 DataException에서 파생된 예외가 던져지면 포광적인 오류메시지가 출력되는데
                // DataException 예외는 프로그래밍 오류보다는 무언가 응용 프로그램 외부의 요인으로 인해서 발생하는 경우가 많으므로,
                // 작업을 다시 시도하도록 사용자를 유도하는 메시지를 출력
                ModelState.AddModelError("", "변경 사항을 저장할 수 없습니다. 다시 시도하고 문제가 지속되면 시스템 관리자에게 문의하십시오.");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var studentToUpdate = db.Students.Find(id);
            if (TryUpdateModel(studentToUpdate, "", new string[] { "LastName", "FirstMidName", "EnrollmentDate" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "변경 사항을 저장할 수 없습니다. 다시 시도하고 문제가 지속되면 시스템 관리자에게 문의하십시오.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            // 삭제 작업의 실패 없이 HttpGet Delete 메서드가 호출된 경우에는 false로 설정되고, 데이터베이스 갱신
            // 실패로 인해서 HttpPost Delete 메서드에서 호출된 경우에만 true로 설정되어 뷰에 오류 메시지가 전달
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "삭제하지 못했습니다. 다시 시도하고 문제가 지속되면 시스템 관리자에게 문의하십시오.";
            }

            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                // 선택된 엔터티를 조회
                Student student = db.Students.Find(id);
                //  Remove 메서드를 호출해서 엔터티의 상태를 Deleted로 설정
                db.Students.Remove(student);
                // 그리고 SaveChanges 메서드가 호출되면 SQL DELETE 명령이 생성
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            { 
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 데이터베이스 연결 닫기
        /// </summary>
        /// 사용 중인 리소스들을 최대한 빨리 해제하려면, 데이터베이스 컨텍스트를 이용한 작업이 끝나는 즉시 인스턴스를 삭제
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
