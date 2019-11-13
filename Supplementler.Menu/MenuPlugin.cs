using Supplementler.Business;
using Supplementler.Interface;
using System.Composition;

namespace Supplementler.Menu.Plugin
{

    [Export(typeof(IPlugin))]
    public class MenuPlugin : IPlugin
    {
        private string redisKey = "Supplementler.Menu";
       
        public string ReadFromRedis()
        {
            var cache = RedisProvider.Connection.GetDatabase();
            var resultStr = cache.StringGet(this.redisKey).ToString();

            return string.IsNullOrEmpty(resultStr)?string.Empty:resultStr;
        }
    }
}
