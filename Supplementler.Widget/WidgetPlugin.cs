using Supplementler.Business;
using Supplementler.Interface;
using System.Composition;

namespace Supplementler.Widget.Plugin
{
    [Export(typeof(IPlugin))]
    public class WidgetPlugin : IPlugin
    {
        private readonly string redisKey = "Supplementler.Widget";
       
        public string ReadFromRedis()
        {
            var cache = RedisProvider.Connection.GetDatabase();
            var resultStr = cache.StringGet(this.redisKey).ToString();

            return string.IsNullOrEmpty(resultStr) ? string.Empty : resultStr;
        }
    }
}
