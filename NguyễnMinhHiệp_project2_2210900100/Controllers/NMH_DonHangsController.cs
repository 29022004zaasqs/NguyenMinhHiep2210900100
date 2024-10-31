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
    public class NMH_DonHangsController : Controller
    {
        private QLBH_NMH2210900100Entities db = new QLBH_NMH2210900100Entities();

        private void PopulateSelectLists(DonHang donHang = null)
        {
            ViewBag.MaKhachHang = new SelectList(db.KhachHang, "MaKhachHang", "TenKhachHang", donHang?.MaKhachHang);
            ViewBag.MaNhanVien = new SelectList(db.NhanVien, "MaNhanVien", "TenNhanVien", donHang?.MaNhanVien);
        }

        // GET: NMH_DonHangs/Create
        public ActionResult Create()
        {
            PopulateSelectLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,NgayDatHang,TongTien,MaKhachHang,MaNhanVien")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                if (IsForeignKeyValid(donHang))
                {
                    db.DonHang.Add(donHang);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid MaKhachHang or MaNhanVien. Please select valid values.");
                }
            }

            PopulateSelectLists(donHang);
            return View(donHang);
        }

        private bool IsForeignKeyValid(DonHang donHang)
        {
            return db.KhachHang.Any(k => k.MaKhachHang == donHang.MaKhachHang) &&
                   db.NhanVien.Any(n => n.MaNhanVien == donHang.MaNhanVien);
        }

        // POST: NMH_DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHang.Find(id);
            try
            {
                db.DonHang.Remove(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Cannot delete this DonHang. It may have related records.");
                Console.WriteLine(ex.Message);  // Log error for debugging
                return View(donHang);
            }
        }
    }

}
