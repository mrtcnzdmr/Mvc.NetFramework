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
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.Tbl_Urunler.ToList();
            var degerler = db.Tbl_Urunler.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(Tbl_Urunler p3)
        {

            var ktg = db.Tbl_Kategoriler.Where(m => m.KategoriId == p3.Tbl_Kategoriler.KategoriId).FirstOrDefault();
            p3.Tbl_Kategoriler = ktg;
            db.Tbl_Urunler.Add(p3);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            db.Tbl_Urunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGuncelle(int? id)
        {
            var urn = db.Tbl_Urunler.Find(id);

            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGuncelle", urn);
        }
        public ActionResult Guncelle(Tbl_Urunler p3)
        {
            var urun = db.Tbl_Urunler.Find(p3.UrunId);
            urun.UrunAd = p3.UrunAd;
            urun.UrunMarka = p3.UrunMarka;
            //urun.UrunKategori = p3.UrunKategori;
            var ktg = db.Tbl_Kategoriler.Where(m => m.KategoriId == p3.Tbl_Kategoriler.KategoriId).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriId;
            urun.UrunFiyat = p3.UrunFiyat;
            urun.UrunStok = p3.UrunStok;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}