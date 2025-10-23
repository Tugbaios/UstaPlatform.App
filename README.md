# UstaPlatform – Dinamik Plug-in Destekli Usta Eşleştirme Sistemi

# Proje Amacı
UstaPlatform, vatandaşların taleplerine göre uygun ustayı (örneğin tesisatçı, elektrikçi, boyacı, marangoz) otomatik olarak eşleştiren, dinamik fiyat hesaplama ve eklenti (plug-in) desteği bulunan çok katmanlı bir C# projesidir.

# Proje Yapısı
Proje **çok katmanlı (multi-project)** bir yapıya sahiptir:

 **UstaPlatform.Domain** → Temel varlıklar (Usta, Vatandas, Talep, WorkOrder)
 **UstaPlatform.Pricing** → Fiyat kuralları ve FiyatlamaMotoru
 **UstaPlatform.App** → Konsol uygulaması (ana çalıştırılabilir katman)
 **Plugins (LoyaltyDiscountRule.dll)** → Eklentiyle yüklenebilir yeni fiyat kuralları

> Bu yapı SOLID prensiplerine göre hazırlanmıştır. Özellikle OCP (Open/Closed Principle) sayesinde sisteme yeni fiyat kuralları, sadece DLL eklenerek eklenebilir.

# Kurulum ve Çalıştırma Adımları

1. Projeyi klonla veya indir:
   ```bash
   git clone https://github.com/Tugbaios/UstaPlatform.git


---

Geliştiren: **Selin Tuğba Özdemir**  
Yozgat Bozok Üniversitesi – Bilgisayar Mühendisliği  
2025 Güz Dönemi – Nesne Yönelimli Programlama Projesi
