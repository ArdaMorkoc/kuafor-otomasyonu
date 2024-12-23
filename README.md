# Kuaför Otomasyonu

Kuaför Otomasyonu, kuaför salonlarının iş akışını dijitalleştirmek ve operasyonel süreçlerini iyileştirmek için geliştirilmiş bir yazılım çözümüdür. Projenin çıkış noktası, sektördeki randevu, müşteri yönetimi, muhasebe işlemleri ve çalışan takibi gibi temel zorlukların, yenilikçi bir otomasyon sistemi ile çözülmesidir.

Bu sistem; müşterilerin randevularını kolayca oluşturup yönetebileceği, çalışanların çalışma programlarını düzenleyebileceği, muhaesebe ve yönetici kısmının işletme gelir-giderlerini detaylı bir şekilde takip edebileceği bir altyapı sunar. PostgreSQL veritabanı ile desteklenen yazılım, kuaför salonlarının iş süreçlerini optimize ederek hem verimliliği artırmayı hem de müşteri memnuniyetini üst düzeye çıkarmayı hedefler.

### Kuaför Otomasyonu Özellikleri

* Randevu Yönetimi : Müşteriler için kolay randevu oluşturma, görüntüleme ve iptal seçenekleri.
* Personel Arayüzü : Çalışanların randevuları yönetmesi ve çalışma programlarını görme imkanı.
* Muhasebe Yönetimi : Gelir-gider takibi ve finansal raporların oluşturulması.
* Yönetici Arayüzü : Sistemdeki tüm kullanıcıları, hizmetleri ve finansal bilgileri yönetme.

### Gereksinimler

Projeyi çalıştırmak için ihtiyacınız olan programlar aşağıdaki gibidir.

* Visual Studio
* PostgreSQL v15

### Kurulum

```
$ Gerekli teknolojileri ve proje dosyalarını bilgisayarına indir.
$ VERİ TABANI klasöründen kuafor2.sql adlı dosyayı postgresql'e aktar.
$ KuaforRandevu.sln'yi aç
$ VeriTabaniYardimcisi adlı class'tan bağlantı ayarlarını güncelle
$ Admin klasöründeki KurumYonetimi.cs'deki bağlantı ayarlarını güncelle
```

### Kullanıcılar

* admin@gmail.com / admin
* muhasebe1@gmail.com / 1234
* personel1@gmail.com / 1234
* musteri1@gmail.com / 1234


### Kullanılan Teknolojiler

* WinForms .NET Framework
* PostgreSQL
* Bunifu.UI.WinForms.4.1.1

## Katkıda Bulunanlar

* Arda Anıl Morkoç
* Emir Can Diktaş
* Emre Arslan
