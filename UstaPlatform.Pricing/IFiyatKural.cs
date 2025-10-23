namespace UstaPlatform.Pricing;

public interface IFiyatKural
{
    string Ad { get; }
    decimal Uygula(decimal mevcutFiyat, DateTime tarih);
}
