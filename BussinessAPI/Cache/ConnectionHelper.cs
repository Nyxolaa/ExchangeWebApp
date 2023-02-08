using StackExchange.Redis;

namespace BussinessAPI.Cache
{
    public class ConnectionHelper
    {
        static ConnectionHelper()
        {
            ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>
                (() => { return ConnectionMultiplexer.Connect("redis-17141.c232.us-east-1-2.ec2.cloud.redislabs.com:17141,user=default,password=AdLcA4cbQkxcrR1N7net2U5QIvmG6H5d"); });
        }
        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
