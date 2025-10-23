namespace UstaPlatform.Pricing;

public class HaftaSonuEkUcreti : IFiyatKural
{
    public string Ad => "Hafta Sonu %20 Artış";

    public decimal Uygula(decimal mevcutFiyat, DateTime tarih)
    {
        if (tarih.DayOfWeek == DayOfWeek.Saturday || tarih.DayOfWeek == DayOfWeek.Sunday)
            return mevcutFiyat * 1.20m;
        return mevcutFiyat;
    }
}
