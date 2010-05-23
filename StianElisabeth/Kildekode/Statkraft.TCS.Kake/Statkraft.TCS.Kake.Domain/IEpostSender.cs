using System;

namespace Statkraft.TCS.Kake.Domain
{
    public interface IEpostSender
    {
        void Send(KakeAnsvarlig kakeAnsvarlig, DateTime kakeDato);
    }
}