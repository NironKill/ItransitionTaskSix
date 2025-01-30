using JointPresentation.Application.DTOs;

namespace JointPresentation.Application.Repositories.Interfaces
{
    public interface IMembershipRepository
    {
        Task<MembershipDTO> GetById(Guid presentationId, Guid userId);

        Task Create(MembershipDTO dto, CancellationToken cancellationToken);
    }
}
