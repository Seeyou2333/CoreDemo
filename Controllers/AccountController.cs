using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CoreDemo.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;//用来登录的

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email,
                NormalizedUserName = registerViewModel.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerViewModel.Password);
            if (identityResult.Succeeded)
            {
                await _signInManager.SignInAsync(identityUser, new AuthenticationProperties { IsPersistent = true });
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult LoginView()
        {
            return View();
        }
        //登陆
        public IActionResult MakeLogin()
        {
            var claims = new List<Claim>(){
                new Claim(ClaimTypes.Name,"wyt"),
                new Claim(ClaimTypes.Role,"admin")
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(RegisterViewModel loginViewModel)
        {
            //var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email,
            //               loginViewModel.Password, true, lockoutOnFailure: true);

            //if (!result.Succeeded)
            //{
            //    return LocalRedirect("");
            //}
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                //异常先不写，后期统一收集
            }
            //账号密码先不做验证，需要可以自己写
            await _signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = true });

            return RedirectToAction("Index", "Home");
        }

        //登出
        public async Task<IActionResult> Logout()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return Ok();
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangeLogin(string returnUrl)
        {
            return Content("<script>window.location.href='/home/index';loginopen();</script> ", "text/html", Encoding.GetEncoding("GB2312"));
        }


    }
}