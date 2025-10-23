using UstaPlatform.Domain;
using UstaPlatform.Pricing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UstaPlatform.App
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("--- UstaPlatform (Dinamik + Plug-in Sürüm) ---\n");

            // 1️⃣ Vatandaş bilgisi al
            Console.Write("Vatandaş Adı: ");
            string vatandasAdi = Console.ReadLine() ?? "Bilinmiyor";
            Environment.SetEnvironmentVariable("CurrentCitizenName", vatandasAdi);


            Console.Write("İş Açıklaması: ");
            string aciklama = Console.ReadLine() ?? "Belirtilmedi";

            // 2️⃣ Usta listesini oluştur
            var ustalar = new List<Usta>
            {
                new Usta { Ad = "Ahmet Usta", Uzmanlik = "Tesisat" },
                new Usta { Ad = "Mehmet Usta", Uzmanlik = "Elektrik" },
                new Usta { Ad = "Ayşe Usta", Uzmanlik = "Boyacı" },
                new Usta { Ad = "Fatma Usta", Uzmanlik = "Marangoz" }
            };

            // 3️⃣ Akıllı usta seçimi (açıklamaya göre)
            Usta secilenUsta;

            if (aciklama.ToLower().Contains("su") || aciklama.ToLower().Contains("patlak"))
                secilenUsta = ustalar.First(u => u.Uzmanlik == "Tesisat");
            else if (aciklama.ToLower().Contains("elektrik"))
                secilenUsta = ustalar.First(u => u.Uzmanlik == "Elektrik");
            else if (aciklama.ToLower().Contains("boya"))
                secilenUsta = ustalar.First(u => u.Uzmanlik == "Boyacı");
            else if (aciklama.ToLower().Contains("marangoz"))
                secilenUsta = ustalar.First(u => u.Uzmanlik == "Marangoz");
            else
            {
                // Eğer hiçbir eşleşme yoksa rastgele bir usta seç
                var random = new Random();
                secilenUsta = ustalar[random.Next(ustalar.Count)];
            }

            // 4️⃣ Fiyat motoru oluştur
            var kurallar = new List<IFiyatKural> { new HaftaSonuEkUcreti() };
            var fiyatMotoru = new FiyatlamaMotoru(kurallar);

            // 🔌 4.1️⃣ Plugins klasöründeki DLL kurallarını yükle
            fiyatMotoru.YukleDLLKurallari("Plugins");
            Console.WriteLine("\n[Bilgi] Plug-in kuralları yüklendi.\n");

            decimal temelFiyat;

            switch (secilenUsta.Uzmanlik)
            {
                case "Tesisat":
                    temelFiyat = 400m;
                    break;
                case "Elektrik":
                    temelFiyat = 350m;
                    break;
                case "Boyacı":
                    temelFiyat = 300m;
                    break;
                case "Marangoz":
                    temelFiyat = 450m;
                    break;
                default:
                    temelFiyat = 300m;
                    break;
            }



            // 6️⃣ Tarihe göre fiyatı hesapla
            var bugun = DateTime.Now;
            decimal toplamFiyat = fiyatMotoru.Hesapla(temelFiyat, bugun);

            // 7️⃣ İş emrini oluştur
            var isEmri = new WorkOrder
            {
                VatandasAdi = vatandasAdi,
                Aciklama = aciklama,
                Usta = secilenUsta,
                Tarih = bugun,
                Fiyat = toplamFiyat
            };

            // 8️⃣ Sonucu yazdır
            Console.WriteLine($"\n {isEmri.Tarih:dd.MM.yyyy HH:mm}");
            Console.WriteLine($" {isEmri.Usta.Ad} ({isEmri.Usta.Uzmanlik})");
            Console.WriteLine($" Fiyat: {isEmri.Fiyat:C2}");
            Console.WriteLine($"\n İş emri başarıyla oluşturuldu!\n");
        }
    }
}
