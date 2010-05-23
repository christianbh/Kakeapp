using System;

namespace Statkraft.TCS.Kake.Domain
{
    public class Kalender
    {
        private const int daysInWeek = 7;
        public DateTime FindNextFriday(DateTime today)
        {
            var friday = DayOfWeek.Friday;
            
            if (today.DayOfWeek == friday)
            {
                return today.AddDays(daysInWeek);
            }
            if (today.DayOfWeek < friday)
            {
                return today.AddDays(friday - today.DayOfWeek);
            }
            if(today.DayOfWeek > friday)
            {
                var endOfWeek = today.AddDays((int)today.DayOfWeek - daysInWeek);
                return FindNextFriday(endOfWeek);
            }
            
            return DateTime.Now;
        }
    }
}