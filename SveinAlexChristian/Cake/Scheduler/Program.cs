using System.ServiceProcess;
using Scheduler;

namespace Cake.Scheduler
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
                                {
                                    new CakeScheduleService()
                                };
            ServiceBase.Run(ServicesToRun);
        }
    }
}