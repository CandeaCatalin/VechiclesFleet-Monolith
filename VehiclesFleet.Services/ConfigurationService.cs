// using VehiclesFleet.Services.Contracts;
//
// namespace VehiclesFleet.Services;
//
// public class ConfigurationService : IConfigurationService
// {
//     private readonly IAppSettingsReader appSettingsReader;
//     private readonly ILoggerService loggerService;
//
//     public ConfigurationService(IAppSettingsReader appSettingsReader,
//         ILoggerService loggerService)
//     {
//         this.appSettingsReader = appSettingsReader;
//         this.loggerService = loggerService;
//     }
//
//     public async Task<string> GetConnectionString(string keyName)
//     {
//         if (string.IsNullOrWhiteSpace(keyName))
//         {
//             throw new ArgumentNullException(nameof(keyName));
//         }
//
//         var content = await apiService.GetAsync(GetConnectionStringFormattedUrl(keyName));
//         var contentAsObject = JObject.Parse(content);
//         var errorMessage = contentAsObject["error"]?.Value<string>();
//
//         if (!string.IsNullOrWhiteSpace(errorMessage))
//         {
//             await loggerService.LogError(errorMessage);
//             throw new InvalidOperationException(errorMessage);
//         }
//
//         var json = JObject.Parse(content)["domainEntity"];
//
//         return await DecryptConnectionString(json.ToObject<ConfigurationEntity>());
//     }
//
//     public async Task<string> GetPassword(string accountName)
//     {
//         if (string.IsNullOrWhiteSpace(accountName))
//         {
//             throw new ArgumentNullException(nameof(accountName));
//         }
//
//         var content = await apiService.GetAsync(GetPasswordUrl(accountName));
//         var contentAsObject = JObject.Parse(content);
//         var errorMessage = contentAsObject["error"]?.Value<string>();
//
//         if (!string.IsNullOrWhiteSpace(errorMessage))
//         {
//             await loggerService.LogError(errorMessage);
//             throw new InvalidOperationException(errorMessage);
//         }
//
//         return contentAsObject["domainEntity"]["value"].ToString();
//     }
//
//     private async Task<string> DecryptConnectionString(ConfigurationEntity setting)
//     {
//         var connectionStringContainerBuilder = new SqlConnectionStringBuilder(setting.Value);
//
//         if (string.IsNullOrWhiteSpace(connectionStringContainerBuilder.Password))
//         {
//             return setting.Value;
//         }
//
//         connectionStringContainerBuilder.Password = await GetPassword(connectionStringContainerBuilder.UserID);
//
//         return connectionStringContainerBuilder.ConnectionString;
//     }
//
//     private string GetConnectionStringFormattedUrl(string keyName)
//     {
//         return
//             $"{BaseUrl}/v2/settings?query={{\"applicationName\":\"{ApplicationName}\",\"environment\":\"{Environment}\",\"type\":2,\"keyName\":\"{keyName}\"}}";
//     }
//
//     private string GetPasswordUrl(string accountName)
//     {
//         return $"{BaseUrl}/v1/tpamDecrypted?accountName={accountName}";
//     }
//
//     private string BaseUrl => appSettingsReader.GetValue(ConfigurationKeys.ConfigurationMicroserviceSectionName,
//         ConfigurationKeys.BaseUrlKey);
//
//     private string Environment => appSettingsReader.GetValue(ConfigurationKeys.RunningConfigurationSectionName,
//         ConfigurationKeys.EnvironmentKey);
//
//     private string ApplicationName => appSettingsReader.GetValue(ConfigurationKeys.RunningConfigurationSectionName,
//         ConfigurationKeys.ApplicationNameKey);
// }