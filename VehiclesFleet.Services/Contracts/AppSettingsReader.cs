using Microsoft.Extensions.Configuration;

namespace VechiclesFleet.Services.Contracts;

public class AppSettingsReader : IAppSettingsReader
{
    private readonly IConfigurationRoot configurationRoot;

    public AppSettingsReader(IConfigurationRoot configurationRoot)
    {
        this.configurationRoot = configurationRoot;
    }

    public string GetValue(string section, string key)
    {
        if (string.IsNullOrWhiteSpace(section))
        {
            throw new ArgumentNullException(nameof(section));
        }

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key));
        }

        return configurationRoot.GetSection(section)[key];
    }
}