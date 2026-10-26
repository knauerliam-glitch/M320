# M320

## Workshop "Vererbung, Polymorphie, Interface" – AlarmWorkshop

Projektmappe: `AlarmWorkshop/AlarmWorkshop.sln`, Code: `AlarmWorkshop/AlarmWorkshop/Program.cs`,
Klassendiagramm zum Startcode: `AlarmWorkshop/Klassendiagramm-Start.png`.

Ausgabe des fertigen Grundwegs:

```
Selbsttest: Gerät bereit.
Selbsttest: Lampe leuchtet kurz.
Kommunikationsgerät: Verbindung hergestellt.
Sirene: Tatü tata!
Warnlampe: Rotes Licht blinkt!
Smartphone: Alarmnachricht und Vibration!
Bildschirmmeldung: WARNUNG!
```

### Antworten (Etappe 12)

1. **Klasse und Objekt:** Die Klasse `Sirene` ist mit `class Sirene : Alarmgeraet { ... }` definiert (der Bauplan). Ein Sirenenobjekt entsteht in `Main` mit `new Sirene()` in der Zeile `Alarmgeraet sirene = new Sirene();` (das konkrete Exemplar).
2. **Typ und Objekt:** `Sirene` erbt von `Alarmgeraet`, eine Sirene *ist also ein* Alarmgerät. Darum darf eine Variable vom Typ `Alarmgeraet` auf sie verweisen. Der deklarierte Typ bestimmt nur, welche Methoden der Compiler über die Variable zulässt – das Objekt selbst wird mit `new Sirene()` erzeugt und bleibt eine Sirene.
3. **Vererbung:** Die Sirene übernimmt die fertige Methode `Selbsttest()` aus `Alarmgeraet` („Selbsttest: Gerät bereit.“), weil sie diese nicht selbst überschreibt.
4. **Abstrakt und virtuell:** `Warnen()` ist in `Alarmgeraet` `abstract` und hat keinen Rumpf. Ohne eigene Umsetzung in `Sirene` bleibt eine abstrakte Methode offen → Compilerfehler (CS0534). `Selbsttest()` ist `virtual` und hat bereits einen Rumpf. Fehlt die Überschreibung in `Warnlampe`, wird einfach die Basisversion verwendet → kein Fehler, nur die Ausgabe „Selbsttest: Gerät bereit.“
5. **Override:** `Sirene.Warnen()` setzt die abstrakte Methode der Basisklasse `Alarmgeraet` um, deshalb `override`. Die Basisklasse des Smartphones, `Kommunikationsgeraet`, hat keine virtuelle oder abstrakte `Warnen()`-Methode – es gibt nichts zu überschreiben. Das Smartphone erfüllt nur die Vereinbarung aus dem Interface, dafür braucht es kein `override`.
6. **Interface:** `IAlarmgeber` vereinbart genau eine Fähigkeit: `void Warnen();`. `Alarmgeraet` (und damit Sirene und Warnlampe) sowie `Smartphone` und `Bildschirmmeldung` geben das Interface in ihrer class declaration an (`: Alarmgeraet`-Hierarchie bzw. `: Kommunikationsgeraet, IAlarmgeber`). So können Klassen mit unterschiedlichen Basisklassen dieselbe Vereinbarung erfüllen, obwohl C# nur eine Basisklasse erlaubt.
7. **Polymorphie:** In der Schleife hat `geraet` immer den deklarierten Typ `IAlarmgeber`, und `Testalarm(geraet)` ruft immer `geraet.Warnen()` auf. Zur Laufzeit wird aber die Implementierung des tatsächlichen Objekts ausgeführt: Durchlauf 1 `Sirene`, 2 `Warnlampe`, 3 `Smartphone`, 4 `Bildschirmmeldung` → vier verschiedene Warnungen.
8. **Erweiterung:** Für die Bildschirmmeldung habe ich nur die neue Klasse `class Bildschirmmeldung : IAlarmgeber` und den Array-Eintrag `new Bildschirmmeldung()` ergänzt. Die `foreach`-Schleife, `Testalarm`, das Interface und alle bisherigen Klassen blieben unverändert.

**Mit der Basisklasse teilen unsere Alarmgeräte** gemeinsamen Code (den fertigen `Selbsttest`) und die Pflicht, `Warnen` umzusetzen.
**Mit dem Interface vereinbaren unterschiedliche Klassen** eine gemeinsame Fähigkeit (`Warnen`), auch wenn sie aus verschiedenen Klassenhierarchien stammen.
**Testalarm muss über ein Objekt nur wissen, dass** es `IAlarmgeber` erfüllt und darum `Warnen()` anbietet.

### Tabellen und Beobachtungen

**Etappe 1 – Vorhersage = Ausgabe Startcode:** „Selbsttest: Gerät bereit.“, „Sirene: Tatü tata!“, „Selbsttest: Lampe leuchtet kurz.“, „Warnlampe: Rotes Licht blinkt!“

**Etappe 2 – erste Zeile in Main:** Variable `sirene`, deklarierter Typ `Alarmgeraet`, Klasse hinter `new`: `Sirene`, zugewiesen wird ein Verweis auf das neue Sirenenobjekt. Mit `new Warnlampe()` werden die ersten zwei Zeilen zu den Warnlampen-Ausgaben – der Name `sirene` ist dann irreführend, weil er nichts über das tatsächliche Objekt aussagt.

**Etappe 3:** Sirene erbt von `Alarmgeraet`; der verwendete Selbsttest-Rumpf steht in `Alarmgeraet`; die Warn-Implementierung steht in `Sirene`; über `Alarmgeraet` aufrufbar: `Warnen()` und `Selbsttest()`.

**Etappe 4:** A: Fehler CS0534 – der konkreten Klasse fehlt die Umsetzung der abstrakten Methode `Alarmgeraet.Warnen()`. B: Fehler CS0144 – von der abstrakten Klasse `Alarmgeraet` kann kein Objekt erzeugt werden. Als Variablentyp ist sie aber erlaubt.

**Etappe 5:**

| Schlüsselwort | Bedeutung im Code |
|---|---|
| `abstract` bei einer Methode | nur Methodenkopf, kein Rumpf; jede konkrete Unterklasse muss sie umsetzen |
| `virtual` bei einer Methode | hat einen Standard-Rumpf, Unterklassen dürfen ihn überschreiben |
| `override` in der Unterklasse | ersetzt die abstrakte/virtuelle Methode der Basisklasse durch eine eigene Implementierung |

**Etappe 6:**

| Aufruf | Deklarierter Parametertyp | Tatsächliche Klasse | Ausgeführte Warn-Implementierung |
|---|---|---|---|
| `Testalarm(sirene)` | `Alarmgeraet` | `Sirene` | `Sirene.Warnen()` |
| `Testalarm(warnlampe)` | `Alarmgeraet` | `Warnlampe` | `Warnlampe.Warnen()` |

**Etappe 7:** Ein gleicher Methodenname reicht nicht: `Testalarm(smartphone)` ergibt CS1503 (kann nicht von `Smartphone` in `Alarmgeraet` konvertiert werden), weil `Smartphone` von `Kommunikationsgeraet` erbt. Testalarm braucht einen gemeinsamen Typ für Objekte, die warnen können.

**Etappe 9:** Elementtyp `IAlarmgeber`, weil nur er auf alle Objekte passt *und* `Warnen()` anbietet (`Alarmgeraet` passt nicht zum Smartphone, `Kommunikationsgeraet` nicht zu Sirene/Warnlampe und hat kein `Warnen`). Das Array speichert nur Verweise – es entsteht kein neues Gerät und keine Warnung wird ausgelöst.

**Etappe 10:**

| Durchlauf | Deklarierter Typ | Tatsächliche Klasse | Ausgeführte Warn-Implementierung |
|---|---|---|---|
| 1 | `IAlarmgeber` | `Sirene` | `Sirene.Warnen()` |
| 2 | `IAlarmgeber` | `Warnlampe` | `Warnlampe.Warnen()` |
| 3 | `IAlarmgeber` | `Smartphone` | `Smartphone.Warnen()` |
| 4 | `IAlarmgeber` | `Bildschirmmeldung` | `Bildschirmmeldung.Warnen()` |

Über eine Variable vom Typ `IAlarmgeber` lässt sich `Verbinden()` nicht aufrufen (CS1061), obwohl das Objekt ein Smartphone bleibt – der deklarierte Typ bestimmt die sichtbaren Methoden.
