using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace MvcWebUI.Controllers
{
    public class DatabaseController : Controller
    {
        public IActionResult Seed() // ~/Database/Seed
        {
            //ETicaretContext db = new ETicaretContext();
            //// veritabanı kodları
            //db.Dispose();
            using (ETicaretContext db = new ETicaretContext())
            {
                // verileri silme
                

                var urunEntities = db.Urunler.ToList();
                db.RemoveRange(urunEntities);

                var kategoriEntities = db.Kategoriler.ToList();
                db.RemoveRange(kategoriEntities);

                var kullaniciDetayiEntities = db.KullaniciDetaylari.ToList();
                db.KullaniciDetaylari.RemoveRange(kullaniciDetayiEntities);

                var kullaniciEntities = db.Kullanicilar.ToList();
                db.Kullanicilar.RemoveRange(kullaniciEntities);

                var rolEntities = db.Roller.ToList();
                db.Roller.RemoveRange(rolEntities);

                if (kategoriEntities.Count > 0)
                {
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Urunler', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Kategoriler', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Kullanicilar', RESEED, 0)");
                    db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Roller', RESEED, 0)");
                }

                // verileri ekleme
                db.Kategoriler.Add(new Kategori()
                {
                    Adi = "Bilgisayar",
                    Urunler = new List<Urun>()
                    {
                        new Urun()
                        {
                            Adi = "Dizüstü Bilgisayar",
                            BirimFiyati = 3000.5,
                            StokMiktari = 10,
                            SonKullanmaTarihi = new DateTime(2032, 1, 27)
                        },
                        new Urun()
                        {
                            Adi = "Bilgisayar Faresi",
                            BirimFiyati = 20.5,
                            StokMiktari = 20,
                            SonKullanmaTarihi = DateTime.Parse("19.05.2027", new CultureInfo("tr-TR")) 
                            // İngilizce bölgesel ayar için: en-US, sadece tarih ve ondalık veri tipleri için CultureInfo kullanılmalı,
                            // ~/Program.cs içersinde tüm uygulama için tek seferde AppCore üzerinden tanımlanıp kullanılabilir.
                        },
                        new Urun()
                        {
                            Adi = "Bilgisayar Klavyesi",
                            BirimFiyati = 40,
                            StokMiktari = 21,
                            Aciklamasi = "Bilgisayar Bileşeni"
                        },
                         new Urun()
                        {
                            Adi = "Bilgisayar Monitörü",
                            BirimFiyati = 2500,
                            StokMiktari = 27,
                            Aciklamasi = "Bilgisayar Bileşeni"
                        }
                    }
                });
                db.Kategoriler.Add(new Kategori()
                {
                    Adi = "Ev Eğlence Sistemi",
                    Aciklamasi = "Ev Sinema Sistemleri ve Televizyonlar",
                    Urunler = new List<Urun>()
                    {
                        new Urun()
                        {
                            Adi = "Hoparlör",
                            BirimFiyati = 2500,
                            StokMiktari = 5
                        },
                        new Urun()
                        {
                            Adi = "Amfi",
                            BirimFiyati = 5000,
                            StokMiktari = 9
                        },
                        new Urun()
                        {
                            Adi = "Ekolayzer",
                            BirimFiyati = 1000,
                            StokMiktari = 11
                        }
                    }
                });

                db.Roller.Add(new Rol()
                {
                    Adi = "Admin",
                    Kullanicilar = new List<Kullanici>()
                    {
                        new Kullanici()
                        {
                            KullaniciAdi = "cagil",
                            Sifre = "cagil",
                            AktifMi = true,
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Adres = "Çankaya",
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "cagil@eticaret.com"
                            }
                        }
                    }
                });
                db.Roller.Add(new Rol()
                {
                    Adi = "Kullanıcı",
                    Kullanicilar = new List<Kullanici>()
                    {
                        new Kullanici()
                        {
                            KullaniciAdi = "leo",
                            Sifre = "leo",
                            AktifMi = true,
                            KullaniciDetayi = new KullaniciDetayi()
                            {
                                Adres = "Çankaya",
                                Cinsiyet = Cinsiyet.Erkek,
                                Eposta = "leo@eticaret.com"
                            }
                        }
                    }
                });

                db.SaveChanges();
            }

            return Content("<label style=\"color:red;\"><b>İlk veriler başarıyla oluşturuldu.</b></label>", "text/html", Encoding.UTF8);
        }
    }
}
