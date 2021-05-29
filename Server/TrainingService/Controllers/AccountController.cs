using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TrainingService.Models;
using TrainingService.Models.ResponsesModels;
using TrainingService.ViewModels;
using TrainingService.DBRepository;

namespace TrainingService.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {
            
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                //var user = await _userManager.FindByNameAsync(User.Identity.Name);                
                return new JsonResult(new UserCheckOutResponse
                {
                    IsAuthenticated = true,
                    IsAdmin = User.IsInRole("admin"),
                    UserId = user.Id,
                    UserName = user.Email,                                     
                });
            }
            else
            {
                return new JsonResult(new UserCheckOutResponse
                {
                    IsAuthenticated = false,
                    IsAdmin = false,
                    UserId = "-1",
                    UserName = null,                   
                });
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return Redirect("~/");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public async Task<IActionResult> Register()
        {
            ViewBag.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {                
                User user = new User { Email = model.Email, UserName = model.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return Redirect("~/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult GoogleLogin()
        {
            //Формируем адрес но который Google будет возвращать результат авторизоции
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            //формируем свойства проверки подлинности
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            //Возвращаем htpekmnfn c указанной схемой проверки подлинности и свойствами.
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            //Получаем данные авторизации
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction(nameof(Login));
            //Пытаемся авторизовать пользователя в нашей системе
            //ищем данные о пользователе в таблице AspNetLogins
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            //если всё прошло успешно - перенаправляем пользователя обратно на гавную страницу
            if (result.Succeeded)
                return Redirect("~/");
            else
            {              
                //Если вход не удался т.е. пользователь впервые авторизуется - создаем нового пользователя
                User user = new User
                {
                    Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,                    
                };
                //добавляем пользователя в БД
                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    //добавляем данные в таблицу  AspNetLogins
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        //авторизуем пользователя
                        await _signInManager.SignInAsync(user, false);
                        return Redirect("~/");
                    }
                }
                return AccessDenied();
            }
        }

        public IActionResult VkontakteLogin()
        {
            //Формируем адрес но который VK будет возвращать результат авторизоции
            string redirectUrl = Url.Action("VkontakteResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Vkontakte", redirectUrl);
            //Возвращаем 'окно входа' VK
            return new ChallengeResult("Vkontakte", properties);
        }

        public async Task<IActionResult> VkontakteResponse()
        {
            //Получаем данные авторизации
            ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null) return RedirectToAction(nameof(Login));
            //Пытаемся авторизовать пользователя в нашей системе
            //ProviderKey связывает пользователя Google с таблицей пользователей
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
            //string userName = info.Principal.FindFirst(ClaimTypes.Name).Value;
            string userName = info.Principal.FindFirst(ClaimTypes.GivenName).Value;
            string userSurName = info.Principal.FindFirst(ClaimTypes.Surname).Value;
            //string userEmail = info.Principal.FindFirst(ClaimTypes.Email).Value;
            if (result.Succeeded)
                return Redirect("~/");
            else
            {
                //Если вход не удался - создаем нового пользователя
                User user = new User
                {
                    Email = info.Principal.FindFirst(ClaimTypes.GivenName).Value + info.Principal.FindFirst(ClaimTypes.Surname).Value,
                    UserName = info.Principal.FindFirst(ClaimTypes.GivenName).Value +info.Principal.FindFirst(ClaimTypes.Surname).Value,                   
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return Redirect("~/");
                    }
                }
                return AccessDenied();
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
