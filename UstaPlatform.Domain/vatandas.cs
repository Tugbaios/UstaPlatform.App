namespace UstaPlatform.Domain;

public class Vatandas
{
    public string AdSoyad { get; init; }

    public Talep TalepOlustur(string aciklama, string uzmanlik)
        => new Talep { VatandasAdi = AdSoyad, Aciklama = aciklama, Uzmanlik = uzmanlik };
}
