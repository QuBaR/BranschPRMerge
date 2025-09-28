
# 🧪 Övning: Branch → PR → **Merge** (utan konflikter)

**Mål:** Alla studenter tränar Git-flöde i ett gemensamt repo och får in sin ändring i `master` utan konflikter.

## Förutsättningar

* Åtkomst till lärarens GitHub-repo
* Git installerat + GitHub-konto

---

## 1) Klona repot

```bash
git clone https://github.com/QuBaR/BranschPRMerge
cd BranschPRMerge
git status
```

## 2) Skapa egen branch + klassfil

Skapa **egen branch** (ersätt med ditt namn utan å/ä/ö och specialtecken):

```bash
git checkout -b feature/<FörnamnEfternamn>
```

Skapa **egen klassfil** i mappen `Students/`:

* Filnamn: `Students/<FörnamnEfternamn>.cs`
* Exempel:

```csharp
// Students/RobertJansz.cs
namespace Students;

public static class RobertJansz
{
    public static string Name => "Robert Jansz";
    public static string SayHello() => $"Hej från {Name}!";
}
```

Commit:

```bash
git add .
git commit -m "Add <FörnamnEfternamn> class in Students/"
```

## 3) Synka med senaste `master` via **merge** och pusha

Uppdatera din lokala `master` och **merga** in i din feature-branch:

```bash
git checkout master
git pull origin master           # hämta senaste master
git checkout feature/<FörnamnEfternamn>
git merge master                 # MERGE (ingen rebase)
# …lös ev. konflikter, redigera filer…
git add .
git commit                     # avsluta mergens commit om konflikter fanns
git push -u origin feature/<FörnamnEfternamn>
```

Skapa **Pull Request (PR)** på GitHub:

* Titel: `Add <FörnamnEfternamn> class`
* Beskriv kort vad du lagt till.
* Kontrollera att PR:en är “able to merge” (inga konflikter).

## 4) Review & merge (lärare)

* Läraren godkänner PR och **mergar med “Create a merge commit”** (standard-merge).
* **Radera branchen** på GitHub efter merge.

Städa lokalt (valfritt):

```bash
git checkout master
git pull origin master
git branch -d feature/<FörnamnEfternamn>
git fetch -p
```

## 5) Nästa elev kör

När första PR:en är mergad och branchen raderad, går nästa elev igenom steg 1–4.

---

## Regler för att undvika konflikter

* **Endast en ny fil** per elev i `Students/`—rör inte andras filer.
* **Unikt filnamn & klassnamn**: `<FörnamnEfternamn>.cs`.
* **Alltid** `git pull origin master` på `master` och **merge** `master` in i din branch **före** PR.

---

## Vanliga problem & snabblösningar

* **“Updates were rejected…” (din branch ligger efter)**
  Uppdatera och **merga**:

  ```bash
  git checkout master
  git pull origin master
  git checkout feature/<FörnamnEfternamn>
  git merge master
  git push
  ```
* **Konflikt vid merge**
  Öppna filerna som Git markerat, lös konflikterna, kör:

  ```bash
  git add .
  git commit
  git push
  ```
* **Fel filnamn/tecken**
  Byt till exakt `Students/<FörnamnEfternamn>.cs` (inga å/ä/ö/specialtecken).

---

## Godkänt när

* Egen branch skapad och pushad.
* PR skapad, grön (mergable) och mergad till `master`.
* Branch raderad efter merge.

Vill du att jag adderar en **.github/pull_request_template.md** och en `.gitignore` för .NET åt dig också?
