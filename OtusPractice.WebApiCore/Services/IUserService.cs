using OtusPractice.Domain.Models;
using System.Threading.Tasks;

namespace OtusPractice.WebApiCore.Services
{
    public interface IUserService
    {
        Task<User> GetById(string id);
        Task<User> GetByLoginAsync(string userLogin);
        Task<string> Register(User user);
    }
}