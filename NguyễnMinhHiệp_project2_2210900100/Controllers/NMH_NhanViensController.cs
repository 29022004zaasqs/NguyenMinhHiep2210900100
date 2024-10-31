using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NguyễnMinhHiệp_project2_2210900100.Models;

namespace NguyễnMinhHiệp_project2_2210900100.Controllers
{
    public class NMH_NhanViensController : Controller
    {
        private QLBH_NMH2210900100Entities db = new QLBH_NMH2210900100Entities();

        // GET: NMH_NhanViens
        public ActionResult Index()
        {
            return View(db.NhanVien.ToList());
        }

        // GET: NMH_NhanViens/Details/5
        public ActionResult Details(int? id)
        {
            var nhanVien = GetNhanVienById(id);
            if (nhanVien == null) return HttpNotFound();
            return View(nhanVien);
        }

        // GET: NMH_NhanViens/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhanVien,TenNhanVien,ChucVu,Luong")] NhanVien nhanVien)
        {
            if (ModelState.IsValid && ValidateNhanVien(nhanVien))
            {
                db.NhanVien.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: NMH_NhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            var nhanVien = GetNhanVienById(id);
            if (nhanVien == null) return HttpNotFound();
            return View(nhanVien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhanVien,TenNhanVien,ChucVu,Luong")] NhanVien nhanVien)
        {
            if (ModelState.IsValid && ValidateNhanVien(nhanVien))
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhanVien);
        }

        // GET: NMH_NhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            var nhanVien = GetNhanVienById(id);
            if (nhanVien == null) return HttpNotFound();
            return View(nhanVien);
        }

        // POST: NMH_NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nhanVien = db.NhanVien.Find(id);
            try
            {
                db.NhanVien.Remove(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Cannot delete this employee because they are associated with other records.");
                Console.WriteLine(ex.Message);  // Optional: For debugging purposes
                return View(nhanVien);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Helper method to retrieve employee by ID with error handling
        private NhanVien GetNhanVienById(int? id)
        {
            if (id == null) return null;
            return db.NhanVien.Find(id) ?? null;
        }

        // Additional validation for NhanVien properties
        private bool ValidateNhanVien(NhanVien nhanVien)
        {
            if (nhanVien.Luong < 0)
            {
                ModelState.AddModelError("Luong", "Salary must be a positive number.");
                return false;
            }
            // Add any additional validation as needed
            return true;
        }
    }

}
