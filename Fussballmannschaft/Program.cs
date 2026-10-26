using System;
using System.Collections.Generic;

namespace Fussball
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Fussballmannschaft heim = ErstelleMannschaft(
                "FC Staad",
                new[] { "Keller", "Brunner", "Huber", "Frei", "Baumann",
                        "Gerber", "Moser", "Steiner", "Fischer", "Wyss" },
                "Zimmermann");

            Fussballmannschaft gast = ErstelleMannschaft(
                "FC Wittenbach",
                new[] { "Weber", "Schneider", "Meier", "Graf", "Roth",
                        "Kunz", "Bachmann", "Sutter", "Vogel", "Hofer" },
                "Lehmann");

           
            Spiel spiel = new Spiel(heim, gast);
            spiel.Spielen(5);

            Console.WriteLine();
            Console.WriteLine("=================================");
            Console.WriteLine(spiel.Resultat);
            Console.WriteLine("=================================");
        }

        
        static Fussballmannschaft ErstelleMannschaft(string name, string[] feldspielerNamen, string torwartName)
        {
            List<Feldspieler> feldspieler = new List<Feldspieler>();
            foreach (string n in feldspielerNamen)
            {
                feldspieler.Add(new Feldspieler(n));
            }

            Torwart torwart = new Torwart(torwartName);

            return new Fussballmannschaft(name, feldspieler, torwart);
        }
    }

    public class Feldspieler
    {
        public string Name { get; }   

        public Feldspieler(string name)
        {
            Name = name;
        }

        public void AufsTorSchiessen()
        {
            Console.WriteLine($"{Name} schiesst aufs Tor!");
        }

        public void Graetschen()
        {
            Console.WriteLine($"{Name} macht eine Blutgraetsche!");
        }

        public void Dribbeln()
        {
            Console.WriteLine($"{Name} dribbelt den Gegner schwindlig!");
        }
    }

    
    public class Torwart
    {
        public string Name { get; }   

        public Torwart(string name)
        {
            Name = name;
        }

        public void SchussHalten()
        {
            Console.WriteLine($"{Name} hält den Schuss vom Gegner!");
        }

        public void Abstoss()
        {
            Console.WriteLine($"{Name} macht einen Abstoss!");
        }
    }

   
    public class Fussballmannschaft
    {
        private static readonly Random zufall = new Random();

        public string Name { get; }   

        private readonly List<Feldspieler> feldspieler;
        private readonly Torwart torwart;

        public Fussballmannschaft(string name, List<Feldspieler> feldspieler, Torwart torwart)
        {
            if (feldspieler == null || feldspieler.Count != 10)
                throw new ArgumentException("Eine Mannschaft braucht genau 10 Feldspieler.");
            if (torwart == null)
                throw new ArgumentException("Eine Mannschaft braucht genau 1 Torwart.");

            Name = name;
            this.feldspieler = feldspieler;
            this.torwart = torwart;
        }

        public int Spielzug()
        {
            Feldspieler dribbler = feldspieler[zufall.Next(feldspieler.Count)];
            Feldspieler verteidiger = feldspieler[zufall.Next(feldspieler.Count)];
            Feldspieler schuetze = feldspieler[zufall.Next(feldspieler.Count)];

            torwart.Abstoss();
            dribbler.Dribbeln();
            verteidiger.Graetschen();
            schuetze.AufsTorSchiessen();
            torwart.SchussHalten();

            return zufall.Next(-1, 2);   
        }
    }

    
    public class Spiel
    {
        public Fussballmannschaft Heim { get; }
        public Fussballmannschaft Gast { get; }
        public Resultat Resultat { get; }

        public Spiel(Fussballmannschaft heim, Fussballmannschaft gast)
        {
            Heim = heim;
            Gast = gast;

           
            Resultat = new Resultat(heim.Name, gast.Name);
        }

        public void Spielen(int spielzuegeProMannschaft)
        {
            Console.WriteLine($"Anpfiff: {Heim.Name} gegen {Gast.Name}");

            for (int i = 1; i <= spielzuegeProMannschaft; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"--- Spielzug {i}: {Heim.Name} ---");
                Auswerten(Heim.Spielzug(), heimHatBall: true);

                Console.WriteLine();
                Console.WriteLine($"--- Spielzug {i}: {Gast.Name} ---");
                Auswerten(Gast.Spielzug(), heimHatBall: false);
            }

            Console.WriteLine();
            Console.WriteLine("Abpfiff!");
        }

        private void Auswerten(int ergebnis, bool heimHatBall)
        {
            if (ergebnis == 0)
            {
                Console.WriteLine("Kein Tor.");
                return;
            }

           
            bool torFuerHeim = heimHatBall ? ergebnis > 0 : ergebnis < 0;

            if (torFuerHeim)
            {
                Resultat.TorHeim();
                Console.WriteLine($"TOR für {Heim.Name}!");
            }
            else
            {
                Resultat.TorGast();
                Console.WriteLine($"TOR für {Gast.Name}!");
            }
        }
    }

    public class Resultat
    {
        public string HeimName { get; }
        public string GastName { get; }
        public int ToreHeim { get; private set; }
        public int ToreGast { get; private set; }

        public Resultat(string heimName, string gastName)
        {
            HeimName = heimName;
            GastName = gastName;
        }

        public void TorHeim() => ToreHeim++;
        public void TorGast() => ToreGast++;

        public override string ToString()
        {
            string sieger =
                ToreHeim > ToreGast ? $"Sieger: {HeimName}" :
                ToreGast > ToreHeim ? $"Sieger: {GastName}" :
                "Unentschieden";

            return $"Endresultat: {HeimName} {ToreHeim} : {ToreGast} {GastName}  ({sieger})";
        }
    }
}
