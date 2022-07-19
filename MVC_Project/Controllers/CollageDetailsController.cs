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
    public class CollageDetailsController : Controller
    {
        private admissionEntities db = new admissionEntities();

        // GET: CollageDetails
        public ActionResult Index()
        {
            return View(db.CollageDetails.ToList());
        }
        public ActionResult Error()
        {
            return View();
        }
        // GET: CollageDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollageDetail collageDetail = db.CollageDetails.Find(id);
            if (collageDetail == null)
            {
                return HttpNotFound();
            }
            return View(collageDetail);
        }
        public ActionResult ViewBranches(int? id)
        {
            if ((string)TempData.Peek("role") == "user")
            {
                return RedirectToAction("Branches", "BranchDetails", new { @id = id });
            }
            return RedirectToAction("Error");
        }
        public ActionResult viewstd()
        {
            return RedirectToAction("Index", "StudentRegistraions");
        }
       
        public ActionResult AddBranches(int? id)
        {
            if ((string)TempData.Peek("role") == "admin")
            {
                return RedirectToAction("AddBranches", "BranchDetails", new { @id = id });
            }
            return RedirectToAction("Error");

        }
        // GET: CollageDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollageDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CollageName,Addrress,Type,City,EstablishIn")] CollageDetail collageDetail)
        {
            if (ModelState.IsValid)
            {
                db.CollageDetails.Add(collageDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(collageDetail);
        }

        // GET: CollageDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollageDetail collageDetail = db.CollageDetails.Find(id);
            if (collageDetail == null)
            {
                return HttpNotFound();
            }
            return View(collageDetail);
        }

        // POST: CollageDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CollageName,Addrress,Type,City,EstablishIn")] CollageDetail collageDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collageDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(collageDetail);
        }

        // GET: CollageDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollageDetail collageDetail = db.CollageDetails.Find(id);
            if (collageDetail == null)
            {
                return HttpNotFound();
            }
            return View(collageDetail);
        }

        // POST: CollageDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollageDetail collageDetail = db.CollageDetails.Find(id);
            db.CollageDetails.Remove(collageDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
