using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaloriTakip.Models;
using KaloriTakip.Models.Class;

namespace KaloriTakip.Controllers
{
    public class YoneticiController : Controller
    {
        // GET: Yonetici
        KaloriTakipEntities2 db = new KaloriTakipEntities2();
        Class2 cs2 = new Class2();
        [Authorize]
        public ActionResult Anasayfa()
        {
            return View();
        }

        public ActionResult KullaniciListele()
        {
            var degerler = db.tbl_Kullanici.ToList(); // kullanici tablosundaki verileri listele
            return View(degerler);
        }
        public ActionResult DiyetisyenListele()
        {
            var degerler = db.tbl_Diyetisyen.ToList(); // diyetsiyen tablosundaki verileri listele
            return View(degerler);
        }

        [HttpGet]
        public ActionResult DiyetisyenEkle()
        {
            cs2.Sehirler = new SelectList(db.iller, "id", "sehiradi");
            cs2.Ilceler = new SelectList(db.ilceler, "id", "ilceadi");
            return View(cs2);
        }
        [HttpPost]
        public ActionResult DiyetisyenEkle(tbl_Diyetisyen diyetisyen)
        {

            if (!ModelState.IsValid)
            {
                return View("DiyetisyenEkle");
            }
            cs2.Sehirler = new SelectList(db.iller, "id", "sehiradi");
            cs2.Ilceler = new SelectList(db.ilceler, "id", "ilceadi");
            db.tbl_Diyetisyen.Add(diyetisyen); // girilen bilgileri diyetisyen tablosuna ekle
            diyetisyen.Yetki = 2; // yetkisini 2 yap
            db.SaveChanges();
            return RedirectToAction("DiyetisyenListele", "Yonetici");
        }

        public JsonResult ilceGetir(int p)
        {
            var ilceler = (from x in db.ilceler
                           join y in db.iller on x.iller.id equals y.id
                           where x.iller.id == p
                           select new
                           {
                               Text = x.ilceadi,
                               Value = x.id.ToString()
                           }).ToList();
            return Json(ilceler, JsonRequestBehavior.AllowGet);
        }
    }
}