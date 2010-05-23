using System.Web.Configuration;

namespace Cake.UI.Models.Settings
{
    public static class SiteSettings
    {
        static SiteSettings()
        {
            RepositoryPath = WebConfigurationManager.AppSettings.Get("RepositoryPath");
        }


        public static string RepositoryPath { get; set; }
    }
}