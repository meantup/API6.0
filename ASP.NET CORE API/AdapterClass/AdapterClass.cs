using ASP.NET_CORE_API.Class;
using ASP.NET_CORE_API.Repository;
using ASP.NET_CORE_API.Repository.IUnity;

namespace ASP.NET_CORE_API.AdapterClass
{
    public class AdapterClass : IAdapterRepository
    {
        public IAccount account { get; }   
        public AdapterClass(IConfiguration config)
        {
            account = new AccountClass(config);
        }
    }
}
