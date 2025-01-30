using JointPresentation.Application.DTOs;

namespace JointPresentation.Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<UserDTO>> GetAll();
        Task<UserDTO> GetByUserName(string userName);
    }
}
