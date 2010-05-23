using System.ComponentModel;
using System.Configuration.Install;

namespace Cake.Scheduler
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}