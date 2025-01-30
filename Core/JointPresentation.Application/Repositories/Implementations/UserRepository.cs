using JointPresentation.Application.DTOs;
using JointPresentation.Application.Interfaces;
using JointPresentation.Application.Repositories.Interfaces;
using JointPresentation.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JointPresentation.Application.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context) => _context = context;
        
        public async Task<ICollection<UserDTO>> GetAll()
        {
            List<User> users = await _context.Users.ToListAsync();

            List<UserDTO> dtos = new List<UserDTO>();
            foreach (User user in users)
            {
                UserDTO dto = new UserDTO()
                {
                    UserName = user.UserName,
                };
                dtos.Add(dto);
            }
            return dtos;
        }
        public async Task<UserDTO> GetByUserName(string userName)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);

            UserDTO dto = new UserDTO();
            if (user is not null)
            {
                dto.UserName = user.UserName;
                dto.Id = user.Id;

                return dto;
            }
            return dto;
        }
    }
}
