using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string EmailRegister { get; set; }
        [Compare(nameof(PasswordConfirm),ErrorMessage ="Şifreleriniz uyuşmuyor.")]
        public string PasswordRegister { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
