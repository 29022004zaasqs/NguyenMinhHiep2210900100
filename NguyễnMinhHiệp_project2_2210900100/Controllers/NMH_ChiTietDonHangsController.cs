using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NguyễnMinhHiệp_project2_2210900100.Models;

namespace NguyễnMinhHiệp_project2_2210900100.Controllers
{
    public class NMH_ChiTietDonHangsController : Controller
    {
        private QLBH_NMH2210900100Entities db = new QLBH_NMH2210900100Entities();

        // GET: NMH_ChiTietDonHangs
        public ActionResult Index()
        {
            var chiTietDonHang = db.ChiTietDonHang.Include(c => c.DonHang).Include(c => c.SanPham);
            return View(chiTietDonHang.ToList());
        }

        // GET: NMH_ChiTietDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHang.Find(id);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // GET: NMH_ChiTietDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.DonHang, "MaDonHang", "MaDonHang");
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: NMH_ChiTietDonHangs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,MaSanPham,SoLuong,GiaBan")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verify MaDonHang exists in DonHang
                    if (!db.DonHang.Any(d => d.MaDonHang == chiTietDonHang.MaDonHang))
                    {
                        ModelState.AddModelError("MaDonHang", "Invalid MaDonHang. Please select a valid order.");
                        ViewBag.MaDonHang = new SelectList(db.DonHang, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
                        ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", chiTietDonHang.MaSanPham);
                        return View(chiTietDonHang);
                    }

                    // Verify MaSanPham exists in SanPham
                    if (!db.SanPham.Any(s => s.MaSanPham == chiTietDonHang.MaSanPham))
                    {
                        ModelState.AddModelError("MaSanPham", "Invalid MaSanPham. Please select a valid product.");
                        ViewBag.MaDonHang = new SelectList(db.DonHang, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
                        ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", chiTietDonHang.MaSanPham);
                        return View(chiTietDonHang);
                    }

                    db.ChiTietDonHang.Add(chiTietDonHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the data. Please try again.");
                    Console.WriteLine(ex.InnerException?.Message);  // For debugging
                }
            }

            ViewBag.MaDonHang = new SelectList(db.DonHang, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", chiTietDonHang.MaSanPham);
            return View(chiTietDonHang);
        }

        // POST: NMH_ChiTietDonHangs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaSanPham,SoLuong,GiaBan")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(chiTietDonHang).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating the data. Please try again.");
                    Console.WriteLine(ex.InnerException?.Message);  // For debugging
                }
            }

            ViewBag.MaDonHang = new SelectList(db.DonHang, "MaDonHang", "MaDonHang", chiTietDonHang.MaDonHang);
            ViewBag.MaSanPham = new SelectList(db.SanPham, "MaSanPham", "TenSanPham", chiTietDonHang.MaSanPham);
            return View(chiTietDonHang);
        }
    }
}