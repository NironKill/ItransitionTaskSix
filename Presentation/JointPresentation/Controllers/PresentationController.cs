using JointPresentation.Application.DTOs;
using JointPresentation.Application.Enums;
using JointPresentation.Application.Repositories.Interfaces;
using JointPresentation.Application.Services.Interfaces;
using JointPresentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JointPresentation.Controllers
{
    [Route("[controller]")]
    public class PresentationController : Controller
    {
        private readonly IUserRepository _user;
        private readonly IPresentationRepository _presentation;
        private readonly IMembershipRepository _membership;
        private readonly IAccessTokenService _accessToken;

        public PresentationController(IUserRepository userRepository, IPresentationRepository presentation,
            IMembershipRepository membership, IAccessTokenService accessToken)
        {
            _user = userRepository;
            _presentation = presentation;
            _membership = membership;
            _accessToken = accessToken;
        }

        [HttpGet("Board/{id}")]
        [Authorize]
        public async Task<IActionResult> Board(Guid id, CancellationToken cancellationToken)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            UserDTO user = await _user.GetByUserName(userIdClaim.Subject.Name);

            MembershipDTO membership = await _membership.GetById(id, user.Id);

            MembershipDTO newMember = new MembershipDTO()
            {
                PresentationId = id,
                UserId = user.Id,
                Role = (int)Role.Member
            };
            if (membership is null)
                await _membership.Create(newMember, cancellationToken);

            MembershipDTO membershipDTO = await _membership.GetById(id, user.Id);
            MembershipModel model = new MembershipModel()
            {
                Id = membershipDTO.Id,
                PresentationId = membershipDTO.PresentationId,
                UserName = user.UserName,
                Role = membershipDTO.Role,
            };

            return View(model);
        }

        [HttpGet("Manage")]
        [Authorize]
        public async Task<IActionResult> Manage()
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            bool isInvalid = await _accessToken.ValidateToken(userIdClaim.Subject.Name);
            if (!isInvalid)
                return RedirectToAction("Login", "Account");

            ICollection<PresentationDTO> dtos = await _presentation.GetAll();

            List<PresentationModel> models = new List<PresentationModel>();
            foreach (PresentationDTO dto in dtos)
            {
                PresentationModel model = new PresentationModel()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    CreatedAt = dto.CreatedAt,
                    UserName = dto.UserName
                };
                models.Add(model);
            }
            return View(models);
        }

        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> Create(string name, CancellationToken cancellationToken)
        {
            Claim userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            ICollection<PresentationDTO> boardNames = await _presentation.GetByUserName(userIdClaim.Subject.Name);

            foreach (PresentationDTO boardName in boardNames)
            {
                if (name == boardName.Name)
                    return BadRequest("You have already created a presentation with this title");
            } 

            PresentationDTO dto = new PresentationDTO()
            {
                Name = name,
                UserName = userIdClaim.Subject.Name
            };

            Guid id = await _presentation.Create(dto, cancellationToken);

            return RedirectToAction("Manage", "Presentation");
        }

        [HttpPost("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _presentation.Delete(id, cancellationToken);

            return RedirectToAction("Manage", "Presentation");
        }
    }
}
