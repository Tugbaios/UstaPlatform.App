namespace UstaPlatform.Domain;

public class Usta
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Ad { get; init; }
    public string Uzmanlik { get; init; }
    public double Puan { get; set; }
    public bool MusaitMi { get; set; } = true;

    public override string ToString()
        => $"{Ad} ({Uzmanlik}) - {(MusaitMi ? "Müsait" : "Meşgul")}";
}
