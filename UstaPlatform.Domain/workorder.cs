namespace UstaPlatform.Domain;

public class WorkOrder
{
    public string VatandasAdi;
    public string Aciklama;

    public Usta Usta { get; init; }
    public Talep Talep { get; init; }
    public decimal Fiyat { get; set; }
    public DateTime Tarih { get; set; } = DateTime.Now;

    public override string ToString() =>
        $"{Tarih:g} | {Usta.Ad} - {Talep.Aciklama} ({Talep.VatandasAdi}) = {Fiyat:C}";
}
