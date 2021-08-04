using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {

        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            //var degerler = db.TabloMusteriler.ToList();
            //return View(degerler);
            var degerler = from d in db.TabloMusteriler select d;
            if (!String.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MusteriAd.Contains(p));
                //İşlem 
            }
            return View(degerler.ToList());
        }
        [HttpGet]
        public ActionResult YeniMusteriEkle()
        {
            return View();
        }
        [HttpPost]

        public ActionResult YeniMusteriEkle(TabloMusteriler p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniMusteriEkle");
            }
            db.TabloMusteriler.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil(int id)
        {
            var musteri = db.TabloMusteriler.Find(id);
            db.TabloMusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TabloMusteriler.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult Guncelle(TabloMusteriler p1)
        {
            var musteri = db.TabloMusteriler.Find(p1.MusteriID);
            musteri.MusteriAd = p1.MusteriAd;
            musteri.MusteriSoyad = p1.MusteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}