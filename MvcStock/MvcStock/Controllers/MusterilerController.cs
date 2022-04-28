using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    public class MusterilerController : Controller
    {
        // GET: Musteriler
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index(int sayfa =1)
        {
            //var degerler = db.Tbl_Musteriler.ToList();
            var degerler = db.Tbl_Musteriler.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniMusteri(Tbl_Musteriler p2)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Tbl_Musteriler.Add(p2);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var musteri = db.Tbl_Musteriler.Find(id);
            db.Tbl_Musteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGuncelle(int id)
        {
            var musteriler = db.Tbl_Musteriler.Find(id);
            return View("MusteriGuncelle", musteriler);
        }
        public ActionResult Guncelle(Tbl_Musteriler p2)
        {
            var musteri = db.Tbl_Musteriler.Find(p2.MusteriId);
            musteri.MusteriAd = p2.MusteriAd;
            musteri.MusteriSoyad = p2.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}