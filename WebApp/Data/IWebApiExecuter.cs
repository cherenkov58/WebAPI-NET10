
namespace WebApp.Data
{
    public interface IWebApiExecuter
    {
		Task InvokeDelete(string v);
		Task<T?> InvokeGet<T>(string relativeUrl);
        Task<T?> InvokePost<T>(string relativeUrl, T obj);
		Task InvokePut<T>(string relativeUrl, T obj);
	}
}