using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private EnstrumanShopContext database;
        public AuthController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Login(string success = "", string exception = "",string registerException="")
        {
            if (!String.IsNullOrEmpty(exception)) ViewBag.Exception = exception;
            if (!String.IsNullOrEmpty(success)) ViewBag.Success = success;
            if (!String.IsNullOrEmpty(registerException)) ViewBag.RegisterException = registerException;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model , string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            User user = database.Users.Include(i=>i.UserRoles).Where(i => i.Email == model.Email && i.Password == CalculateMD5Hash(model.Password)).FirstOrDefault();
            if (user==null)
            {
                ViewBag.Exception = "Kullanıcı adı ya da parolanız yanlış.";
                return View(model);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            foreach (UserRole role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role));
            }
            var UserIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(UserIdentity);
            await HttpContext.SignInAsync(principal);
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Login");
            User user = database.Users.Where(i => i.Email == model.EmailRegister).FirstOrDefault();
            if (user != null)
            {
              return RedirectToAction("Login", new { RegisterException = "Bu e-posta adresi ile kayıtlı bir üye zaten var." });
            }
            user = new User()
            {
                Email = model.EmailRegister,
                FirstName = model.FirstName,
                LastName = model.Surname,
                Password = CalculateMD5Hash(model.PasswordRegister),
                Address = model.Address
            };
            database.Users.Add(user);
            database.SaveChanges();
            UserBasket userBasket = new UserBasket() { TotalPrice = 0, UserId = user.Id };
            database.UserBaskets.Add(userBasket);
            UserRole role = new UserRole() { UserId = user.Id, Role = "user" };
            database.UserRoles.Add(role);
            database.SaveChanges();
            return RedirectToAction("Login", new { Success="Kaydınız başarıyla oluşturuldu. Giriş yapabilirsiniz."});
        }
        
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        public string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString().ToUpper();
        }
    }


}
