namespace VehiclesFleet.Services.Contracts;

public interface IAppSettingsReader
{
    string GetValue(string section, string key);
}