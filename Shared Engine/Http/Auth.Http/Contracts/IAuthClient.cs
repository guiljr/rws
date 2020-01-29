using System.Net.Http;
using System.Threading.Tasks;

namespace Engine.Http
{
    public interface IAuthClient
    {
        Task<HttpResponseMessage> GetUsers();

    }
}