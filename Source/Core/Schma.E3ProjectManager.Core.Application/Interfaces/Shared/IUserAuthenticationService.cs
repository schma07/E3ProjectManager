using System.Threading.Tasks;
using Schma.E3ProjectManager.Common;
using Schma.E3ProjectManager.Core.Application.DTOs;

namespace Schma.E3ProjectManager.Core.Application
{
    public interface IUserAuthenticationService
    {
        Task<Result<string>> Login(LoginRequestDTO loginRequest);
        Task<Result> Logout();
    }
}
