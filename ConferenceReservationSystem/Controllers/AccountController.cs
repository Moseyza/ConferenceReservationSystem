using System.Threading.Tasks;
using ConferenceReservationSystem.Infrastructure;
using ConferenceReservationSystem.Interfaces;
using ConferenceReservationSystem.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceReservationSystem.Controllers
{
    public class AccountController : Controller
    {
        #region Variables
        private readonly CustomSignInManager _signInManager;
        private readonly CustomUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISmsSender _smsSender;
        #endregion

        #region Constructor
        public AccountController(CustomSignInManager signInManager, CustomUserManager userManager, IUnitOfWork unitOfWork, ISmsSender smsSender) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _smsSender = smsSender;
        }
        #endregion
         
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            
            //var user = new Person { UserName = "Moseyza", Email = "Moseyza@gmail.com" };
            //var result = await _userManager.CreateAsync(user, "123456");


            //var user = await _userManager.FindByNationalCodeAsync("2003312579");
            //await _signInManager.SignOutAsync();
            
            
            //var result = await _signInManager.PasswordSignInAsync("Moseyza", "123456", false, true);
            //var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            //return RedirectToAction("SendCode");
            
            return View();

            //"617393"


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel) 
        {
            if (ModelState.IsValid) 
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.NationalCode , "123456", false, true);
                if (result.RequiresTwoFactor)
                    return RedirectToAction("VerifyCode" , new {nationalCode = loginModel.NationalCode });

            }
            return View(loginModel);
        }

        [HttpGet]
        public async Task<ActionResult> VerifyCode(string nationalCode)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, TokenOptions.DefaultPhoneProvider);
            if (string.IsNullOrEmpty(code))
            {
                return View("Error");
            }
            await _smsSender.SendSecurityCodeAsync(user.PhoneNumber, code);

            //var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(user);
            //var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            //return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeModel verifyCodeModel)
        {
            if (ModelState.IsValid) 
            {
                var result = await  _signInManager.TwoFactorSignInAsync(TokenOptions.DefaultPhoneProvider, verifyCodeModel.Code, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNationalCodeAsync(verifyCodeModel.NationalCode);
                    var isAdmin =  await _userManager.IsInRoleAsync(user, "Admin");
                    if(isAdmin)
                        return RedirectToAction("ConferenceList" , "Admin");
                    return RedirectToAction("Participate", "Conference");
                }

            }
          
            return View(verifyCodeModel);
        }

        public async Task<IActionResult> Logout() 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index" , "Home");
        }


    }

}
