using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Teacher_011.Models;
using Teacher_011.Repositories;
using Teacher_011.Repositories.Interfaces;
using Teacher_011.ViewModel;
using X.PagedList;

namespace Teacher_011.Controllers
{
    [Authorize]
    public class TeachersController : Controller
    {
        private readonly TeacherDbContext db = new TeacherDbContext();
        IGenericRepo<Teacher> repo;
        public TeachersController()
        {
            this.repo = new GenericRepo<Teacher>(db);
        }
        // GET: Teachers
        
        public ActionResult Index(int pg = 1)
        {
            // var data = await db.Teachers.OrderBy(a => a.TeacherId).ToPagedListAsync(pg, 5);
            var data = this.repo.GetAll("TeacherLogs").ToPagedList(pg, 5); //await db.Teachers.OrderBy(a => a.TeacherId).ToPagedListAsync(pg, 5);

            return View(data);
        }
        public ActionResult Create()
        {
            TeacherViewModel a = new TeacherViewModel();
            a.TeacherLogs.Add(new TeacherLog { });
            return View(a);

        }
        [HttpPost]
        public ActionResult Create(TeacherViewModel data, string act = "")
        {
            if (act == "add")
            {
                data.TeacherLogs.Add(new TeacherLog { });
                foreach(var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.TeacherLogs.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var a = new Teacher
                    {
                        TeacherName = data.TeacherName,
                        BirthDate = data.BirthDate,
                        CourseName = data.CourseName,
                        Gender = data.Gender,
                        ExpectedSalary=data.ExpectedSalary,
                        IsReadyToTeachAnySubject = data.IsReadyToTeachAnySubject

                    };
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Pictures/") + fileName;
                    data.Picture.SaveAs(savePath);
                    a.Picture = fileName;
                    foreach (var q in data.TeacherLogs)
                    {
                        a.TeacherLogs.Add(q);
                    }
                    this.repo.Insert(a);
                    return RedirectToAction("Index");
                }
            }
            return View(data);
        }
        public ActionResult Edit(int id)
        {
            var a = db.Teachers
              .Select(x => new TeacherEditModel
              {
                  TeacherId = x.TeacherId,
                  TeacherName = x.TeacherName,
                  BirthDate = x.BirthDate,
                  Gender = x.Gender,
                  CourseName = x.CourseName,
                  ExpectedSalary=x.ExpectedSalary,
                  IsReadyToTeachAnySubject = x.IsReadyToTeachAnySubject,
                  TeacherLogs = x.TeacherLogs.ToList()

              })
              .FirstOrDefault(x => x.TeacherId == id);
             ViewBag.CurrentPic = db.Teachers.First(x => x.TeacherId == id).Picture;

            return View(a);
        }
        [HttpPost]
        public ActionResult Edit(TeacherEditModel data, string act = "")
        {
            if (act == "add")
            {
                data.TeacherLogs.Add(new TeacherLog { });
            }
            if (act.StartsWith("remove"))
            {
                int i = int.Parse(act.Substring(act.IndexOf("_") + 1));
                var it = data.TeacherLogs.First(x => x.TeacherLogId == i);
                data.TeacherLogs.Remove(it);
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var a = db.Teachers.First(x => x.TeacherId == data.TeacherId);

                    a.TeacherName = data.TeacherName;
                    a.BirthDate = data.BirthDate;
                    a.CourseName = data.CourseName;
                    a.ExpectedSalary = data.ExpectedSalary;
                    a.Gender = data.Gender;
                    a.IsReadyToTeachAnySubject = data.IsReadyToTeachAnySubject;


                    if (data.Picture != null)
                    {
                        string ext = Path.GetExtension(data.Picture.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Pictures/") + fileName;
                        data.Picture.SaveAs(savePath);
                        a.Picture = fileName;
                    }
                    db.TeacherLogs.RemoveRange(db.TeacherLogs.Where(x => x.TeacherId == data.TeacherId).ToList());
                    foreach (var item in data.TeacherLogs)
                    {
                        a.TeacherLogs.Add(new TeacherLog
                        {
                            TeacherId = data.TeacherId,
                            InstitudeName = item.InstitudeName,
                            SubjcetTopic = item.SubjcetTopic,
                            CourseDuration = item.CourseDuration,
                            TeachingAbility = item.TeachingAbility
                        });
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CurrentPic = db.Teachers.First(x => x.TeacherId == data.TeacherId).Picture;
            return View(data);
        }
        [HttpPost]

        public ActionResult Delete(int id)
        {
            this.repo.ExecuteCommand($"EXECUTE dbo.DeleteTeacher {id}");
            return Json(new { success = true, id });
        }


        
    }
}
    