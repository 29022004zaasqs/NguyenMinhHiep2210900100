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
    public class NMH_SanPhamsController : Controller
    {
        private QLBH_NMH2210900100Entities db = new QLBH_NMH2210900100Entities();

        // GET: NMH_SanPhams
        public ActionResult Index()
        {
            return View(db.SanPham.ToList());
        }

        // GET: NMH_SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            var sanPham = GetSanPhamById(id);
            if (sanPham == null) return HttpNotFound();
            return View(sanPham);
        }

        // GET: NMH_SanPhams/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,TenSanPham,Gia,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid && ValidateSanPham(sanPham))
            {
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: NMH_SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            var sanPham = GetSanPhamById(id);
            if (sanPham == null) return HttpNotFound();
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,TenSanPham,Gia,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid && ValidateSanPham(sanPham))
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sanPham);
        }

        // GET: NMH_SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            var sanPham = GetSanPhamById(id);
            if (sanPham == null) return HttpNotFound();
            return View(sanPham);
        }

        // POST: NMH_SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var sanPham = db.SanPham.Find(id);
            try
            {
                db.SanPham.Remove(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Cannot delete this product as it is referenced in other records.");
                Console.WriteLine(ex.Message);  // Optional: For debugging purposes
                return View(sanPham);
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

        // Helper method to retrieve product by ID with error handling
        private SanPham GetSanPhamById(int? id)
        {
            if (id == null) return null;
            return db.SanPham.Find(id);
        }

        // Additional validation for SanPham properties
        private bool ValidateSanPham(SanPham sanPham)
        {
            if (sanPham.Gia < 0)
            {
                ModelState.AddModelError("Gia", "Price must be a positive number.");
                return false;
            }
            if (sanPham.SoLuong < 0)
            {
                ModelState.AddModelError("SoLuong", "Quantity cannot be negative.");
                return false;
            }
            return true;
        }
    }

}
