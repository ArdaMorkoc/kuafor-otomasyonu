-- Yetki Tanımları
CREATE TABLE Yetkiler (
    YetkiID SERIAL PRIMARY KEY,
    YetkiAdi VARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO Yetkiler (YetkiAdi) VALUES
('Müşteri'), -- Yetki 1
('Personel'), -- Yetki 2
('Muhasebe'), -- Yetki 3
('Yönetici'); -- Yetki 4

-- Kullanıcılar Tablosu (Hakkimda ve SadakatPuani kaldırılmış)
CREATE TABLE Kullanicilar (
    KullaniciID SERIAL PRIMARY KEY,
    TamAd VARCHAR(100) NOT NULL,
    Eposta VARCHAR(100) UNIQUE,
    Sifre VARCHAR(255) NOT NULL,
    TelefonNumarasi VARCHAR(20),
    YetkiID INT NOT NULL REFERENCES Yetkiler(YetkiID),
    KayitTarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Verilerin eklenmesi (Hakkimda ve SadakatPuani olmadan)
INSERT INTO Kullanicilar (TamAd, Eposta, Sifre, TelefonNumarasi, YetkiID) VALUES
('Admin Kullanıcı', 'admin@gmail.com', 'admin', '05001234567', 4),
('Ayşe Yılmaz', 'personel1@gmail.com', '1234', '05007654321', 2),
('Fatma Kaya', 'muhasebe1@gmail.com', '1234', '05009876543', 3),
('Merve Ak', 'musteri1@gmail.com', '1234', '05003456789', 1);

-- Kategoriler
CREATE TABLE Kategoriler (
    KategoriID SERIAL PRIMARY KEY,
    KategoriAdi VARCHAR(50) NOT NULL
);

INSERT INTO Kategoriler (KategoriAdi) VALUES 
('Saç'),
('Cilt Bakımı'),
('Tırnak Hizmetleri'),
('Kaş');

-- Hizmetler
CREATE TABLE Hizmetler (
    HizmetID SERIAL PRIMARY KEY,
    HizmetAdi VARCHAR(100) NOT NULL,
    KategoriID INT NOT NULL REFERENCES Kategoriler(KategoriID),
    Aciklama TEXT,
    Fiyat NUMERIC(10, 2) NOT NULL,
    SureDakika INT NOT NULL,
    PopulerlikPuani INT DEFAULT 0 -- Popülerlik
);

INSERT INTO Hizmetler (HizmetAdi, KategoriID, Aciklama, Fiyat, SureDakika) VALUES
('Saç Kesimi', 1, 'Saç kesimi hizmeti', 100.00, 30),
('Saç Boyama', 1, 'Saç boyama hizmeti', 100.00, 30),
('Cilt Temizliği', 2, 'Siyahnokta vb temizliği hizmeti', 200.00, 60),
('Manikür', 3, 'Manikür hizmeti', 50.00, 45),
('Pedikür', 3, 'Pedikür hizmeti', 60.00, 45),
('Kaş Alma', 4, 'Kaş alma hizmeti', 30.00, 15),
('Altın Oran Kaş Alma', 4, 'Altın oran Kaş alma hizmeti', 50.00, 15);

-- Randevular
CREATE TABLE Randevular (
    RandevuID SERIAL PRIMARY KEY,
    MusteriID INT NOT NULL REFERENCES Kullanicilar(KullaniciID),
    PersonelID INT NOT NULL REFERENCES Kullanicilar(KullaniciID),
    RandevuTarihi TIMESTAMP NOT NULL,
    RandevuSaati TIME NOT NULL,
    Notlar TEXT,
    Durum VARCHAR(20) DEFAULT 'Beklemede' CHECK (Durum IN ('Beklemede', 'Tamamlandı', 'İptal Edildi','Onaylandı')),
    ToplamTutar NUMERIC(10, 2) -- Randevu Toplam Tutarı
);

INSERT INTO Randevular (MusteriID, PersonelID, RandevuTarihi, RandevuSaati, Notlar, Durum) VALUES
(4, 2, '2024-12-25', '10:00:00', 'Saclarimi gothic bir tarzda kestirmek istiyorum.', 'Beklemede'),
(4, 2, '2024-12-26', '14:00:00', 'Cilt bakimi islemi yapilacak.', 'Onaylandı');

-- Randevu Hizmetleri
CREATE TABLE RandevuHizmetleri (
    RandevuID INT NOT NULL REFERENCES Randevular(RandevuID),
    HizmetID INT NOT NULL REFERENCES Hizmetler(HizmetID),
    Miktar INT DEFAULT 1,
    PRIMARY KEY (RandevuID, HizmetID)
);

INSERT INTO RandevuHizmetleri (RandevuID, HizmetID, Miktar) VALUES
(1, 1, 1),
(2, 3, 1);

-- Gelirler
CREATE TABLE Gelirler (
    GelirID SERIAL PRIMARY KEY,
    RandevuID INT NOT NULL REFERENCES Randevular(RandevuID),
    MusteriID INT NOT NULL REFERENCES Kullanicilar(KullaniciID),
    PersonelID INT NOT NULL REFERENCES Kullanicilar(KullaniciID),
    Tutar NUMERIC(10, 2) NOT NULL,
    GelirTarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Örnek Gelir Verisi
INSERT INTO Gelirler (RandevuID, MusteriID, PersonelID, Tutar) VALUES
(1, 4, 2, 100.00),
(2, 4, 2, 50.00);


-- Gider Adları
CREATE TABLE GiderTurleri (
    GiderTurleriID SERIAL PRIMARY KEY,
    GiderAdi VARCHAR(50) NOT NULL
);

INSERT INTO GiderTurleri (GiderAdi) VALUES
('Kira Masrafı'),
('Maaş'),
('Malzeme Masrafı'),
('Temizlik Masrafı');

-- Giderler
CREATE TABLE Giderler (
    GiderID SERIAL PRIMARY KEY, 
    GiderTuruID INT NOT NULL REFERENCES GiderTurleri(GiderTurleriID),
    Tutar NUMERIC(10, 2) NOT NULL,
    GiderTarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Örnek Gider Verisi
INSERT INTO Giderler (GiderTuruID, Tutar, GiderTarihi) VALUES
(1, 2000.00, '2024-11-01'),
(2, 500.00, '2024-11-05');

-- Muhasebe İşlemleri
CREATE TABLE MuhasebeIslemleri (
    IslemID SERIAL PRIMARY KEY,
    IslemTarihi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    GiderID INT NOT NULL REFERENCES Giderler(GiderID)
);

-- Örnek Muhasebe İşlemi Verisi
INSERT INTO MuhasebeIslemleri (GiderID, IslemTarihi) VALUES
(1, '2024-11-01'),
(2, '2024-11-05');

ALTER TABLE randevular DROP CONSTRAINT randevular_durum_check;
ALTER TABLE randevular ADD CONSTRAINT randevular_durum_check 
CHECK (durum IN ('Beklemede', 'Tamamlandı', 'İptal Edildi', 'Onaylandı'));

ALTER TABLE Randevular ADD COLUMN OdemeTarihi TIMESTAMP;
Drop Table Gelirler;