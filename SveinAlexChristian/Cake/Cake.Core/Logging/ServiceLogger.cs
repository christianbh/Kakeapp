using System;
using System.IO;

namespace Cake.Core.Logging
{
    public class ServiceLogger
    {
        private readonly string _logPath;

        public ServiceLogger(string logPath)
        {
            _logPath = logPath;
        }

        public void Write(string logText)
        {
            if (!File.Exists(_logPath))
            {
                if(!CreateLogFile())
                    return;
            }

            File.AppendAllText(_logPath, logText + Environment.NewLine);
        }

        private bool CreateLogFile()
        {
            try
            {
                var stream = File.Create(_logPath);
                stream.Close();  
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

    }
}