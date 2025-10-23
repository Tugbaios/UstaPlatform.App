namespace UstaPlatform.Domain;

public class Talep
{
    public string VatandasAdi { get; init; }
    public string Aciklama { get; init; }
    public string Uzmanlik { get; init; }
    public DateTime TalepZamani { get; init; } = DateTime.Now;
}
