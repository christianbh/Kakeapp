using System;
using System.Collections.Generic;

namespace Statkraft.TCS.Kake.Domain
{
    public interface IKakeFordelerRepository
    {
        IList<KakeAnsvarlig> GetMuligeKakeAnsvarlige();
        FerieOversikt GetFerieOversikt();
        KakeAnsvarlig GetNextKakeAnsvarlig();
        void SaveNextKakeAnsvarlig(KakeAnsvarlig kakeAnsvarlig);
        DateTime GetNextKakeDato();
        void SaveNextKakeDato(DateTime kakeDato);
    }
}