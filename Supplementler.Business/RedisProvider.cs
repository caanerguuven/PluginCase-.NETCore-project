using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Supplementler.Business
{
    public class RedisProvider
    {
        static RedisProvider()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { "192.168.99.100:6379" },
                DefaultDatabase = 1,
            };

            RedisProvider.LazyConnection = new Lazy<ConnectionMultiplexer>(() => {
                return ConnectionMultiplexer.Connect(configurationOptions);
            });
           
        }

        private static Lazy<ConnectionMultiplexer> LazyConnection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return LazyConnection.Value;
            }
        }

    }
}
