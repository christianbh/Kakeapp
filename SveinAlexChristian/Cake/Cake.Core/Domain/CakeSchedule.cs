using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cake.Core.Domain
{
    [Serializable]
    public class CakeSchedule
    {
        private readonly IList<DateTime> _holidays = new List<DateTime>();


        [DisplayName("Neste dato")]
        public DateTime NextDate { get; set; }

        [DisplayName("Feriedager")]
        public IList<DateTime> Holidays
        {
            get { return new List<DateTime>(_holidays).AsReadOnly(); }
        }

        public bool IsValidHolidayDate(DateTime holidayDate)
        {
            return Holidays.Contains(holidayDate);
        }

        public bool AddHoliday(DateTime holidayDate)
        {
            if (IsValidHolidayDate(holidayDate))
                return false;

            _holidays.Add(holidayDate);
            return true;
        }

        public bool DeleteHoliday(DateTime holidayDate)
        {
            if (IsValidHolidayDate(holidayDate) == false)
                return false;

            _holidays.Remove(holidayDate);
            return true;
        }

        public bool EditHoliday(DateTime exisitingHolidayDate, DateTime newHolidayDate)
        {
            if (IsValidHolidayDate(exisitingHolidayDate) == false || IsValidHolidayDate(newHolidayDate))
                return false;

            DeleteHoliday(exisitingHolidayDate);
            AddHoliday(newHolidayDate);

            return true;
        }
    }
}