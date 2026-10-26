using System;

internal class Program {
    private static void Main() {
        Alarmgeraet sirene = new Sirene();
        Alarmgeraet warnlampe = new Warnlampe();
        Smartphone smartphone = new Smartphone();

        sirene.Selbsttest();
        warnlampe.Selbsttest();
        smartphone.Verbinden();

        IAlarmgeber[] alarmgeber = {
            sirene,
            warnlampe,
            smartphone,
            new Bildschirmmeldung()
        };

        foreach (IAlarmgeber geraet in alarmgeber) {
            Testalarm(geraet);
        }
    }

    private static void Testalarm(IAlarmgeber geraet) {
        geraet.Warnen();
    }
}

public interface IAlarmgeber {
    void Warnen();
}

abstract class Alarmgeraet : IAlarmgeber {
    public abstract void Warnen();

    public virtual void Selbsttest() {
        Console.WriteLine("Selbsttest: Gerät bereit.");
    }
}

class Sirene : Alarmgeraet {
    public override void Warnen() {
        Console.WriteLine("Sirene: Tatü tata!");
    }
}

class Warnlampe : Alarmgeraet {
    public override void Warnen() {
        Console.WriteLine("Warnlampe: Rotes Licht blinkt!");
    }

    public override void Selbsttest() {
        Console.WriteLine("Selbsttest: Lampe leuchtet kurz.");
    }
}

class Kommunikationsgeraet {
    public void Verbinden() {
        Console.WriteLine("Kommunikationsgerät: Verbindung hergestellt.");
    }
}

class Smartphone : Kommunikationsgeraet, IAlarmgeber {
    public void Warnen() {
        Console.WriteLine("Smartphone: Alarmnachricht und Vibration!");
    }
}

class Bildschirmmeldung : IAlarmgeber {
    public void Warnen() {
        Console.WriteLine("Bildschirmmeldung: WARNUNG!");
    }
}
