using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;


namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.TabloUrunler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.TabloKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(TabloUrunler p1)
        {
            var ktg = db.TabloKategoriler.Where(m => m.KategoriID == p1.TabloKategoriler.KategoriID).FirstOrDefault();
            p1.TabloKategoriler = ktg;
            db.TabloUrunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.TabloUrunler.Find(id);
            db.TabloUrunler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TabloUrunler.Find(id);
            List<SelectListItem> degerler = (from i in db.TabloKategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir",urun);
        }
        public ActionResult Guncelle(TabloUrunler p)
        {
            var urun = db.TabloUrunler.Find(p.UrunID);
            urun.UrunAd = p.UrunAd;
            urun.Marka = p.Marka;
            urun.UrunStok = p.UrunStok;
            //urun.UrunKategori = p1.UrunKategori;
            urun.UrunFiyat = p.UrunFiyat;
            var ktg = db.TabloKategoriler.Where(m => m.KategoriID == p.TabloKategoriler.KategoriID).FirstOrDefault();
            urun.UrunKategori = ktg.KategoriID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}