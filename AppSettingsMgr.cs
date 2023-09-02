using Microsoft.Extensions.Configuration;

namespace SystemToolbox
{
    public static class AppSettingsMgr
    {
        private static IConfiguration config;
        public static AppSettings appSettings;

        static AppSettingsMgr()
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("system-toolbox.json")
                .AddEnvironmentVariables()
                .Build();

            appSettings = config.GetRequiredSection("Settings").Get<AppSettings>();
        }
    }
}