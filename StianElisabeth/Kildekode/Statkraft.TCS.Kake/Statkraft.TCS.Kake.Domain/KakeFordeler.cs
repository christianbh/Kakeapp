using System;
using System.Collections.Generic;
using System.Linq;

namespace Statkraft.TCS.Kake.Domain
{
    public class KakeFordeler : IKakeFordeler
    {
        public IList<KakeAnsvarlig> MuligeKakeAnsvarlige { get; private set; }
        public IFerieOversikt FerieOversikt { get; private set; }

        public KakeAnsvarlig NextKakeAnsvarlig { get; private set; }
        public DateTime NextKakeDato { get; private set; }

        public KakeFordeler(IList<KakeAnsvarlig> muligeKakeAnsvarlige, IFerieOversikt ferieOversikt, KakeAnsvarlig nextKakeAnsvarlig, DateTime nextKakeDato, DateTime today)
        {
            MuligeKakeAnsvarlige = muligeKakeAnsvarlige;
            FerieOversikt = ferieOversikt;
            
            NextKakeAnsvarlig = nextKakeAnsvarlig;
            if (NextKakeAnsvarlig == null)
            {
                FindNextKakeAnsvarlig();
            }

            NextKakeDato = nextKakeDato;
            if (NextKakeDato.Date < today.Date)
            {
                NextKakeDato = FindNextAvailableFriday(today.Date);
            }
        }

        public void FindNextKakeAnsvarlig()
        {
            int forrigeKakeAnsvarligIndex = MuligeKakeAnsvarlige.IndexOf(NextKakeAnsvarlig);
            
            int nesteKakeAnsvarligIndex = forrigeKakeAnsvarligIndex + 1;
            if (nesteKakeAnsvarligIndex >= MuligeKakeAnsvarlige.Count)
            {
                nesteKakeAnsvarligIndex = 0;
            }

            NextKakeAnsvarlig = MuligeKakeAnsvarlige[nesteKakeAnsvarligIndex];
        }

        public void FindNextKakeDato(DateTime now)
        {
            NextKakeDato = FindNextAvailableFriday(now.AddDays(7));
        }
        
        private DateTime FindNextAvailableFriday(DateTime now)
        {
            DateTime nextFriday = new Kalender().FindNextFriday(now);

            if (!FerieOversikt.IsHoliday(nextFriday))
            {
                return nextFriday;
            }
            return FindNextAvailableFriday(nextFriday);     
        }
    }
}