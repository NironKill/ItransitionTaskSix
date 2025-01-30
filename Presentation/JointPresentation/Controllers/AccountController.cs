using JointPresentation.Application.DTOs;
using JointPresentation.Application.Repositories.Interfaces;
using JointPresentation.Application.Services.Interfaces;
using JointPresentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace JointPresentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _account;
        private readonly IUserRepository _userRepository;

        public AccountController(IAccountService account, IUserRepository userRepository)
        {
            _account = account;
            _userRepository = userRepository;
        }

        [HttpGet("Account/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Account/Login")]
        public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellationToken)
        {
            LoginDTO dto = new LoginDTO()
            {
                UserName = model.UserName,
            };

            UserDTO userDTO = await _userRepository.GetByUserName(model.UserName);

            if (string.IsNullOrEmpty(userDTO.UserName))
            {
                bool isLoggedIn = await _account.Registration(dto, cancellationToken);

                if (!isLoggedIn)
                    ModelState.AddModelError(string.Empty, "An error occurred while registering the user.");
            }

            if (ModelState.IsValid)
            {
                await _account.Login(dto);         
                
                return RedirectToAction("Manage", "Presentation");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _account.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}
