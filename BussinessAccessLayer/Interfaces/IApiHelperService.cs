namespace Management.Services.Interfaces
{
    public interface IApiHelperService
    {
        Task<T?> DeleteAsync<T>(string endpoint, Dictionary<string, string> headers = null, string token = null);

        Task<T?> GetAsync<T>(string endpoint, Dictionary<string, string> headers = null, string token = null);

        Task<T?> PostAsync<T>(string endpoint, object body, Dictionary<string, string> headers = null, string token = null);
    }
}