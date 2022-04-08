using EmployeeAPI.Models;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{

    public interface IIdentityService
    {
        Task SignUpAsync(UserModel user);

        Task SignInAsync(UserModel user);
    }
    
}
