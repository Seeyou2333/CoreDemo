using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager) {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginView() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string userName, string email,string password) {

            var result = await _userManager.CreateAsync(new IdentityUser { UserName = userName,Email= email }, password);
            if (result.Succeeded) {
                return Json("创建成功");
            }

            return BadRequest(result.Errors);
        }
        public IActionResult RegisterView()
        {
            return View();
        }
        
    }
}