using JointPresentation.Application.DTOs;

namespace JointPresentation.Application.Repositories.Interfaces
{
    public interface IPresentationRepository
    {
        Task<ICollection<PresentationDTO>> GetAll();
        Task<PresentationDTO> GetById(Guid id);
        Task<ICollection<PresentationDTO>> GetByUserName(string userName);

        Task<Guid> Create(PresentationDTO dto, CancellationToken cancellationToken);
        //Task<bool> Update(PresentationDTO dto, CancellationToken cancellationToken);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken);
    }
}
