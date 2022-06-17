using KaloriTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KaloriTakip.Models.Class;
using PagedList;
using PagedList.Mvc;
namespace KaloriTakip.Controllers
{
    public class KullaniciController : Controller
    {
        KaloriTakipEntities2 db = new KaloriTakipEntities2();

        // GET: Kullanici
        [Authorize]
        public ActionResult Anasayfa()
        {
            var id = (int)Session["ID"];
            if (id != null)
            {
                double kahvaltiKalori = 0;
                double ogleKalori = 0;
                double aksamKalori = 0;
                double araOgunKalori = 0;
                double toplamKalori = 0;
                /* login olan kullanicinın id veritabanıyla uyuşuyorsa ve tablodaki tarih bugunun tarihile
                uyuşuyorsa listele*/
                Class1 cs = new Class1();
                cs.kahvaltı = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList(); //kahvaltı tablosundaki kullanici id ile login olan id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
                cs.ogle = db.tbl_OgleYemegi.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList(); //ogle tablosundaki kullanici id ile login olan id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
                cs.aksam = db.tbl_AksamYemegi.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList(); //aksam tablosundaki kullanici id ile login olan id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
                cs.ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList(); //ara tablosundaki kullanici id ile login olan id ve tablodaki tarih bugünün tarihi ile eşleşiyorsa listele
                var kahvalti = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();
                if (kahvalti.Count == 0)
                {
                    /* Eğer kahvaltı tablosunun içerisi boşsa kalori 0*/
                    Session["KahvaltiKalori"] = 0;
                }
                else
                {
                    for (var i = 0; i < kahvalti.Count; i++) // kahvaltı uzunlugu kadar dön
                    {
                        kahvaltiKalori = (double)(kahvaltiKalori + kahvalti[i].Kalori); // kahvaltı içinde bulunanların kalorisini topla
                        Session["KahvaltiKalori"] = kahvaltiKalori; // sesiona kaloriyi ata
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
                    for (var i = 0; i < ogle.Count; i++) // ogle uzunlugu kadar dön
                    {
                        ogleKalori = (double)(ogleKalori + ogle[i].Kalori); // ogle  içinde bulunanların kalorisini topla
                        Session["OgleKalori"] = ogleKalori; // sesiona kaloriyi ata
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
                    for (var i = 0; i < aksam.Count; i++) // aksam uzunlugu kadar dön
                    {
                        aksamKalori = (double)(aksamKalori + aksam[i].Kalori); // aksam içinde bulunanların kalorisini topla
                        Session["AksamKalori"] = aksamKalori; // sesiona kaloriyi ata
                    }
                }
                var ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == DateTime.Today).ToList();
                if (ara.Count == 0)
                {
                    /* Eğer ara tablosunun içerisi boşsa kalori 0*/
                    Session["AraOgunKalori"] = 0;
                }
                else
                {
                    for (var i = 0; i < ara.Count; i++) // ara uzunlugu kadar dön
                    {
                        araOgunKalori = (double)(araOgunKalori + ara[i].Kalori); // ara içinde bulunanların kalorisini topla
                        Session["AraOgunKalori"] = araOgunKalori; // sesiona kaloriyi ata
                    }
                }
                toplamKalori = kahvaltiKalori + ogleKalori + aksamKalori + araOgunKalori; // toplam kalori değişkenine hesaplanan kalorileri ata
                Session["ToplamKalori"] = toplamKalori; // sesiona toplam kaloriyi ata
                return View("Anasayfa", cs);
            }
            return View();

        }

        [Authorize]
        [HttpPost]
        public ActionResult Anasayfa(DateTime? tarih)
        {
            double kahvaltiKalori = 0;
            double ogleKalori = 0;
            double aksamKalori = 0;
            double araOgunKalori = 0;
            double toplamKalori = 0;
            Session["ToplamKalori"] = 0;
            Class1 cs = new Class1();
            var id = (int)Session["ID"];
            /* login olan kullanicinın id veritabanıyla uyuşuyorsa ve tablodaki tarih girilen tarihle
            uyuşuyorsa listele*/
            cs.kahvaltı = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // kahvaltı tablosundaki kullanıcı id ile login olan id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            cs.ogle = db.tbl_OgleYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // ogle tablosundaki kullanıcı id ile login olan id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            cs.aksam = db.tbl_AksamYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // aksam tablosundaki kullanıcı id ile login olan id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele
            cs.ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList(); // ara tablosundaki kullanıcı id ile login olan id ve tablodaki tarih ile parametre olarak gönderilen tarih eşleşiyorsa listele

            var kahvalti = db.tbl_Kahvaltı.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList();
            if (kahvalti.Count == 0)
            {
                // kahvaltı tablosu boşsa kaloriye 0 ata
                Session["KahvaltiKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < kahvalti.Count; i++) // kahvaltı uzunlugu kadar dön
                {
                    kahvaltiKalori = (double)(kahvaltiKalori + kahvalti[i].Kalori); // kahvaltının i elemanın kalorisini kalori ile topla ve sessiona ata 
                    Session["KahvaltiKalori"] = kahvaltiKalori;
                }
            }
            var ogle = db.tbl_OgleYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList();
            if (ogle.Count == 0)
            {
                // ogle tablosu boşsa kaloriye 0 ata
                Session["OgleKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < ogle.Count; i++)
                {
                    ogleKalori = (double)(ogleKalori + ogle[i].Kalori); // ogle i elemanın kalorisini kalori ile topla ve sessiona ata 
                    Session["OgleKalori"] = ogleKalori;
                }
            }
            var aksam = db.tbl_AksamYemegi.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList();
            if (aksam.Count == 0)
            {
                // aksam tablosu boşsa kaloriye 0 ata
                Session["AksamKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < aksam.Count; i++)
                {
                    aksamKalori = (double)(aksamKalori + aksam[i].Kalori); // aksam i elemanın kalorisini kalori ile topla ve sessiona ata 
                    Session["AksamKalori"] = aksamKalori;
                }
            }
            var ara = db.tbl_AraOgun.Where(m => m.Kullanici == id && m.Tarih == tarih).ToList();
            if (ara.Count == 0)
            {
                // ara tablosu boşsa kaloriye 0 ata
                Session["AraOgunKalori"] = 0;
            }
            else
            {
                for (var i = 0; i < ara.Count; i++)
                {
                    araOgunKalori = (double)(araOgunKalori + ara[i].Kalori); // ara  i elemanın kalorisini kalori ile topla ve sessiona ata 
                    Session["AraOgunKalori"] = araOgunKalori;
                }
            }
            toplamKalori = kahvaltiKalori + ogleKalori + aksamKalori + araOgunKalori; // toplam kalori değişkenine hesaplanan kalorileri ata
            Session["ToplamKalori"] = toplamKalori; // toplam kaloriyi sessiona ata
            return View("Anasayfa", cs);
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult YemekArama(string y, int sayfa = 1)
        {
            var yemekler = from yemek in db.tbl_Yemek select yemek;
            if (!string.IsNullOrEmpty(y))
            {
                yemekler = yemekler.Where(m => m.YemekAdi.Contains(y));
            }
            return View(yemekler.ToList().ToPagedList(sayfa, 10));
        }
        public ActionResult DiyetisyenListele()
        {
            var degerler = db.tbl_Diyetisyen.ToList(); // diyetisyen tablosunu listele
            return View(degerler);
        }
        public ActionResult DiyetisyenGetir(int id)
        {
            var kullaniciID = (int)Session["ID"]; // login olan kullanici id değişkene ata
            var diyetisyen = db.tbl_Diyetisyen.Find(id); // parametre olarak gelen idyi diyetisyen tablosunda bul
            Session["diyetisyenID"] = id; // parametre olarak gelen idyi sesiona ata
            var klnc = db.tbl_DiyetisyenlerinHastalari.Where(m => m.Kullanici == kullaniciID).ToList(); // diyetisyenhastaları tablosundaki kullanici id ve login olan kullanicinin id eşleşiyorsa listele
            if (klnc.Count != 0) // 0 değilse
            {
                ViewBag.Disable = "Disable"; // disable
                ViewBag.Hata = "Zaten Bir Diyetisyen var"; // diyetisyen var mesajı
            }
            return View("DiyetisyenGetir", diyetisyen);
        }

        [HttpPost]
        public ActionResult DiyetisyenGetir(tbl_DiyetisyenlerinHastalari d)
        {
            var kullaniciID = (int)Session["ID"]; // login olan idyi ata
            var diyetisyenID = (int)Session["diyetisyenID"]; // diyetisyenIDyi ata

            var klnc = db.tbl_DiyetisyenlerinHastalari.Where(m => m.Kullanici == kullaniciID).ToList(); // diyetisyenhastaları tablosundaki kullanici id ve login olan kullanicinin id eşleşiyorsa listele
            if (klnc.Count != 0) // 0 değilse
            {
                ViewBag.Disable = "Disable"; // disable
                ViewBag.Hata = "Zaten Bir Diyetisyen var"; // diyetisyen var mesajı
            }
            else // 0 ise
            {
                ViewBag.Disable = "Enable";  // enable
                db.tbl_DiyetisyenlerinHastalari.Add(d); // diyetisyen hastaları tablosuna ekle
                d.Diyetisyen = diyetisyenID; // diyetiyen idsini 
                d.Kullanici = kullaniciID; // kullanıcı idsini
                db.SaveChanges(); // değişiklikleri kaydet
            }
            return RedirectToAction("Anasayfa", "Kullanici");
        }

        public ActionResult Sil(int id)
        {
            var kahvaltı = db.tbl_Kahvaltı.Find(id); // parametre olarak gönderilen idyi kahvaltı tablosunda bul
            if (kahvaltı != null) // null değilse
            {
                db.tbl_Kahvaltı.Remove(kahvaltı); // kahvaltı tablosundan sil
            }

            var ogle = db.tbl_OgleYemegi.Find(id); // parametre olarak gönderilen idyi ogle tablosunda bul
            if (ogle != null) // null değilse
            {
                db.tbl_OgleYemegi.Remove(ogle); // ogle tablosundan sil
            }

            var aksam = db.tbl_AksamYemegi.Find(id); // parametre olarak gönderilen idyi aksam tablosunda bul
            if (aksam != null) // null değilse
            {
                db.tbl_AksamYemegi.Remove(aksam); // aksam tablosundan sil
            }
            var ara = db.tbl_AraOgun.Find(id);  // parametre olarak gönderilen idyi ara tablosunda bu
            if (ara != null) // null değilse
            {
                db.tbl_AraOgun.Remove(ara); // ara tablosundan sil
            }

            db.SaveChanges();
            return RedirectToAction("Anasayfa");
        }
        public ActionResult YemekGetir(int id)
        {
            var yemek = db.tbl_Yemek.Find(id); // parametre olarak gönderilen idyi yemek tablosunda bul
            Session["YemekID"] = id; // idyi sessiona ata
            return View("YemekGetir", yemek);
        }

        [HttpPost]
        public ActionResult YemekGetir(string Ogunler, double porsiyon, tbl_Kahvaltı kahvaltı, tbl_OgleYemegi ogle, tbl_AksamYemegi aksam, tbl_AraOgun ara)
        {
            var yemekID = (int)Session["YemekID"]; // sesiondaki yemekid değişkene ata
            var id = (int)Session["ID"]; // sesiondaki id değişkene ata
            if (Ogunler == "Breakfast" && id != null) // ogunler yani kullanıcı tarafından seçilen öğün Breakfast ise ve id null değilse
            {
                var yemek = db.tbl_Yemek.Find(yemekID); // yemek tablosunda yemek id bul
                db.tbl_Kahvaltı.Add(kahvaltı); // kahvaltı tablosuna ekle
                kahvaltı.Kullanici = id; // kahvaltı tablosundakki kullanıcı idye login olan idyi ata
                kahvaltı.Yemek = yemek.ID; // tablodaki yemekId ye sessiondan gelen değeri ata
                kahvaltı.PorsiyonMiktari = (porsiyon / 100); // porsiyon miktarını kullanıcı tarafından girilen miktar / 100 olarak ekle ÖRN(100 gram yazıldıysa 1 porsyion )
                kahvaltı.Kalori = yemek.Kalori_100Gram_ * (porsiyon / 100); // kalori miktarını girilen porsiyon miktarı ile çarpılıp ekle
            }
            else if (Ogunler == "Lunch" && id != null) // ogunler yani kullanıcı tarafından seçilen öğün Lunch ise ve id null değilse
            {
                var yemek = db.tbl_Yemek.Find(yemekID); // yemek tablosunda yemek id bul
                db.tbl_OgleYemegi.Add(ogle); // ogle tablosuna ekle
                ogle.Kullanici = id; // ogle tablosundakki kullanıcı idye login olan idyi ata
                ogle.Yemek = yemek.ID; // tablodaki yemekId ye sessiondan gelen değeri ata
                ogle.PorsiyonMiktari = (porsiyon / 100); // porsiyon miktarını kullanıcı tarafından girilen miktar / 100 olarak ekle ÖRN(100 gram yazıldıysa 1 porsyion )
                ogle.Kalori = yemek.Kalori_100Gram_ * (porsiyon / 100); // kalori miktarını girilen porsiyon miktarı ile çarpılıp ekle
            }
            else if (Ogunler == "Dinner" && id != null) // ogunler yani kullanıcı tarafından seçilen öğün Dinner ise ve id null değilse
            {
                var yemek = db.tbl_Yemek.Find(yemekID); // yemek tablosunda yemek id bul
                db.tbl_AksamYemegi.Add(aksam); // aksam tablosuna ekle
                aksam.Kullanici = id;// aksam tablosundakki kullanıcı idye login olan idyi ata
                aksam.Yemek = yemek.ID; // tablodaki yemekId ye sessiondan gelen değeri ata
                aksam.PorsiyonMiktari = (porsiyon / 100); // porsiyon miktarını kullanıcı tarafından girilen miktar / 100 olarak ekle ÖRN(100 gram yazıldıysa 1 porsyion )
                aksam.Kalori = yemek.Kalori_100Gram_ * (porsiyon / 100); // kalori miktarını girilen porsiyon miktarı ile çarpılıp ekle
            }
            else if (Ogunler == "Snacks/Other" && id != null) // ogunler yani kullanıcı tarafından seçilen öğün Snacks/Other ise ve id null değilse
            {
                var yemek = db.tbl_Yemek.Find(yemekID); // yemek tablosunda yemek id bul
                db.tbl_AraOgun.Add(ara); // ara tablosuna ekle
                ara.Kullanici = id; // ara tablosundakki kullanıcı idye login olan idyi ata
                ara.Yemek = yemek.ID; // tablodaki yemekId ye sessiondan gelen değeri ata
                ara.PorsiyonMiktari = (porsiyon / 100); // porsiyon miktarını kullanıcı tarafından girilen miktar / 100 olarak ekle ÖRN(100 gram yazıldıysa 1 porsyion )
                ara.Kalori = yemek.Kalori_100Gram_ * (porsiyon / 100); // kalori miktarını girilen porsiyon miktarı ile çarpılıp ekle
            }
            db.SaveChanges(); // değişiklikleri kaydet
            return RedirectToAction("Anasayfa", "Kullanici");
        }

        public ActionResult Bilgiler()
        {
            var id = (int)Session["ID"]; // login olan idyi değişkene ata
            if (id != null) // null değilse
            {
                var kullanici = db.tbl_KiloTakip.FirstOrDefault(m => m.Kullanici == id); // kilotakip tablosundakki kullanici id ile eşleşiyorsa
                if (kullanici == null) // null ise
                {
                    Session["GunlukKalori"] = 0; // kalori 0
                }
                else
                {
                    Session["GunlukKalori"] = kullanici.GunlukKalori; // null değilse kalori miktarını sessiona ata
                }
                return View("Bilgiler", kullanici);
            }
            else
            {
                ViewBag.hata = "error";
            }
            if (!ModelState.IsValid) // doğrulama işlemkleri yapılmadığında Bilgiler yönlendir
            {
                return View("Bilgiler");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Bilgiler(tbl_KiloTakip kiloTakip)
        {
            var id = (int)Session["ID"]; // login olan id değişkene ata
            var cinsiyet = (string)Session["Cinsiyet"]; // login olan kullanıcının cinsiyet değişkene ata
            var kullanici = db.tbl_KiloTakip.FirstOrDefault(m => m.Kullanici == id); // kilo takip tablosundaki id işe eşleşiyorsa
            if (kullanici != null) // null değilse
            {
                Session["GunlukKalori"] = 0; // kalori 0
                kullanici.MevcutBoy = kiloTakip.MevcutBoy; // girilen boy bilgisini tablodaki boya ata
                kullanici.MevcutKilosu = kiloTakip.MevcutKilosu; // girilen kilo bilgisini tablodaki kilo ata
                kullanici.Yas = kiloTakip.Yas; // girilen yas bilgisini tablodaki yas ata
                kullanici.AktiviteSeviyesi = kiloTakip.AktiviteSeviyesi; // girilen aktivite seviyesini bilgisini tablodaki aktivite seviyesine ata
                var aktiviteSeviyesi = kullanici.AktiviteSeviyesi; // tablodaki aktivite seviyesini değişklene ata
                if (cinsiyet == "E") // cinsiyet E ise
                {
                    if (aktiviteSeviyesi == "Sedentary") // aktivite seviyesi Sedentary ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.2);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Lightly active") // aktivite seviyesi Lightly active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.375);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Moderately active") // aktivite seviyesi Moderately active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.55);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Very active") // aktivite seviyesi Very active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.725);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Extra active") // aktivite seviyesi Extra active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.9);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    Session["GunlukKalori"] = kullanici.GunlukKalori; //tablodaki günlük kalori miktarını sessiona ata

                }
                else if (cinsiyet == "K") // Cinsiyet K ise
                {
                    if (aktiviteSeviyesi == "Sedentary") // aktivite seviyesi Sedentary ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.2);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Lightly active") // aktivite seviyesi Lightly active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.375);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Moderately active") // aktivite seviyesi Moderately active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.55);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Very active") // aktivite seviyesi Very active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.725);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Extra active") // aktivite seviyesi Extra active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.9);
                        kullanici.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    Session["GunlukKalori"] = kullanici.GunlukKalori; //tablodaki günlük kalori miktarını sessiona ata
                }
            }
            else // kullanici null ise
            {
                Session["GunlukKalori"] = 0; // kalori 0
                db.tbl_KiloTakip.Add(kiloTakip); //kilo takip tablosuna girilen verileri ekle
                var aktiviteSeviyesi = kiloTakip.AktiviteSeviyesi; // tablodaki aktiviyte seviyesini değişkene ata
                kiloTakip.Kullanici = id; // login olan idyi tablodaki kullanici id ye ata
                if (cinsiyet == "E") // cinsiyet E ise
                {
                    if (aktiviteSeviyesi == "Sedentary") // aktivite seviyesi Sedentary ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.2);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Lightly active") // aktivite seviyesi Lightly active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.375);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Moderately active") // aktivite seviyesi Moderately active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.55);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Very active") // aktivite seviyesi Very active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.725);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    } 
                    else if (aktiviteSeviyesi == "Extra active") // aktivite seviyesi Extra active ise
                    {
                        var BMR = 66.5 + (13.75 * kiloTakip.MevcutKilosu) + (5.003 * kiloTakip.MevcutBoy) - (6.75 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.9);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    } 
                    Session["GunlukKalori"] = kiloTakip.GunlukKalori; //tablodaki günlük kalori miktarını sessiona ata

                }
                else if (cinsiyet == "K") // cinsiyet K ise
                {
                    if (aktiviteSeviyesi == "Sedentary") // aktivite seviyesi Sedentary ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.2);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Lightly active") // aktivite seviyesi Lightly active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.375);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Moderately active") // aktivite seviyesi Moderately active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.55);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Very active") // aktivite seviyesi Very active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.725);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    else if (aktiviteSeviyesi == "Extra active") // aktivite seviyesi Extra active ise
                    {
                        var BMR = 655.1 + (9.563 * kiloTakip.MevcutKilosu) + (1.850 * kiloTakip.MevcutBoy) - (4.676 * kiloTakip.Yas);
                        int kalori = Convert.ToInt32(BMR * 1.9);
                        kiloTakip.GunlukKalori = kalori; // hesaplanan günlük kalori miktarının tabloya ata
                    }
                    Session["GunlukKalori"] = kiloTakip.GunlukKalori; //tablodaki günlük kalori miktarını sessiona ata
                }
            }
            if (!ModelState.IsValid) // doğrulama işlemkleri yapılmadığında Bilgiler yönlendir
            {
                return View("Bilgiler");
            }
            db.SaveChanges(); // değişiklikleri kaydet
            return RedirectToAction("Bilgiler", "Kullanici");
        }
    }
}