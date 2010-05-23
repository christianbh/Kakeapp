using System;

namespace Statkraft.TCS.Kake.Domain
{
    public interface IKakeFordeler
    {
        void FindNextKakeAnsvarlig();
        void FindNextKakeDato(DateTime now);
        KakeAnsvarlig NextKakeAnsvarlig { get; }
        DateTime NextKakeDato { get; }
    }
}