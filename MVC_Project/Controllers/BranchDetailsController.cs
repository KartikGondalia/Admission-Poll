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
    public class BranchDetailsController : Controller
    {
        private admissionEntities db = new admissionEntities();

        // GET: BranchDetails
        public ActionResult Index()
        {
            var branchDetails = db.BranchDetails.Include(b => b.CollageDetail);
            return View(branchDetails.ToList());
        }

        // GET: BranchDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchDetail branchDetail = db.BranchDetails.Find(id);
            if (branchDetail == null)
            {
                return HttpNotFound();
            }
            return View(branchDetail);
        }
        public ActionResult Branches(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<BranchDetail> branchDetail = db.BranchDetails.Where(t => t.CollageId == id).ToList();
          
            if (branchDetail == null)
            {
                return HttpNotFound();
            }
            return View(branchDetail);
        }
        public ActionResult AddBranches(int? id)
        {
            if ((string)TempData.Peek("role") == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                return RedirectToAction("Create");
            }
            return RedirectToAction("Error");
        }
        // GET: BranchDetails/Create
        public ActionResult Create()
        {
            ViewBag.CollageId = new SelectList(db.CollageDetails, "Id", "CollageName");
            return View();
        }

        // POST: BranchDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BranchName,Seats,CollageId,Fees")] BranchDetail branchDetail)
        {
            if (ModelState.IsValid)
            {
                db.BranchDetails.Add(branchDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollageId = new SelectList(db.CollageDetails, "Id", "CollageName", branchDetail.CollageId);
            return View(branchDetail);
        }

        // GET: BranchDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchDetail branchDetail = db.BranchDetails.Find(id);
            if (branchDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollageId = new SelectList(db.CollageDetails, "Id", "CollageName", branchDetail.CollageId);
            return View(branchDetail);
        }
        
        // POST: BranchDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BranchName,Seats,CollageId,Fees")] BranchDetail branchDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(branchDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollageId = new SelectList(db.CollageDetails, "Id", "CollageName", branchDetail.CollageId);
            return View(branchDetail);
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Admission(int? id)
        {
            if ((string)TempData.Peek("role") == "user")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                BranchDetail branchDetail = db.BranchDetails.Find(id);
                if (branchDetail == null)
                {
                    return HttpNotFound();
                }
                int seat = (int)branchDetail.Seats;
                int i = (int)TempData.Peek("id");
                StudentRegistraion s = db.StudentRegistraions.Where(m => m.Id == i).FirstOrDefault();

                if (seat > 0 && s.Contact.ToString() == "nottaken")
                {
                    int ns = seat - 1;
                    branchDetail.Seats = ns;
                    db.SaveChanges();
                    ViewBag.success = "You got the admission";
                    s.Contact = "taken";
                    db.SaveChanges();
                    return RedirectToAction("Details", "CollageDetails", new { @id = id });
                }
                else
                {
                    if (seat <= 0)
                    {
                        TempData["fail"] = "sorry, seat of your required branch is not avilable";
                    }
                    else
                    {
                        TempData["fail"] = "You have already entered your college";
                    }
                    return RedirectToAction("Index", "CollageDetails");
                }
            }
            return RedirectToAction("Error");
        }
        // GET: BranchDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchDetail branchDetail = db.BranchDetails.Find(id);
            if (branchDetail == null)
            {
                return HttpNotFound();
            }
            return View(branchDetail);
        }

        // POST: BranchDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BranchDetail branchDetail = db.BranchDetails.Find(id);
            db.BranchDetails.Remove(branchDetail);
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
