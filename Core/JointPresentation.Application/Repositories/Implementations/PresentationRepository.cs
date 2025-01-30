using JointPresentation.Application.DTOs;
using JointPresentation.Application.Enums;
using JointPresentation.Application.Interfaces;
using JointPresentation.Application.Repositories.Interfaces;
using JointPresentation.Domain;
using Microsoft.EntityFrameworkCore;

namespace JointPresentation.Application.Repositories.Implementations
{
    public class PresentationRepository : IPresentationRepository
    {
        private readonly IApplicationDbContext _context;
        private readonly IMembershipRepository _membershipRepository;

        public PresentationRepository(IApplicationDbContext context, IMembershipRepository membershipRepository)
        {
            _context = context;
            _membershipRepository = membershipRepository;
        }

        public async Task<Guid> Create(PresentationDTO dto, CancellationToken cancellationToken)
        {
            Presentation presentation = new Presentation()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                UserName = dto.UserName,
                CreatedAt = Convert.ToInt32(DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds)
            };

            await _context.Presentations.AddAsync(presentation);
            await _context.SaveChangesAsync(cancellationToken);

            Guid userId = await _context.Users.Where(x => x.UserName == dto.UserName).Select(x => x.Id).FirstOrDefaultAsync();
            MembershipDTO membershipDTO = new MembershipDTO()
            {
                PresentationId = presentation.Id,
                UserId = userId,
                Role = (int)Role.Creator
            };

            await _membershipRepository.Create(membershipDTO, cancellationToken);

            return presentation.Id;
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
        {
            Presentation presentation = await _context.Presentations.FirstOrDefaultAsync(x => x.Id == id);

            _context.Presentations.Remove(presentation);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<ICollection<PresentationDTO>> GetAll()
        {
            List<Presentation> presentations = await _context.Presentations.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync();

            List<PresentationDTO> dtos = new List<PresentationDTO>();
            foreach (Presentation presentation in presentations)
            {
                PresentationDTO dto = new PresentationDTO()
                {
                    Id = presentation.Id,
                    Name = presentation.Name,
                    CreatedAt = presentation.CreatedAt,
                    UserName = presentation.UserName
                };
                dtos.Add(dto);
            }
            return dtos;
        }

        public async Task<PresentationDTO> GetById(Guid id)
        {
            Presentation presentation = await _context.Presentations.FirstOrDefaultAsync(x => x.Id == id);

            PresentationDTO dto = new PresentationDTO();
            if (presentation is not null)
            {
                dto.Id = presentation.Id;
                dto.Name = presentation.Name;
                dto.CreatedAt = presentation.CreatedAt;
                dto.UserName = presentation.UserName;
                return dto;
            }
            return dto;
        }

        public async Task<ICollection<PresentationDTO>> GetByUserName(string userName)
        {
            Guid userId = await _context.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefaultAsync();

            List<string> boardlNames = await _context.Memberships
                .Where(x => x.UserId == userId && x.Role == (int)Role.Creator)
                .Select(x => x.Presentation.Name)
                .ToListAsync();

            ICollection<PresentationDTO> dtos = new List<PresentationDTO>();
            foreach (string boardName in boardlNames)
            {
               PresentationDTO dto = new PresentationDTO()
               {
                   Name = boardName
               };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
