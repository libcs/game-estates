using System.Configuration;

namespace Gamer.ProxyService
{
    internal class Config
    {
        public static string ProxyHost => ConfigurationManager.AppSettings["ProxyHost"];
    }
}