using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopBerry.Data;
using ShopBerry.Models;
using System.Security.Claims;

namespace ShopBerry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ShopContext _context;

        public AccountController(ShopContext context)
        {
            _context = context;
        }

        // Метод для входа
        [HttpPost("login")] // Уникальный маршрут для логина
        public async Task<IActionResult> Login([FromBody] LoginModel model) // Используем модель
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
                if (user != null) // Проверяем, если пользователь найден
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return Ok(new { message = "Login successful" }); // Возвращаем сообщение об успехе
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return BadRequest(ModelState); // Возвращаем ошибку валидации
        }

        // Метод для регистрации
        [HttpPost("register")] // Уникальный маршрут для регистрации
        public async Task<IActionResult> Register([FromBody] RegisterModel model) // Используем модель
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.UserName == model.UserName);
                if (existingUser == null)
                {
                    var newUser = new User { UserName = model.UserName, Password = model.Password, Role = model.Role };
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Registration successful" }); // Возвращаем сообщение об успехе
                }
                ModelState.AddModelError("", "User already exists.");
            }
            return BadRequest(ModelState); // Возвращаем ошибку валидации
        }

        // Метод для выхода
        [HttpPost("logout")] // Уникальный маршрут для выхода
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logout successful" }); // Возвращаем сообщение об успехе
        }
    }
}


