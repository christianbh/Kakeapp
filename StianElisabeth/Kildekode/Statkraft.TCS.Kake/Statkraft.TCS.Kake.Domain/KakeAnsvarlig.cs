namespace Statkraft.TCS.Kake.Domain
{
    public class KakeAnsvarlig
    {
        public KakeAnsvarlig(string navn, string epost)
        {
            Navn = navn;
            Epost = epost;
        }

        public string Epost { get; private set; }
        public string Navn { get; private set; }

        public override string ToString()
        {
            return Navn + " " + Epost;
        }

        public override bool Equals(object ansvarlig)
        {
            if (ansvarlig is KakeAnsvarlig)
            {
                return ((KakeAnsvarlig)ansvarlig).Navn.Equals(Navn);
            }
            return base.Equals(ansvarlig);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        
    }
}
