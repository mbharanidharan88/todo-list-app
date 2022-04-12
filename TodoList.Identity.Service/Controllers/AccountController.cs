using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoList.Domain;
using TodoList.Identity.Service.Models;

namespace TodoList.Identity.Service.Controllers
{
    //[SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {

        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        #endregion

        #region Constructor

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
        }

        #endregion

        #region Public Methods

        #region Login

     

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberLogin, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (context != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        throw new Exception("invalid return URL");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid credentials");
            }

            return View(model);
        }

        #endregion

        #region Logout

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();
            }

            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            return Redirect(logout.PostLogoutRedirectUri);
        }

        #endregion

        #region Register

        public IActionResult Register(string returnUrl)
        {
            var model = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Username,
                        EmailConfirmed = true,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    //result = await _userManager.AddClaimsAsync(user, new Claim[]{
                    //            new Claim(JwtClaimTypes.Email, model.Username)
                    //        });

                    //if (!result.Succeeded)
                    //{
                    //    throw new Exception(result.Errors.First().Description);
                    //}

                    return RedirectToAction("Login", new { returnUrl = model.ReturnUrl });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            

            return View(model);
        }

        #endregion

        #endregion
    }
}
