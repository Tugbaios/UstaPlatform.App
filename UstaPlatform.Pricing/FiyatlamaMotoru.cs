namespace UstaPlatform.Pricing;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

public class FiyatlamaMotoru
{
    private readonly List<IFiyatKural> _kurallar;

    public FiyatlamaMotoru(List<IFiyatKural> kurallar)
    {
        _kurallar = kurallar;
    }

    public decimal Hesapla(decimal temelFiyat, DateTime tarih)
    {
        decimal fiyat = temelFiyat;
        foreach (var kural in _kurallar)
            fiyat = kural.Uygula(fiyat, tarih);
        return fiyat;
    }

    public void YukleDLLKurallari(string klasorYolu)
    {
        if (string.IsNullOrWhiteSpace(klasorYolu))
            return;

        // Çalışma dizinine göre tam yol oluştur
        var tamYol = Path.IsPathRooted(klasorYolu)
            ? klasorYolu
            : Path.Combine(AppContext.BaseDirectory, klasorYolu);

        if (!Directory.Exists(tamYol))
        {
            Console.WriteLine($"[Plugin klasörü bulunamadı] {tamYol}");
            return;
        }

        // Klasördeki tüm DLL dosyalarını tara
        foreach (var dllYolu in Directory.GetFiles(tamYol, "*.dll"))
        {
            try
            {
                var assembly = Assembly.LoadFrom(dllYolu);

                var tipler = assembly.GetTypes()
                    .Where(t => typeof(IFiyatKural).IsAssignableFrom(t)
                                && !t.IsInterface && !t.IsAbstract);

                foreach (var tip in tipler)
                {
                    if (Activator.CreateInstance(tip) is IFiyatKural kural)
                    {
                        _kurallar.Add(kural);
                        Console.WriteLine($"[Plugin yüklendi] {kural.Ad}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Plugin hatası] {Path.GetFileName(dllYolu)}: {ex.Message}");
            }
        }
    }
    
}
