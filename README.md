# Coin Master
Game ini dibuat sebagai proyek Open Recruitment Network Laboratory 2025.

Coin Master adalah game clicker yang bertujuan untuk menjadikan player sekaya-kayanya. Player bisa mendapatkan currency dengan menekan ikon koin yang terdapat di tengah layar permainan. 

Karena game ini adalah game pembaik, game ini juga menyediakan opsi untuk mempercepat pengumpulan currency dengan membeli beberapa jenis upgrade, seperti menambahkan jumlah currency yang didapat per click, ataupun mendapatkan currency secara pasif. Player juga dapat menghabiskan currency mereka untuk "menyerang" player, yang efeknya akan merendahkan level fasilitas upgrade mereka, sehingga pendapatan currency mereka akan berkurang secara drastis.

## Tech Stack
### Front End
- **Unity**: Coin Master diuji menggunakan Unity LTS 6000.0.49f1.
- **C#**: Programming Language yang digunakan dalam *game engine* Unity.
- **Graphics, Arts, and Sounds**: Unity Asset Store.

### Back End
- **Spring Boot**: Framework Java untuk mengembangkan API backend.
- **Java**: Programming Language yang digunakan dalam Spring Boot.
- **Maven**: Build tool Spring Boot.
- **NeonTech** **Database**: Menyimpan data game.
- **PostgreSQL**: SQL yang digunakan untuk berinteraksi dengan database.

### Others
- **Draw.io, PlantUML**: Menggambarkan UML.
- **Lucid App**: Menggambakan ERD.
- **Microsoft VSCode**: Code Editor.
- **Git, GitHub**: Version Control.

## Requirements
Ada 2 cara untuk memainkan game ini, yaitu:

### Clone & Play
Anda bisa melakukan clone pada repository ini, dan mengakses folder serta mengeksekusi file yang bersangkutan, seperti:

```bash
# Clone Repository
git clone https://github.com/sleepingpolice-afk/CoinMaster.git
cd CoinMaster
```

Masuk ke Unity Hub, dan pilih *Add project from disk*

![picture 0](https://i.imgur.com/SAyGzoG.png)  

Pilih folder **Game**.

![picture 1](https://i.imgur.com/wWHDNRc.png)  

Lalu eksekusi Frontend dengan game engine Unity dengan cara di-*Build* secara langsung ataupun menggunakan *play mode* di Unity.

> Backend telah di-*deploy* dengan menggunakan service **Scalingo**, sehingga tidak perlu dijalankan secara local.

### Web Game
Akses game saya di **Itch.io**: https://wf-835.itch.io/coinmaster-beta

> Di-*deploy* menggunakan WebGL agar game dari Unity bisa dimainkan di *web browser*.


## Game Screenshots and Documentation
- Login Screen

    ![picture 2](https://i.imgur.com/EWG7TnL.png)  

- Register Screen

    ![picture 6](https://i.imgur.com/o5v66YC.png)  

- Main Game Screen

    ![picture 3](https://i.imgur.com/k0kuXiQ.png)  

    ![picture 4](https://i.imgur.com/4EbK2A0.png)  

    ![picture 5](https://i.imgur.com/KA50RDW.png)  


## Additional Documents


- Class Diagram UML

    ![](https://uml.planttext.com/plantuml/png/jLXBSnev4BxxLsXz2QuDSATSL6piEDHYXp9GEMw9cGOKQwHPIME7JV7VjoNDG_C0lAdTapsjbhhxQtL_BJvfGvN94dxnMmmR9e3CfWzVbhUprVt5Rqn4F8iXKvpiZ4drQ3XKz6smPMQNhJCDAfB2W323I2R3EqLV895g-0pndGAj-tDEXLazpeO9rGRKK9izXq5gZLry8FS_K2Oe9ol1p7vuepKO4d6AVpNf2Mc8ToWcJ907ce1NqSD5f5XghYvI6htGBP1BTy2zsABo9VbvGVmfv54AUGix-ifafiZrDUG6nsEdT2WLxU5VrzyXCgUq5gbY1heq3fKhoyUvag4JAFXpAjBCV6R0urFdMyqbhfwn40GxM4r3KxUPCLASsRxOOoeIlwtOU1CP9iNrSjpUwnqi5AVt8aj0KICxyFI6AaiMrO5FsS2dp5gjl4MzhPAPY1SHvQ28FKx4pRJvatyd2tjDUrV5bzro8-89NZvGIpcHhw3G281mEeV0JhR_1ZH5O4uRU7DAJ-YqJSAeJ4TjUIArwLnUPtiF_oXFGwVRgpIc1bRJKd1ZC1cfANriEZz1V330HoeGqdCGHPA9EwPJJlSj_xhyc5JwFPfOIoF2fTWsVBYZXcAs5w0OvUnlkkPGPjmRrNvVBh7dWPh68wADOgNOjWk1w3R4a5366xA8i-XbbMuLZM46hu3hJ1IHfEWJUuMfm9vpPlKPWnoH3PUqMDvHVIkpxSvuk4TaBINFrxuA6-baHyKMq7bV525yy_3i1fRjjGQ2xqlXJ-p6XaK7CXNuduZG_M1pheJsg9SNj-WDEszkbDIioNxVvsz44YVIbLVthrWynnER2IwZbpzWtyoZaozPqe2MwPLRiTW5g68VPniMKGlcaXdUp7-u_eYaWJu66eU6kx-COhYx5WZUtHdJvjefZDrIdm2dgOQu7UHNKGMPTqtlwxYLYkfkb9_J4gBUIuNhLOcNpLujJ9k-IqpS_ZBFreXOngLOSatLVdzCdW8iKHKhBaZtJ29bd04kByOwPmMLzIPLV6HPP-k4cNLN3zpAfNo6BRCpG4UJx6FpGDTmo6XSZQwUbN_sVJWhx2d80f_1H3jxpNk8PS1H-Sw7mFuY0b6LYiV4GXSMJOvSiJmOZCdSzJJt_Gn_PQ1TAXIWHrsAzhHdqDXXDA2YWWYmEGJ9w3ZlQ25sTSWg6Lsb4hPEVRfzJsoh3HieZYznvcvs8JqqtQv5T_Ja8pGGPKg1YFOrol18nZVcrpaZ2sCUjI78CzuHWIsoDc91fPNXFs6vR7rrdQwZIK1WUW4S9qlRCQevimNsEvJB8U6KKisowxUTuqhRy37TVdFoUcDORCMk7hOpwYzBtioAJM1iVto5R_XMcEmOZp7hFRd-7lhyaL2dunlWHmJLFJUUWemtcBJMuVur2THsVy253U1bB3fOo4HpAknSqOBN74KqyTpLkSRmAaqJnsprwNtWfEWAZibPWk4IBzWecJHK2_XtyYqCesT2JlrOk549hErH_dQvGajpgZ0ex8GQoxkCFwpnvjKuWtDUU7TkA2csV20z5n8-tZRktICtnEnJA1IAsyHjmNLY5lqNatRvYRCTzbWbzwK4gwcAj7SLN8SSVNT0VfjhSxjh8osGZf98MpLeE2sMBdZgFMnTT4V7nKTesRNAKme5avpIKSV9sxExyxQJsekjgvFitY_Zjen-dCMTdP326ynqOffzFvDqSYjfPARY-c0GABrPq5UmlUCzusgVQDJkm2S_euJ82YhwwWPf9cdiDzaId4lxump2mVV5nMrwTkJQDaU9z_M0fpR0Qwh1FfhhT8-2numRbd86gZvX-_d0j1vs2VtHaY41kTyLxDEYgrNypzqbUCVFCnNXsn9ok9gtCnSxLfdWMA4aF_-BHsgrdv5xbeeAlP4ggTD7RIdaaFf2aqiw0xAquhmh9LzOxcykmUkqiZ_CtJqCbcy0mXh2ymaLB75CfFEO3nUVCAWiuVy0)

- Use Case Diagram UML

    ![picture 7](https://i.imgur.com/p9CCzfe.png)  

- Entity Relationship Diagram (ERD)

    ![picture 8](https://i.imgur.com/5T99M5U.png)  

## Credits
**Wesley Frederick Oh**

**2306202763**

Shoutout to all users whose assets I exploited from Unity Asset Store.

