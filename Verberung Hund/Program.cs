using System;

namespace Hunde
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Console.WriteLine("=== Hund ===");
            Hund hund = new Hund();
            hund.Groesse = 45;                                   
            Console.WriteLine($"Groesse: {hund.Groesse} cm");    
            hund.Bellen();

            Console.WriteLine();
            Console.WriteLine("=== Jagdhund ===");
            Jagdhund jagdhund = new Jagdhund();
            jagdhund.Groesse = 60;                               
            jagdhund.Geschwindigkeit = 45;                       
            Console.WriteLine($"Groesse: {jagdhund.Groesse} cm");
            Console.WriteLine($"Geschwindigkeit: {jagdhund.Geschwindigkeit} km/h");
            jagdhund.Bellen();                                   
            jagdhund.Fass("Kaninchen");                          

         
            Console.WriteLine();
            Console.WriteLine("=== EntfesselterJagdhund ===");
            EntfesselterJagdhund wilder = new EntfesselterJagdhund();
            wilder.Groesse = 75;
            wilder.Geschwindigkeit = 60;
            wilder.Blutdurst = "unstillbar";
            Console.WriteLine($"Groesse: {wilder.Groesse} cm");
            Console.WriteLine($"Geschwindigkeit: {wilder.Geschwindigkeit} km/h");
            Console.WriteLine($"Blutdurst: {wilder.Blutdurst}");
            wilder.Bellen();
            wilder.Fass("Wildschwein");
            wilder.Zerfetze(3);
        }
    }

  
    public class Hund
    {
        private int groesse;

        public int Groesse            
        {
            get { return groesse; }
            set { groesse = value; }
        }

        public void Bellen()
        {
            Console.WriteLine("Wuff Wuff");
        }
    }

    public class Jagdhund : Hund
    {
        private int geschwindigkeit;

        public int Geschwindigkeit    
        {
            get { return geschwindigkeit; }
            set { geschwindigkeit = value; }
        }

        
        public virtual void Fass(string nameDesTiers)
        {
            Console.WriteLine($"Ich habe das {nameDesTiers} geschnappt.");
        }
    }

 
    public class EntfesselterJagdhund : Jagdhund
    {
        private string blutdurst;

        public string Blutdurst
        {
            get { return blutdurst; }
            set { blutdurst = value; }
        }

        
        public override void Fass(string nameDesTiers)
        {
            Console.WriteLine($"GRRRR! Mit {Geschwindigkeit} km/h stürze ich mich auf das {nameDesTiers}!");
            Console.WriteLine($"Das {nameDesTiers} hat keine Chance mehr!");
        }

        public void Zerfetze(int zeitdauer)
        {
            Console.WriteLine($"Ich wüte {zeitdauer} Minuten lang wie ein Tornado durch das Unterholz!");
            Console.WriteLine("Danach bleibt kein Blatt mehr am Busch.");
        }
    }
}