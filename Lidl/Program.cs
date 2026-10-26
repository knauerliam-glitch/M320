using System;
using System.Collections.Generic;

namespace Lidl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Liste von Personen
            List<Person> personen = new List<Person>();

            Console.WriteLine("--- Personen erstellen ---");

            Kunde kunde1 = new Kunde();
            kunde1.Name = "Müller";
            kunde1.Vorname = "Anna";
            kunde1.Umsatz = 1000.50;

            Lagerist lagerist1 = new Lagerist();
            lagerist1.Name = "Schmidt";
            lagerist1.Vorname = "Peter";
            lagerist1.Groesse = 180.5;

            Kind kind1 = new Kind();
            kind1.Name = "Meier";
            kind1.Vorname = "Lena";
            kind1.Alter = 5;

            ChefLagerist chef1 = new ChefLagerist();
            chef1.Name = "Weber";
            chef1.Vorname = "Klaus";
            chef1.Groesse = 175.0;

            // Personen in die Liste einfügen
            personen.Add(kunde1);
            personen.Add(lagerist1);
            personen.Add(kind1);
            personen.Add(chef1);

            Console.WriteLine();
            Console.WriteLine("--- Über die Liste iterieren ---");

            foreach (Person p in personen)
            {
                Console.WriteLine();
                Console.WriteLine($"[{p.GetType().Name}] {p.Vorname} {p.Name}");

                // Diese Methoden kennt jede Person
                p.Esse();
                p.Tragen();

                // p.LagereEin("Milch");  // FEHLER: Person kennt LagereEin() nicht!

                // Spezielle Methoden nur nach Typprüfung (Cast)
                if (p is Kunde k)
                {
                    k.Kaufen();
                }

                if (p is Lagerist l)          // trifft auch auf ChefLagerist zu
                {
                    l.LagereEin("Milch");
                }

                if (p is ChefLagerist c)
                {
                    c.LagereAus("Milch");
                }
            }

            Console.WriteLine();
            Lager.ZeigeInhalt();
        }
    }

    // Einfaches Lager, in das ein- und ausgelagert wird
    public static class Lager
    {
        private static List<string> materialien = new List<string>();

        public static void Hinzufuegen(string material)
        {
            materialien.Add(material);
        }

        public static bool Entfernen(string material)
        {
            return materialien.Remove(material);
        }

        public static void ZeigeInhalt()
        {
            Console.WriteLine("Lagerinhalt: " +
                (materialien.Count == 0 ? "(leer)" : string.Join(", ", materialien)));
        }
    }

    public class Person
    {
        private static int naechsteNummer = 1;

        public string Name { get; set; }
        public string Vorname { get; set; }

        private int key;
        protected int personenNummer;

        public Person()
        {
            personenNummer = naechsteNummer++;
            Console.WriteLine("Konstruktor Person");
        }

        public virtual void Esse()
        {
            Console.WriteLine($"{Name} (Nr. {personenNummer}) isst.");
        }

        public virtual void Tragen()
        {
            Console.WriteLine($"{Name} {Vorname} trägt den Einkauf.");
        }
    }

    public class Kunde : Person
    {
        public double Umsatz { get; set; }

        public Kunde()
        {
            Console.WriteLine("Konstruktor Kunde");
        }

        public void Kaufen()
        {
            Console.WriteLine($"{Name} {Vorname} kauft.");
        }
    }

    public class Lagerist : Person
    {
        public double Groesse { get; set; }

        public Lagerist()
        {
            Console.WriteLine("Konstruktor Lagerist");
        }

        public void LagereEin(string material)
        {
            Lager.Hinzufuegen(material);
            Console.WriteLine($"{Name} {Vorname} lagert {material} ein.");
        }

        public override void Tragen()
        {
            Console.WriteLine($"{Name} {Vorname} trägt die Ware ins Lager.");
        }
    }

    // ChefLagerist IST ein Lagerist und kann zusätzlich auslagern.
    // Tragen() wird NICHT überschrieben -> er trägt wie ein Lagerist.
    public class ChefLagerist : Lagerist
    {
        public ChefLagerist()
        {
            Console.WriteLine("Konstruktor ChefLagerist");
        }

        public void LagereAus(string material)
        {
            if (Lager.Entfernen(material))
                Console.WriteLine($"{Name} {Vorname} lagert {material} aus.");
            else
                Console.WriteLine($"{Name} {Vorname}: {material} ist nicht im Lager.");
        }
    }

    public class Kind : Person
    {
        public int Alter { get; set; }

        public Kind()
        {
            Console.WriteLine("Konstruktor Kind");
        }

        public override void Tragen()
        {
            Console.WriteLine($"{Name} {Vorname} wird getragen.");
        }

        public override void Esse()
        {
            Console.WriteLine($"{Name} {Vorname} wird gefüttert.");
        }
    }
}