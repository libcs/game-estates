using System.Configuration;

namespace Game.NetstreamService
{
    internal class Config
    {
        public static string StreamHost => ConfigurationManager.AppSettings["StreamHost"];
    }
}