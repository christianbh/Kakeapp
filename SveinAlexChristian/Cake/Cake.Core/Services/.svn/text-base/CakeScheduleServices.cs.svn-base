using System;
using System.Linq;
using Cake.Core.Domain;

namespace Cake.Core.Services
{
    public class CakeScheduleServices : ICakeScheduleServices
    {
        #region ICakeScheduleServices Members

        public CakeSchedule SetNextCakeDate(CakeSchedule cakeSchedule, DateTime currentDate)
        {
            if (cakeSchedule == null)
                cakeSchedule = CreateCakeSchedule();


            cakeSchedule.NextDate = GetSecondFridayFromToday(currentDate);

            cakeSchedule = AdjustNextCakeDateForHollidays(cakeSchedule);

            return cakeSchedule;
        }

        #endregion

        private CakeSchedule CreateCakeSchedule()
        {
            return new CakeSchedule
                       {
                           NextDate = DateTime.Now
                       };
        }

        private DateTime GetSecondFridayFromToday(DateTime currentDate)
        {
            DateTime nextFriday = currentDate;

            if (nextFriday.DayOfWeek == DayOfWeek.Friday)
            {
                return nextFriday.AddDays(14);
            }


            while (nextFriday.DayOfWeek != DayOfWeek.Friday)
            {
                nextFriday = nextFriday.AddDays(1);
            }

            return nextFriday.AddDays(7);
        }

        private CakeSchedule AdjustNextCakeDateForHollidays(CakeSchedule cakeSchedule)
        {
            if (cakeSchedule.Holidays.Count == 0)
            {
                return cakeSchedule;
            }

            while ((from d in cakeSchedule.Holidays
                    where d.Date == cakeSchedule.NextDate.Date
                    select d).Any())
            {
                cakeSchedule.NextDate = cakeSchedule.NextDate.AddDays(7);
            }

            return cakeSchedule;
        }
    }
}