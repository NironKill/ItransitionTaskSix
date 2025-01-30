using JointPresentation.Application.DTOs;
using JointPresentation.Application.Enums;
using JointPresentation.Application.Interfaces;
using JointPresentation.Application.Repositories.Interfaces;
using JointPresentation.Domain;
using Microsoft.EntityFrameworkCore;

namespace JointPresentation.Application.Repositories.Implementations
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly IApplicationDbContext _context;

        public MembershipRepository(IApplicationDbContext context) => _context = context;

        public async Task Create(MembershipDTO dto, CancellationToken cancellationToken)
        {
            Membership membership = new Membership()
            {
                PresentationId = dto.PresentationId,
                UserId = dto.UserId,
                Role = dto.Role,
            };

            await _context.Memberships.AddAsync(membership);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<MembershipDTO> GetById(Guid presentationId, Guid userId)
        {
            Membership membership = await _context.Memberships.FirstOrDefaultAsync(x => x.PresentationId == presentationId && x.UserId == userId);

            MembershipDTO dto = new MembershipDTO();
            if (membership is not null)
            {
                dto.Id = membership.Id;
                dto.PresentationId = membership.PresentationId;
                dto.UserId = membership.UserId;
                dto.Role = membership.Role;
            }

            return dto;
        }
    }
}
