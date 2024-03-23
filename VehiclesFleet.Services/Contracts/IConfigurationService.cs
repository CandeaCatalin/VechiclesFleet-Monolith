namespace VechiclesFleet.Services.Contracts;

public interface IConfigurationService
{
    Task<string> GetConnectionString(string keyName);
    Task<string> GetPassword(string accountName);
}