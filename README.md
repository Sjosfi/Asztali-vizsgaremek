# 🧠 BrainBoostAdmin

A **BrainBoostAdmin** egy adminisztrációs asztali alkalmazás, amely lehetővé teszi a BrainBoost webes és mobilalkalmazás mögött álló adatbázis közvetlen szerkesztését.

---

## ⚙️ Használat

1. **Töltsük le a legújabb release-t.**
2. **Indítsuk el az alkalmazást.**
3. **Admin felhasználóként be kell jelentkezni**, ezzel hitelesítve magunkat.
4. **A bal felső sarokban található lenyíló listából válasszuk ki a szerkeszteni kívánt táblát.**
5. **Szerkesztéshez**:
   - Kattintsunk a szerkeszteni kívánt elemre.
   - Írjuk be az új értéket.
   - Enter lenyomásával vagy mezőből való kikattintással az adat elmentésre kerül.
6. **Sor hozzáadásához**:
   - Görgessünk a lista végére.
   - Töltsük ki az adatok alatti üres sort (az `id` mezőt **nem kell megadni**, automatikusan generálódik).
7. **Sorok rendezése**:
   - Az adattag nevére kattintva a sorok **abc sorrendbe** rendezhetők.

---

## 🗓️ Verziótörténet

| Dátum           | Változás                                                                 |
|----------------|--------------------------------------------------------------------------|
| 2025.03.25      | A projekt felkerül GitHubra.                                             |
| 2025.03.26–04.05 | Adatbázis elérésének fejlesztése (hiba: rossz MySQL package telepítve). |
| 2025.04.08      | Az adatbázis szerkesztése működik, oszlopok abc szerint is rendezhetőek. |
| 2025.04.13      | Hozzáadás funkció működik. Az alkalmazás csak admin bejelentkezés után használható. |

---

## 🧪 Tesztelés

### 🧾 Általános

| Teszt                                | Elvárt eredmény                                    | Eredmény               |
|-------------------------------------|----------------------------------------------------|------------------------|
| Program indítása                    | Először egy bejelentkezési felület jelenik meg.    | ✅ Elvárt eredményt kapjuk |

---

### 🔐 Bejelentkezés / Hitelesítés

| Teszt                                             | Elvárt eredmény                                                                 | Eredmény                              |
|--------------------------------------------------|----------------------------------------------------------------------------------|----------------------------------------|
| Hibás email cím megadása                         | Felugró ablakban értesítés, hogy a megadott email cím hibás                      | ✅ Elvárt eredményt kapjuk              |
| Hibás jelszó megadása                            | Felugró ablakban értesítés, hogy a megadott jelszó hibás                         | ✅ Elvárt eredményt kapjuk              |
| Hibás email cím és jelszó                        | Értesítés, hogy a megadott email vagy jelszó hibás                               | ⚠️ Csak az email hibájáról értesít     |
| Email és jelszó mezők üresen hagyása             | Értesítés, hogy ne hagyjuk üresen a mezőket                                      | ⚠️ Az email cím helytelenségéről értesít |
| Helyes adatok megadása                           | Az alkalmazás tovább lép az admin felületre                                      | ✅ Elvárt eredményt kapjuk              |
| Helyes adatok, de nem fut az adatbázis           | A program hibaüzenetet ad ki                                                     | ✅ Elvárt eredményt kapjuk              |

---

### 🖥️ Admin felület

| Teszt                                                                 | Elvárt eredmény                                                                 | Eredmény                  |
|----------------------------------------------------------------------|----------------------------------------------------------------------------------|---------------------------|
| Táblák közötti váltás                                                | A kiválasztott tábla jelenik meg a lenyíló listából                             | ✅ Elvárt eredményt kapjuk |
| Tábla szerkesztése helyes adattal                                    | Az adat szerkesztése megtörténik, frissül az adatbázis                          | ✅ Elvárt eredményt kapjuk |
| Tábla szerkesztése nem megfelelő adattal                             | A program nem engedi a szerkesztést, a hibás mezőt pirossal jelöli              | ✅ Elvárt eredményt kapjuk |
| Sor hozzáadása helyes adattal                                        | A sor hozzáadása megtörténik, visszajelzés egy felugró ablakban                 | ✅ Elvárt eredményt kapjuk |
| Sor hozzáadása nem megfelelő adattal                                 | A program nem engedi a hozzáadást, hibás mezőt pirossal jelöli                  | ✅ Elvárt eredményt kapjuk |
| Új sor hozzáadásakor az `id` mező üresen hagyása                     | Az új sor automatikusan kap egy új ID-t                                         | ✅ Elvárt eredményt kapjuk |

---

## ℹ️ Egyéb megjegyzések

- A program kizárólag admin jogosultsággal érhető el.
- Az összes adatmanipuláció közvetlenül hat az adatbázisra.
- A rendszer figyel a hibás adattípusokra (pl. szöveg beírása szám helyett).
- Felhasználóbarát felület, azonnali visszajelzésekkel.

---

## 📌 Technológia (opcionálisan bővíthető)

- C# WPF alkalmazás
- MySQL adatbázis
- Admin jogosultság bcrypt-tel védett jelszóval
