using System;

namespace Statkraft.TCS.Kake.Domain
{
    public interface IKakeFordelerService
    {
        void Run(IKakeFordeler kakeFordeler, DateTime now);
        KakeFordeler Initialize();
    }
}