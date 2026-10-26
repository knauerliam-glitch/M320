using System;

namespace Warenwirtschaft
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("=== Produkt ===");
            Produkt produkt = new Produkt();
            Console.WriteLine("Initialwerte:");
            produkt.Ausgeben();

            produkt.Artikelnummer = 1001;
            produkt.Name = "Kaffeemaschine";
            produkt.Nettopreis = 89.90;
            Console.WriteLine("Nach dem Setzen:");
            produkt.Ausgeben();

          
            Console.WriteLine();
            Console.WriteLine("=== MusikCD ===");
            MusikCD cd = new MusikCD();
            Console.WriteLine("Initialwerte:");
            cd.Ausgeben();

            cd.Artikelnummer = 2001;
            cd.Name = "Greatest Hits";
            cd.Nettopreis = 14.95;
            cd.Laufzeit = 62;
            cd.Interpret = "Die Beispielband";
            Console.WriteLine("Nach dem Setzen:");
            Console.WriteLine($"Artikelnummer: {cd.Artikelnummer}");
            Console.WriteLine($"Name:          {cd.Name}");
            Console.WriteLine($"Nettopreis:    {cd.Nettopreis:F2} CHF");
            Console.WriteLine($"Laufzeit:      {cd.Laufzeit} Minuten");
            Console.WriteLine($"Interpret:     {cd.Interpret}");

            Console.WriteLine();
            Console.WriteLine("=== Kinderfahrrad ===");
            Kinderfahrrad fahrrad = new Kinderfahrrad();
            Console.WriteLine("Initialwerte:");
            fahrrad.Ausgeben();

            fahrrad.Artikelnummer = 3001;
            fahrrad.Name = "Flitzer 16 Zoll";
            fahrrad.Nettopreis = 199.00;
            fahrrad.KlingelAuswaehlen("pink");
            Console.WriteLine("Nach dem Setzen:");
            fahrrad.Ausgeben();
        }
    }

    
    public class Produkt
    {
        private int artikelnummer = 0;
        private string name = "unbekannt";
        private double nettopreis = 0.0;

        public int Artikelnummer
        {
            get { return artikelnummer; }
            set { artikelnummer = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double Nettopreis
        {
            get { return nettopreis; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Der Preis darf nicht negativ sein.");
                nettopreis = value;
            }
        }

        public virtual void Ausgeben()
        {
            Console.WriteLine($"  Artikelnummer: {Artikelnummer}");
            Console.WriteLine($"  Name:          {Name}");
            Console.WriteLine($"  Nettopreis:    {Nettopreis:F2} CHF");
        }
    }

   
    public class MusikCD : Produkt
    {
        private int laufzeit = 0;              
        private string interpret = "unbekannt";

        public int Laufzeit
        {
            get { return laufzeit; }
            set { laufzeit = value; }
        }

        public string Interpret
        {
            get { return interpret; }
            set { interpret = value; }
        }

        public override void Ausgeben()
        {
            base.Ausgeben();
            Console.WriteLine($"  Laufzeit:      {Laufzeit} Minuten");
            Console.WriteLine($"  Interpret:     {Interpret}");
        }
    }

    
    public class Kinderfahrrad : Produkt
    {
        private string klingelFarbe = "silber";

        public string KlingelFarbe
        {
            get { return klingelFarbe; }
        }

        public void KlingelAuswaehlen(string klingelFarbe)
        {
            this.klingelFarbe = klingelFarbe;
            Console.WriteLine($"  Klingel in der Farbe '{klingelFarbe}' wurde ausgewählt.");
        }

        public override void Ausgeben()
        {
            base.Ausgeben();
            Console.WriteLine($"  Klingelfarbe:  {KlingelFarbe}");
        }
    }
}