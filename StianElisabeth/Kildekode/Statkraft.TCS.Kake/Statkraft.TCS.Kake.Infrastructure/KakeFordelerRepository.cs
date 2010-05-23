using System;
using System.Collections.Generic;
using System.IO;
using Statkraft.TCS.Kake.Domain;

namespace Statkraft.TCS.Kake.Infrastructure
{
    public class KakeFordelerRepository : IKakeFordelerRepository
    {
        public string FerierFilename { get; private set;}
        public string NesteKakeAnsvarligFilename { get; private set; }
        public string NesteKakeDatoFilename { get; private set; }
        public string MuligeKakeAnsvarligeFilename { get; private set; }

        public KakeFordelerRepository()
        {
            FerierFilename = "Ferier.txt";
            NesteKakeAnsvarligFilename = "NesteKakeAnsvarlig.txt";
            NesteKakeDatoFilename = "NesteKakeDato.txt";
            MuligeKakeAnsvarligeFilename = "MuligeKakeAnsvarlige.txt";
        }

        public IList<KakeAnsvarlig> GetMuligeKakeAnsvarlige()
        {
            IList<KakeAnsvarlig> kakeAnsvarlige = new List<KakeAnsvarlig>();

            string[] textLines = File.ReadAllLines(MuligeKakeAnsvarligeFilename);

            foreach (var line in textLines)
            {
                string[] splittedLine = line.Split(' ');
                string navn = splittedLine[0];
                string epost = splittedLine[1];

                KakeAnsvarlig kakeAnsvarlig = new KakeAnsvarlig(navn, epost);
                kakeAnsvarlige.Add(kakeAnsvarlig);
            }

            return kakeAnsvarlige;
        }

        public FerieOversikt GetFerieOversikt()
        {
            IList<Ferie> ferier = new List<Ferie>();

            string[] textLines = File.ReadAllLines(FerierFilename);

            foreach (var line in textLines)
            {
                string[] splittedLine = line.Split('-');
                DateTime start = DateTime.Parse(splittedLine[0]);
                DateTime end = DateTime.Parse(splittedLine[1]);

                Ferie ferie = new Ferie(start, end);
                ferier.Add(ferie);
            }

            return new FerieOversikt(ferier);
        }

        public KakeAnsvarlig GetNextKakeAnsvarlig()
        {
            string line = File.ReadAllText(NesteKakeAnsvarligFilename);
            string[] splittedLine = line.Split(' ');

            if (splittedLine.Length > 1)
            {
                return new KakeAnsvarlig(splittedLine[0], splittedLine[1]);
            }
            return null;
        }

        public void SaveNextKakeAnsvarlig(KakeAnsvarlig kakeAnsvarlig)
        {
            File.WriteAllText(NesteKakeAnsvarligFilename, kakeAnsvarlig.ToString());
        }

        public DateTime GetNextKakeDato()
        {
            string line = File.ReadAllText(NesteKakeDatoFilename);
            DateTime nextKakeDato;
            if (DateTime.TryParse(line, out nextKakeDato))
            {
                return nextKakeDato;
            }
            return DateTime.MinValue;
        }

        public void SaveNextKakeDato(DateTime kakeDato)
        {
            File.WriteAllText(NesteKakeDatoFilename, kakeDato.ToShortDateString());
        }
    }
}
