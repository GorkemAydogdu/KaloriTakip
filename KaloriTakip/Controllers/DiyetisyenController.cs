using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaloriTakip.Models;
using KaloriTakip.Models.Class;

namespace KaloriTakip.Controllers
{
    public class DiyetisyenController : Controller
    {
        // GET: Diyetisyen
        KaloriTakipEntities2 db = new KaloriTakipEntities2();
        [Authorize]
        public ActionResult Anasayfa()
        {
            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult KullaniciListele()
        {
            var diyetisyenID = (int)Session["DiyetisyenID"]; // login olan diyetisyen idsinin değişkene ata
            var kullanici = db.tbl_DiyetisyenlerinHastalari.Where(m => m.Diyetisyen == diyetisyenID).ToList(); // diyetisyenhastaları tablosundaki diyetisyen id ve login olan id eşleşiyorsa listele
            return View(kullanici);
        }
        public ActionResult KiloTakip(int id)
        {
            var klncBilgi = db.tbl_KiloTakip.FirstOrDefault(m => m.Kullanici == id); // parametre olarak gelen id kilo takip tablosundaki kullanici id ile eşleşiyorsa listele

            return View(klncBilgi);
        }
        public ActionResult KaloriTakip(int id)
        {
            var klncBilgi = db.tbl_KiloTakip.FirstOrDefault(m => m.Kullanici == id); //parametre olarak gelen id kilo takip tablosundaki kullanici id ile eşleşiyorsa listele
            if (klncBilgi != null) // null değilse
            {
                Session["GunlukKalori"] = klncBilgi.GunlukKalori; // gunluk kaloriyi sessiona ata
            }
            else
            {
                Session["GunlukKalori"] = 0; // null ise session = 0
            }
            double kahvaltiKalori = 0;
            double ogleKalori = 0;
            double aksamKalori = 0;
            double araOgunKalori = 0;
            double toplamKalori = 0;
            Class1 cs = new Class1();
            cs.kahvaltı = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList(); //kahvaltı tablosundaki kullanici id ile parametre olarak gönderilen id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
            cs.ogle = db.tbl_OgleYemegi.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();  //ogle tablosundaki kullanici id ile parametre olarak gönderilen id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
            cs.aksam = db.tbl_AksamYemegi.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList(); //aksam tablosundaki kullanici id ile parametre olarak gönderilen id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
            cs.ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();  //ara tablosundaki kullanici id ile parametre olarak gönderilen id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
           
            
            var kahvalti = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();
            if (kahvalti.Count == 0) 
            {
                /* Eğer kahvaltı tablosunun içerisi boşsa kalori 0*/
                Session["KahvaltiKalori"] = 0;
            }
            
            else
            {
                for (var i = 0; i < kahvalti.Count; i++) // kahvaltı uzunluğu kadar dön
                {
                    kahvaltiKalori = (double)(kahvaltiKalori + kahvalti[i].Kalori); // kaloriyi topla 
                    Session["KahvaltiKalori"] = kahvaltiKalori; // kaloriyi sesiona ata
                }
            }

            var ogle = db.tbl_OgleYemegi.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();
            if (ogle.Count == 0)
            {
                /* Eğer ogle tablosunun içerisi boşsa kalori 0*/
                Session["OgleKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < ogle.Count; i++) // ogle uzunluğu kadar for döngüsü
                {
                    ogleKalori = (double)(ogleKalori + ogle[i].Kalori); // kaloriyi topla
                    Session["OgleKalori"] = ogleKalori; // sesionna ata
                }
            }

            var aksam = db.tbl_AksamYemegi.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();
            if (aksam.Count == 0)
            {
                /* Eğer aksam tablosunun içerisi boşsa kalori 0*/
                Session["AksamKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < aksam.Count; i++) // aksam uzunluğu kadar for döngüsü
                {
                    aksamKalori = (double)(aksamKalori + aksam[i].Kalori); // kaloriyi topla
                    Session["AksamKalori"] = aksamKalori; // sesiona ata
                }
            }

            var ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();
            if (ara.Count == 0)
            {
                /* Eğer aksam tablosunun içerisi boşsa kalori 0*/
                Session["AraOgunKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < ara.Count; i++)
                {
                    araOgunKalori = (double)(araOgunKalori + ara[i].Kalori); // kaloriyi topla
                    Session["AraOgunKalori"] = araOgunKalori; // sessiona ata
                }
            }
            toplamKalori = kahvaltiKalori + ogleKalori + aksamKalori + araOgunKalori; // toplam kalori değişkenine hesaplanan kalorileri ata
            Session["ToplamKalori"] = toplamKalori; // sesiona toplam kaloriyi ata
            return View("KaloriTakip", cs);
        }

        [HttpPost]
        public ActionResult KaloriTakip(DateTime? tarih,int id)
        {
            double kahvaltiKalori = 0;
            double ogleKalori = 0;
            double aksamKalori = 0;
            double araOgunKalori = 0;
            double toplamKalori = 0;
            Session["ToplamKalori"] = 0;

            Class1 cs = new Class1();
            cs.kahvaltı = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // kahvaltı tablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            cs.ogle = db.tbl_OgleYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList();// ogle tablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            cs.aksam = db.tbl_AksamYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // aksamtablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            cs.ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // ara tablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele

            var kahvalti = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // kahvaltı tablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            if (kahvalti.Count == 0)
            {
                // kahvaltı tablosu boşsa kaloriye 0 ata
                Session["KahvaltiKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < kahvalti.Count; i++) // kahvaltı uzunluğuı kadar dön
                {
                    kahvaltiKalori = (double)(kahvaltiKalori + kahvalti[i].Kalori); // kahvaltının i elemanın kalorisini kalori ile topla ve sessiona ata
                    Session["KahvaltiKalori"] = kahvaltiKalori;
                }
            }
            var ogle = db.tbl_OgleYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // ogle tablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            if (ogle.Count == 0)
            {
                // ogle tablosu boşsa kaloriye 0 ata
                Session["OgleKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < ogle.Count; i++) // ogle uzunluğuı kadar dön
                {
                    ogleKalori = (double)(ogleKalori + ogle[i].Kalori); // ogle i elemanın kalorisini kalori ile topla ve sessiona ata
                    Session["OgleKalori"] = ogleKalori;
                }
            }
            var aksam = db.tbl_AksamYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // aksaam tablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            if (aksam.Count == 0)
            {
                // aksam tablosu boşsa kaloriye 0 ata
                Session["AksamKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < aksam.Count; i++) // aksam uzunluğuı kadar dön
                {
                    aksamKalori = (double)(aksamKalori + aksam[i].Kalori); // aksam i elemanın kalorisini kalori ile topla ve sessiona ata
                    Session["AksamKalori"] = aksamKalori;
                }
            }
            var ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // ara tablosundaki kullanıcı ıd ile parametre olarak gönderilen id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            if (ara.Count == 0)
            {
                // ara tablosu boşsa kaloriye 0 ata
                Session["AraOgunKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < ara.Count; i++) // ara uzunluğuı kadar dön
                {
                    araOgunKalori = (double)(araOgunKalori + ara[i].Kalori); // ara i elemanın kalorisini kalori ile topla ve sessiona ata
                    Session["AraOgunKalori"] = araOgunKalori;
                }
            }
            toplamKalori = kahvaltiKalori + ogleKalori + aksamKalori + araOgunKalori; // toplam kalori değişkenine hesaplanan kalorileri ata
            Session["ToplamKalori"] = toplamKalori; // toplam kaloriyi sessiona ata
            return View("KaloriTakip", cs);
        }
    }
}