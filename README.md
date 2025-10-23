 🧰 UstaPlatform – Şehrin Uzmanlık Platformu  
**Ders:** Nesne Yönelimli Programlama (NYP) ve İleri C#  
**Öğrenci:** Selin Tuğba Özdemir  
**Üniversite:** Yozgat Bozok Üniversitesi – Bilgisayar Mühendisliği  
**Yıl:** 2025 Güz Dönemi  

---

## 🎯 Projenin Amacı
Bu proje, Arcadia şehrinde vatandaşların açtığı iş taleplerini uygun ustalarla (tesisatçı, elektrikçi, boyacı vb.) eşleştiren, dinamik fiyat hesaplaması yapabilen ve eklenti (plug-in) mimarisiyle genişletilebilen bir **usta yönetim sistemi** geliştirmeyi amaçlamaktadır.  
Sistem, yeni fiyat kuralları veya kampanyalar eklendiğinde **ana kodda hiçbir değişiklik yapılmadan** çalışmaya devam eder. Bu sayede **Açık/Kapalı (Open/Closed)** SOLID prensibi uygulanmış olur.  

---

## 🧩 Katmanlı Mimari
Uygulama 3 ana katmandan oluşmaktadır:

1. **UstaPlatform.Domain** → Temel varlıklar (Usta, Vatandaş, İşEmri, Talep sınıfları)  
2. **UstaPlatform.Pricing** → Fiyatlama kuralları (örneğin: HaftaSonuEkUcreti, Sadakat indirimi)  
3. **UstaPlatform.App** → Kullanıcı arayüzü (Console uygulaması)  

> *Ek olarak “Plugins” klasörü, sonradan eklenebilen fiyat kurallarını (DLL) barındırır.*

---

## ⚙️ Kurulum Adımları
1. **Projeyi indir veya klonla:**
   ```bash
   git clone https://github.com/Tugbaios/UstaPlatform.git
Visual Studio’da UstaPlatform.sln dosyasını aç.

UstaPlatform.App projesini Startup Project olarak ayarla.

Projeyi Build et (Ctrl+Shift+B).

Plugins klasörüne fiyat kuralı DLL dosyasını (örneğin LoyaltyDiscountRule.dll) ekle.

Programı çalıştır (Ctrl + F5).

 Çalışma Mantığı (Demo Akışı)
 Adım 1 – Temel Çalışma
Program açıldığında vatandaş adı ve iş açıklaması istenir.
Girilen açıklamaya göre uygun usta otomatik atanır ve temel fiyat belirlenir.
Fiyat motoru (FiyatlamaMotoru) mevcut kuralları uygular.


Örnek Çıktı:

Vatandaş Adı: Selin  
İş Açıklaması: Su patladı  
 Tesisat Ustası: Ahmet Usta  
 Fiyat: ₺400,00  
İş emri başarıyla oluşturuldu!

 Adım 2 – Plug-in (Eklenti) Mimarisi
Programın “Plugins” klasörü otomatik olarak taranır.
Eğer bu klasöre yeni bir DLL (örneğin LoyaltyDiscountRule.dll) eklenirse, sistem bu kuralı otomatik tanır.

Bu durumda konsolda şu mesaj görülür:

plaintext
Kodu kopyala
[Plugin yüklendi] Sadakat İndirimi (%10 - 3. işlemden itibaren)
 Selin için sadakat indirimi uygulandı (%10)
 23.10.2025 11:24
 Ahmet Usta (Tesisat)
 Fiyat: ₺360,00
 İş emri başarıyla oluşturuldu!
Hiçbir kod değişikliği yapılmadan yeni bir işlevsellik eklenmiş olur.

 Tasarım Kararları
Açık/Kapalı Prensibi (OCP): Yeni fiyat kuralları DLL olarak eklenir, ana kod değişmez.

Tek Sorumluluk (SRP): Her sınıf kendi görevini üstlenir (Usta veri tutar, Motor fiyat hesaplar).

Bağımlılıkların Tersine Çevrilmesi (DIP): Fiyatlama motoru somut sınıflara değil, IFiyatKural arayüzüne bağlıdır.

Plug-in Mimarisi:
FiyatlamaMotoru, Plugins klasöründeki tüm .dll dosyalarını tarar ve IFiyatKural arayüzünü uygulayan sınıfları otomatik olarak yükler.

 Kullanılan C# Özellikleri
init-only properties → ID ve kayıt zamanı gibi değişmez alanlarda

Nesne başlatıcıları (object initializer) → kod okunabilirliğini artırmak için

Indexer (Dizinleyici) → Schedule sınıfında tarih bazlı erişim için

IEnumerable → Rota koleksiyonunun gezilebilir hale getirilmesi

Static yardımcı sınıflar → Fiyat formatlama, tarih kontrolü gibi işlemler için
