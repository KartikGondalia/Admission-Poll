using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class StudentRegistraionsController : Controller
    {
        private admissionEntities db = new admissionEntities();

        // GET: StudentRegistraions
        public ActionResult Index()
        {
            if ((string)TempData.Peek("role") == "admin")
            {
                return View(db.StudentRegistraions.ToList());
            }
            return RedirectToAction("Error");
        }

        // GET: StudentRegistraions/Details/5
        public ActionResult Details(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentRegistraion studentRegistraion = db.StudentRegistraions.Find(id);
                if (studentRegistraion == null)
                {
                    return HttpNotFound();
                }
                return View(studentRegistraion);
            

        }

        // GET: StudentRegistraions/Create
        public ActionResult Register()
        {
            
                return View();
            

        }

        // POST: StudentRegistraions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,FirstName,LastName,Addrress,Total,Email,Password,Contact")] StudentRegistraion studentRegistraion)
        {
            if (ModelState.IsValid)
            {
                db.StudentRegistraions.Add(studentRegistraion);
                db.SaveChanges();
                TempData["role"] = "user";
                TempData["isLoggedIn"] = true;
                TempData["id"] = studentRegistraion.Id;
                TempData["name"] = studentRegistraion.FirstName;
                return RedirectToAction("Index","CollageDetails");
            }

            return View(studentRegistraion);
        }

        // GET: StudentRegistraions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (TempData.ContainsKey("isLoggedIn"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentRegistraion studentRegistraion = db.StudentRegistraions.Find(id);
                if (studentRegistraion == null)
                {
                    return HttpNotFound();
                }
                return View(studentRegistraion);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: StudentRegistraions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Addrress,Total,Email,Password,Contact")] StudentRegistraion studentRegistraion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentRegistraion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentRegistraion);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection form)
        {
            string mail = form["Email"].ToString();
            string pass = form["Password"].ToString();
            if(mail=="kartik@gmail.com" && pass=="123456")
            {
                TempData["role"] = "admin";
                TempData["name"] = "Admin";
                TempData["id"] = 1;
                TempData["isLoggedIn"] = true;
                return RedirectToAction("Index", "CollageDetails");
            }
            StudentRegistraion s = db.StudentRegistraions.Where(p => p.Email.Equals(mail)).ToList().FirstOrDefault();
            if (s != null && s.Password == pass)
            {
                TempData["role"] = "user";
                TempData["name"] = s.FirstName;
                TempData["id"] = s.Id;
                TempData["isLoggedIn"] = true;
                return RedirectToAction("Index", "CollageDetails");

            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        public ActionResult prof()
        {
            int? id = null;
            if(TempData.ContainsKey("isLoggedIn"))
            {
                id = (int)TempData.Peek("id");
            }
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRegistraion studentRegistraion = db.StudentRegistraions.Find(id);
            if (studentRegistraion == null)
            {
                return HttpNotFound();
            }
            return View(studentRegistraion);


        }
        public ActionResult Error()
        {
            return View();
        }
        // GET: StudentRegistraions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRegistraion studentRegistraion = db.StudentRegistraions.Find(id);
            if (studentRegistraion == null)
            {
                return HttpNotFound();
            }
            return View(studentRegistraion);
        }

        // POST: StudentRegistraions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentRegistraion studentRegistraion = db.StudentRegistraions.Find(id);
            db.StudentRegistraions.Remove(studentRegistraion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Logoff()
        {
            TempData.Clear();
            return RedirectToAction("Login");
        }

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
