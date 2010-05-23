using System;
using System.ComponentModel;

namespace Cake.UI.Models.Forms
{
    public class HolidayEditForm
    {
        private DateTime _newDate;

        public HolidayEditForm(DateTime existingDate)
        {
            ExistingDate = existingDate;
        }

        [DisplayName("Dato")]
        public DateTime ExistingDate { get; private set; }

        [DisplayName("Ny dato")]
        public DateTime NewDate
        {
            get
            {
                if (_newDate < DateTime.Now)
                    return DateTime.Now;
                return _newDate;
            }
            set { _newDate = value; }
        }
    }
}