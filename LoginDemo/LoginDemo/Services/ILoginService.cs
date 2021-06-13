using System.Threading.Tasks;

namespace LoginDemo.Services
{
    public interface ILoginService
    {
        Task<bool> CheckLogin(string userName, string password);
    }
}