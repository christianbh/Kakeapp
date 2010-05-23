using System;

namespace Statkraft.TCS.Kake.Domain
{
    public class Ferie
    {
        public Ferie(DateTime from, DateTime to)
        {
            FromDate = from;
            ToDate = to;
        }
        public DateTime FromDate { get; private set; }
        public DateTime ToDate { get; private set; }
    }
}
