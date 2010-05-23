using System;
using System.Collections.Generic;
using System.Linq;

namespace Statkraft.TCS.Kake.Domain
{
    public class FerieOversikt : IFerieOversikt
    {
        public IList<Ferie> Ferier { get; private set; }

        public FerieOversikt(IList<Ferie> ferier)
        {
            Ferier = ferier;
        }

        public virtual bool IsHoliday(DateTime day)
        {
            if (Ferier.Any(ferie => ferie.FromDate.Date >= day.Date && ferie.ToDate.Date <= day.Date))
            {
                return true;
            }
            return false;
        }
    }
}