using JointPresentation.Application.DTOs;

namespace JointPresentation.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Registration(LoginDTO dto, CancellationToken cancellationToken);
        Task Login(LoginDTO dto);
        Task Logout();
    }
}
