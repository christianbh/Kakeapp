using System;

namespace Statkraft.TCS.Kake.Domain
{
    public interface IFerieOversikt
    {
        bool IsHoliday(DateTime day);
    }
}