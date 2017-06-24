using Akka.Configuration;

namespace ImageSeriesClustering.Core
{
    public class ConfigFactory
    {
        public Config Create(string host, int port) => ConfigurationFactory.ParseString(
            $@"akka {{
                actor {{
                    provider = ""Akka.Remote.RemoteActorRefProvider, Akka.Remote""
                }}

                remote {{
                    helios.tcp {{
                        port = {port}
                        hostname = {host}
                    }}
                }}
            }}"
        );
    }
}
