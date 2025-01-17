using System;
using System.Collections.Generic;
using System.Linq;
using System.Media; //Ses çalınması için gerekli kütüphane

// Şarkı Türü ENUM
public enum SarkiTuru
{
    Pop,
    Rock,
    Jazz,
    Klasik,
    Rap
}

// Şarkı sınıfı (Encapsulation ile)
public class Muzik
{
    public string ParcaAdi { get; set; }
    public string Sanatci { get; set; }
    public string Album { get; set; }
    public double Sure { get; set; }
    public SarkiTuru Tur { get; set; }
    public string DosyaYolu { get; set; } // Şarkının dosya yolunu burdan erişiriz

    public Muzik(string parcaAdi, string sanatci, string album, double sure, SarkiTuru tur, string dosyaYolu = "")
    {
        ParcaAdi = parcaAdi;
        Sanatci = sanatci;
        Album = album;
        Sure = sure;
        Tur = tur;
        DosyaYolu = dosyaYolu;
    }

    public override string ToString()
    {
        return $"{ParcaAdi} - {Sanatci} ({Sure} dk) [{Tur}]";
    }
}

// Temel Kullanıcı sınıfı (Abstract ve Polymorphism)
public abstract class Kullanici
{
    public string KullaniciAdi { get; set; }

    public Kullanici(string kullaniciAdi)
    {
        KullaniciAdi = kullaniciAdi;
    }

    public abstract void ParcaCal(Muzik parca);
}

// Standart Kullanıcı sınıfı (Inheritance)
public class StandartKullanici : Kullanici
{
    public StandartKullanici(string kullaniciAdi) : base(kullaniciAdi) { }

    public override void ParcaCal(Muzik parca)
    {
        Console.WriteLine($"{KullaniciAdi}, şu an çalan şarkı: {parca.ParcaAdi} - {parca.Sanatci}");
        CalSarkiDosyasi(parca.DosyaYolu);
    }

    private void CalSarkiDosyasi(string dosyaYolu)
    {
        if (!string.IsNullOrEmpty(dosyaYolu))
        {
            try
            {
                SoundPlayer player = new SoundPlayer(dosyaYolu);
                player.Play();
                Console.WriteLine("Şarkı çalınıyor...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: Şarkı çalınamadı. {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Şarkı dosya yolu eksik!");
        }
    }
}

// Sanatçı sınıfı (Inheritance)
public class Sanatci : Kullanici
{
    public Sanatci(string kullaniciAdi) : base(kullaniciAdi) { }

    public void SarkiEkle(List<Muzik> muzikListesi, Muzik yeniSarki)
    {
        muzikListesi.Add(yeniSarki);
        Console.WriteLine($"Sanatçı {KullaniciAdi}, {yeniSarki.ParcaAdi} adlı şarkıyı listeye ekledi.");
    }

    public void ReplaceMetodu(List<Muzik> muzikListesi, string eskiParcaAdi, Muzik yeniSarki)
    {
        var mevcutSarki = muzikListesi.FirstOrDefault(m =>
            string.Equals(m.ParcaAdi, eskiParcaAdi, StringComparison.OrdinalIgnoreCase));

        if (mevcutSarki != null)
        {
            muzikListesi.Remove(mevcutSarki);
            muzikListesi.Add(yeniSarki);
            Console.WriteLine($"Şarkı '{eskiParcaAdi}' başarıyla '{yeniSarki.ParcaAdi}' ile değiştirildi.");
        }
        else
        {
            Console.WriteLine($"Hata: '{eskiParcaAdi}' adlı şarkı bulunamadı.");
        }
    }

    public void SilMetodu(List<Muzik> muzikListesi, string sarkiAdi)
    {
        var silinecekSarki = muzikListesi.FirstOrDefault(m =>
            string.Equals(m.ParcaAdi, sarkiAdi, StringComparison.OrdinalIgnoreCase));

        if (silinecekSarki != null)
        {
            muzikListesi.Remove(silinecekSarki);
            Console.WriteLine($"Şarkı '{sarkiAdi}' başarıyla silindi.");
        }
        else
        {
            Console.WriteLine($"Hata: '{sarkiAdi}' adlı şarkı bulunamadı.");
        }
    }

    public override void ParcaCal(Muzik parca)
    {
        Console.WriteLine($"{KullaniciAdi}, şu an çalan şarkı: {parca.ParcaAdi} - {parca.Sanatci}");
        CalSarkiDosyasi(parca.DosyaYolu);
    }

    private void CalSarkiDosyasi(string dosyaYolu)
    {
        if (!string.IsNullOrEmpty(dosyaYolu))
        {
            try
            {
                SoundPlayer player = new SoundPlayer(dosyaYolu);
                player.Play();
                Console.WriteLine("Şarkı çalınıyor...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: Şarkı çalınamadı. {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Şarkı dosya yolu eksik!");
        }
    }
}

// Program sınıfı
public class Program
{
    static void Main(string[] args)
    {
        // Şarkı listesi
        List<Muzik> muzikListesi = new List<Muzik>
        {
            new Muzik("Viva La Vadi", "Son Feci Bisiklet", "Viva La Vida", 4.5, SarkiTuru.Rock, dosyaYolu : "viva_la_vadi.wav"),
            new Muzik("Sakin Ol Evladım", "Kalben", "Sonuna Kadar", 3.8, SarkiTuru.Pop,dosyaYolu:"sakin_ol_evladım.wav" ),
            new Muzik("Kiss Of Life", "Sade","Time Out",5.2,SarkiTuru.Jazz,dosyaYolu: "kiss_of_life.wav")
        };

        Console.WriteLine("That’s What She Played Müzik Uygulamasına Hoş Geldiniz!");
        Console.WriteLine("Lütfen kullanıcı adınızı giriniz:");
        string kullaniciAdi = Console.ReadLine();

        Console.WriteLine("Sanatçı mısınız? (E/H):");
        string sanatciMi = Console.ReadLine();

        Kullanici aktifKullanici;

        if (sanatciMi.Equals("E", StringComparison.OrdinalIgnoreCase))
        {
            aktifKullanici = new Sanatci(kullaniciAdi);
            Console.WriteLine("Sanatçı olarak giriş yaptınız.");
        }
        else
        {
            aktifKullanici = new StandartKullanici(kullaniciAdi);
            Console.WriteLine("Standart kullanıcı olarak giriş yaptınız.");
        }

        // Ana döngü
        while (true)
        {
            Console.WriteLine("\nLütfen bir işlem seçin:");
            Console.WriteLine("1 - Şarkı Çal");
            Console.WriteLine("2 - Şarkı Ekle (Sadece Sanatçılar için)");
            Console.WriteLine("3 - Şarkı Değiştir (Replace - Sadece Sanatçılar için)");
            Console.WriteLine("4 - Şarkı Sil (Sadece Sanatçılar için)");
            Console.WriteLine("5 - Şarkı Listesini Görüntüle");
            Console.WriteLine("6 - Çıkış");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    Console.WriteLine("Çalmak istediğiniz şarkının adını yazınız:");
                    string sarkiAdi = Console.ReadLine();

                    var secilenSarki = muzikListesi.FirstOrDefault(m =>
                        string.Equals(m.ParcaAdi, sarkiAdi, StringComparison.OrdinalIgnoreCase));

                    if (secilenSarki != null)
                    {
                        aktifKullanici.ParcaCal(secilenSarki);
                    }
                    else
                    {
                        Console.WriteLine("Üzgünüz, şarkı bulunamadı.");
                    }
                    break;

                case "2":
                    if (aktifKullanici is Sanatci sanatci)
                    {
                        Console.WriteLine("Lütfen yeni şarkı bilgilerini giriniz:");
                        Console.Write("Şarkı Adı: ");
                        string yeniParcaAdi = Console.ReadLine();

                        Console.Write("Sanatçı Adı: ");
                        string yeniSanatciAdi = Console.ReadLine();

                        Console.Write("Albüm Adı: ");
                        string yeniAlbumAdi = Console.ReadLine();

                        Console.Write("Süre (dakika): ");
                        double yeniSure = double.Parse(Console.ReadLine());

                        Console.Write("Şarkı Türü (Pop, Rock, Jazz, Klasik, Rap): ");
                        SarkiTuru yeniTur = (SarkiTuru)Enum.Parse(typeof(SarkiTuru), Console.ReadLine(), true);

                        Console.Write("Şarkı Dosya Yolu: ");
                        string yeniDosyaYolu = Console.ReadLine();

                        var yeniSarki = new Muzik(yeniParcaAdi, yeniSanatciAdi, yeniAlbumAdi, yeniSure, yeniTur, yeniDosyaYolu);
                        sanatci.SarkiEkle(muzikListesi, yeniSarki);
                    }
                    else
                    {
                        Console.WriteLine("Bu işlemi sadece sanatçılar gerçekleştirebilir!");
                    }
                    break;

                case "3":
                    if (aktifKullanici is Sanatci sanatciReplace)
                    {
                        Console.WriteLine("Değiştirmek istediğiniz şarkının adını girin:");
                        string eskiParcaAdi = Console.ReadLine();

                        Console.WriteLine("Yeni şarkı bilgilerini giriniz:");
                        Console.Write("Şarkı Adı: ");
                        string yeniParcaAdi = Console.ReadLine();

                        Console.Write("Sanatçı Adı: ");
                        string yeniSanatciAdi = Console.ReadLine();

                        Console.Write("Albüm Adı: ");
                        string yeniAlbumAdi = Console.ReadLine();

                        Console.Write("Süre (dakika): ");
                        double yeniSure = double.Parse(Console.ReadLine());

                        Console.Write("Şarkı Türü (Pop, Rock, Jazz, Klasik, Rap): ");
                        SarkiTuru yeniTur = (SarkiTuru)Enum.Parse(typeof(SarkiTuru), Console.ReadLine(), true);

                        Console.Write("Şarkı Dosya Yolu: ");
                        string yeniDosyaYolu = Console.ReadLine();

                        var yeniSarki = new Muzik(yeniParcaAdi, yeniSanatciAdi, yeniAlbumAdi, yeniSure, yeniTur, yeniDosyaYolu);
                        sanatciReplace.ReplaceMetodu(muzikListesi, eskiParcaAdi, yeniSarki);
                    }
                    else
                    {
                        Console.WriteLine("Bu işlemi sadece sanatçılar gerçekleştirebilir!");
                    }
                    break;

                case "4":
                    if (aktifKullanici is Sanatci sanatciSil)
                    {
                        Console.WriteLine("Silmek istediğiniz şarkının adını girin:");
                        string silinecekSarkiAdi = Console.ReadLine();
                        sanatciSil.SilMetodu(muzikListesi, silinecekSarkiAdi);
                    }
                    else
                    {
                        Console.WriteLine("Bu işlemi sadece sanatçılar gerçekleştirebilir!");
                    }
                    break;

                case "5":
                    Console.WriteLine("\nMevcut Şarkılar:");
                    foreach (var muzik in muzikListesi)
                    {
                        Console.WriteLine($"- {muzik}");
                    }
                    break;

                case "6":
                    Console.WriteLine("Çıkış yapılıyor. Hoşça kalın!");
                    return;

                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
               Console.ReadLine ();
    }
}
