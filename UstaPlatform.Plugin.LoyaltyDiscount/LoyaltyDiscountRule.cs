using UstaPlatform.Pricing;
using System;
using System.IO;
using System.Linq;

namespace UstaPlatform.Plugin.LoyaltyDiscount
{
    public class LoyaltyDiscountRule : IFiyatKural
    {
        public string Ad => "Sadakat İndirimi (%10 - 3. işlemden itibaren)";

        public decimal Uygula(decimal mevcutFiyat, DateTime tarih)
        {
            string logDosyasi = Path.Combine(AppContext.BaseDirectory, "customer_log.txt");
            string vatandasAdi = Environment.GetEnvironmentVariable("CurrentCitizenName") ?? "Bilinmiyor";

            // Log dosyası yoksa oluştur
            if (!File.Exists(logDosyasi))
                File.WriteAllText(logDosyasi, "");

            // Müşteri geçmişini oku
            var tumKayitlar = File.ReadAllLines(logDosyasi).ToList();

            // Aynı isim kaç kez geçmiş?
            int kacDefa = tumKayitlar.Count(x => x.Equals(vatandasAdi, StringComparison.OrdinalIgnoreCase));

            // Şu anki işlemi de kaydet (listeye ekle)
            tumKayitlar.Add(vatandasAdi);
            File.WriteAllLines(logDosyasi, tumKayitlar);

            // Eğer 3. veya sonraki işlemlerden biriyse indirim uygula
            if (kacDefa >= 2)
            {
                Console.WriteLine($" {vatandasAdi} için sadakat indirimi uygulandı (%10)");
                return mevcutFiyat * 0.9m;
            }

            // İlk 2 işlemde indirim yok
            return mevcutFiyat;
        }
    }
}
