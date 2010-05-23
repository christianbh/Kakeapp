using System;
using System.Configuration;

namespace Cake.Core.Configuration
{
    public class AppConfigReader : IAppConfigReader
    {
        #region IAppConfigReader Members

        public int TimerRunInterval
        {
            get { return (int) GetAppConfigValue("TimerRunInterval", typeof (int)); }
        }

        public int HourOfDayToRunService
        {
            get { return (int) GetAppConfigValue("HourOfDayToRunService", typeof (int)); }
        }

        public int NumberOfDaysBeforeCakeDayToSendMail
        {
            get { return (int) GetAppConfigValue("NumberOfDaysBeforeCakeDayToSendMail", typeof (int)); }
        }


        public string FromMailAddress
        {
            get { return (string) GetAppConfigValue("FromMailAddress", typeof (string)); }
        }

        public string MailSubject
        {
            get { return (string) GetAppConfigValue("MailSubject", typeof (string)); }
        }

        public string MailBody
        {
            get { return (string) GetAppConfigValue("MailBody", typeof (string)); }
        }


        public string MailHost
        {
            get { return (string) GetAppConfigValue("MailHost", typeof (string)); }
        }


        public string RepositoryPath
        {
            get { return (string) GetAppConfigValue("RepositoryPath", typeof (string)); }
        }


        public string LogPath
        {
            get { return (string) GetAppConfigValue("LogPath", typeof (string)); }
        }

        #endregion

        private object GetAppConfigValue(string configName, Type type)
        {
            string value = ConfigurationManager.AppSettings[configName];

            if (type.Equals(typeof (int)))
                return Int32.Parse(value);
            if (type.Equals(typeof (bool)))
                return Boolean.Parse(value);


            return value;
        }
    }
}