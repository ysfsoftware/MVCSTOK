using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TabloKategoriler.ToList();
            var degerler = db.TabloKategoriler.ToList().ToPagedList(sayfa,4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TabloKategoriler p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TabloKategoriler.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult Sil (int id)
        {
            var kategori = db.TabloKategoriler.Find(id);
            db.TabloKategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TabloKategoriler.Find(id);
            return View("KategoriGetir", ktgr);
        }
        public ActionResult Guncelle(TabloKategoriler p1)
        {
            var ktgr = db.TabloKategoriler.Find(p1.KategoriID);
            ktgr.KategoriAd = p1.KategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}