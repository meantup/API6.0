using ASP.NET_CORE_API.Model;

namespace ASP.NET_CORE_API.Repository
{
    public interface IAccount
    {
        Task<ServiceResponse1<object>> register(AccountModel accmodel);
        Task<ServiceResponse<object>> selectALLregister();
        Task<ServiceResponse<object>> getUserLogin(object cred);
    }
}
