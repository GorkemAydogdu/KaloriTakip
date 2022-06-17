using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KaloriTakip.Models;
namespace KaloriTakip.Controllers
{
    public class AccountController : Controller
    {
        KaloriTakipEntities2 db = new KaloriTakipEntities2();
        
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut(); // kullanıcının oturumu kapatmasını sağlar
            Session.Abandon(); /*Kullanıcı sayfayı kapattığında arka planda tanımlı olan tüm sessionların silinmesini sağlar*/
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult Login(tbl_Kullanici kullanici,tbl_Diyetisyen diyetisyen,tbl_Yonetici yonetici)
        {

            /* FirstOrDefault ilk değeri döner */
            var kullaniciBilgiler = db.tbl_Kullanici.FirstOrDefault(x=>x.Email == kullanici.Email && x.Sifre == kullanici.Sifre); // Girilen email ve şifre kullanıcı tablosundaki bilgilerle eşleşiyorsa 
            if (kullaniciBilgiler != null) // girilen bilgiler null değilse
            {
                FormsAuthentication.SetAuthCookie(kullaniciBilgiler.Email,false);
                /* sisteme giriş yapan kullanıcıyı yetkilendirme | kalıcı cookie = false */

                /*Session : Kullanıcının sayfayı görüntülediği anda başlayıp tarayıcının,
                 * kapanana ya da session süresinin bitmesine kadar geçen süreye session (oturum) denir.
                 * Sunucu taraflıdır. Veri saklama işlemleri için kullanılır.*/

                Session["Email"] = kullaniciBilgiler.Email.ToString(); // login olan kullanıcının email bilgisini sessiona atama
                Session["Ad"] = kullaniciBilgiler.Adi.ToString(); // login olan kullanıcının Ad bilgisini sessiona atama
                Session["Yetki"] = kullaniciBilgiler.Yetki.ToString(); // login olan kullanıcının Yetki bilgisini sessiona atama
                Session["ID"] = kullaniciBilgiler.ID; // login olan kullanıcının ID bilgisini sessiona atama
                Session["Cinsiyet"] = kullaniciBilgiler.Cinsiyet; // login olan kullanıcının Cinsiyet bilgisini sessiona atama

                var kullaniciKalori = db.tbl_KiloTakip.FirstOrDefault(m => m.Kullanici == kullaniciBilgiler.ID); // login olan kullanıcının idsi kilo takip tablosundaki kullanici id ile eşleşiyorsa
                if (kullaniciKalori != null) // kullanıcı kalori null değilse
                {
                    Session["GunlukKalori"] = kullaniciKalori.GunlukKalori; // kullanıcının kalori miktarını sessiona atama
                }
                else
                {
                    Session["GunlukKalori"] = 0; // kullanıcı kalori null ise 0 atama
                }

                if (kullaniciBilgiler.Yetki == 1) {  // yetkisi 1 ise anasayfaya yönlendir
                    return RedirectToAction("Anasayfa", "Kullanici");
                }
            }
            var diyetisyenBilgiler = db.tbl_Diyetisyen.FirstOrDefault(x => x.Email == diyetisyen.Email && x.Sifre == diyetisyen.Sifre); // Girilen email ve şifre diyetisyen tablosundaki bilgilerle eşleşiyorsa 
            if (diyetisyenBilgiler != null) // girilen bilgiler null değilse
            {
                FormsAuthentication.SetAuthCookie(diyetisyenBilgiler.Email, false);
                 /* sisteme giriş yapan kullanıcıyı yetkilendirme */
                Session["Email"] = diyetisyenBilgiler.Email.ToString(); // login olan diyetisyenin email bilgisini sessiona atama
                Session["Ad"] = diyetisyenBilgiler.DiyetisyenAdi.ToString(); // login olan diyetisyenin Ad bilgisini sessiona atama
                Session["Soyad"] = diyetisyenBilgiler.DiyetisyenSoyadi.ToString(); // login olan diyetisyenin Soyad bilgisini sessiona atama
                Session["DiyetisyenID"] = diyetisyenBilgiler.ID; // login olan diyetisyenin DiyetisyenID bilgisini sessiona atama

                Session["Yetki"] = diyetisyenBilgiler.Yetki.ToString(); // login olan diyetisyenin Yetki bilgisini sessiona atama
                if (diyetisyenBilgiler.Yetki == 2) // yetkisi 2 ise diyetisyen sayfasına yönlendir
                {
                    return RedirectToAction("Anasayfa", "Diyetisyen");
                }
            }
            var yoneticiBilgiler = db.tbl_Yonetici.FirstOrDefault(x => x.Email == yonetici.Email && x.Sifre == yonetici.Sifre); // Girilen email ve şifre yönetici tablosundaki bilgilerle eşleşiyorsa 
            if (yoneticiBilgiler != null) // bilgiler null değilse
            {
                FormsAuthentication.SetAuthCookie(yoneticiBilgiler.Email, false); // giriş yapan kullanıcıyı yetkilendirme sayfalar arası gezme
                Session["Email"] = yoneticiBilgiler.Email.ToString(); // login olan yöneticinin email bilgisini sessiona atama
                Session["Ad"] = yoneticiBilgiler.Adi.ToString(); // login olan yöneticinin Ad bilgisini sessiona atama
                Session["Yetki"] = yoneticiBilgiler.Yetki.ToString(); // login olan yöneticinin Yetki bilgisini sessiona atama
                if (yoneticiBilgiler.Yetki == 3) // yetki 3 ise yönetici sayfasına yönlendir
                {
                    return RedirectToAction("Anasayfa", "Yonetici");
                }
            }
            else // bilgiler yanlışsa hata ver
            {
                ViewBag.hata = "Email or password incorrect";
            }
            if (!ModelState.IsValid) // doğrulama işlemkleri yapılmadığında logine yönlendir
            {
                return View("Login");
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tbl_Kullanici veri)
        {
            if (!ModelState.IsValid) // doğrulama işlemkleri yapılmadığında register yönlendir
            {
                return View("Register");
            }
            var kullaniciBilgileri = db.tbl_Kullanici.FirstOrDefault(x => x.Email == veri.Email); // girilen email kullanıcı tablosunda varsa
            if (kullaniciBilgileri != null) // null değilse
            {
                ViewBag.Error = "E-mail is already in use"; // email kullanılıyor hatası
            }
            else
            {
                db.tbl_Kullanici.Add(veri); // kullanıcı tablosuna girilen değerleri ekle
                veri.Yetki = 1; // yetkisi 1 
                db.SaveChanges(); // değişiklikleri kaydet
                Session["Adi"] = veri.Adi; // sessiona girilen adı ata
                Session["Soyadi"] = veri.Soyadi; // sessiona girilen soyadi
                Session["ID"] = veri.ID; // sessiona girilen id ata
                Session["Cinsiyet"] = veri.Cinsiyet; // sessiona cinsiyet
                return RedirectToAction("Login", "Account"); //login sayfasına yönlendir
            }
            return View(); 
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(tbl_Kullanici kullanici, tbl_Diyetisyen diyetisyen)
        {
            var kullaniciBilgileri = db.tbl_Kullanici.FirstOrDefault(x => x.Email == kullanici.Email); // giriilen email tbl kullanıcı tablosundaki email ile eşleşiyorsa 
            if (kullaniciBilgileri != null) // null değilse
            {
                Session["Email"] = kullaniciBilgileri.Email.ToString(); // kullanicinin email adresinin sessiona ata
                return RedirectToAction("CreateNewPassword", "Account"); // create new password sayfasına yönlendir
            }
            var diyetisyenBilgileri = db.tbl_Diyetisyen.FirstOrDefault(x => x.Email == diyetisyen.Email); // giriilen email tbl diyetisyen tablosundaki email ile eşleşiyorsa 
            if (diyetisyenBilgileri != null) // null değilse
            {
                Session["Email"] = diyetisyenBilgileri.Email.ToString(); // diyetisyenin email adresinin sessiona ata
                return RedirectToAction("CreateNewPassword", "Account"); // create new password sayfasına yönlendir
            }
            else
            {
                ViewBag.hata = "Email not found"; // eğer girilen email tablolarda bulunmadıysa bulunamadı hatası ver
            }
            if (!ModelState.IsValid) // doğrulama işlemleri yapılmadıysa reset password sayfasına dön
            {
                return View("ResetPassword");
            }
            return View();
        }

        public ActionResult CreateNewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNewPassword(tbl_Kullanici kullanici,tbl_Diyetisyen diyetisyen)
        {
            var kullaniciBilgileri = db.tbl_Kullanici.FirstOrDefault(x => x.Email == kullanici.Email); // girilen email kullanici tablosundaki ile eşleşiyorsa
            if (kullaniciBilgileri!=null) // null değilse
            {
                kullaniciBilgileri.Sifre = kullanici.Sifre; // kullanıcının veritabanındaki şifreyi girilen şifreyi ile güncelle
                db.SaveChanges(); // değişiklikleri kaydet
                return RedirectToAction("Login", "Account"); // logine dön
            }
            var diyetsiyenBilgileri = db.tbl_Diyetisyen.FirstOrDefault(x => x.Email == diyetisyen.Email); // girilen email diyetisyen tablosundaki ile eşleşiyorsa
            if (diyetsiyenBilgileri != null) // null
            {
                diyetsiyenBilgileri.Sifre = diyetisyen.Sifre; // diyetisyenin veritabanindaki şifreyi girilen şifre ile güncelle
                db.SaveChanges(); // değişiklikleri kaydet
                return RedirectToAction("Login", "Account"); // logine dön
            }
            else
            {
                ViewBag.hata = "ERROR";
            }
            return View();

        }
    }
}