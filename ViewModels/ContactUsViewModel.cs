using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.ViewModels
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage ="İsim soyisim girilmelidir.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Başlık girilmelidir.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "E-posta girilmelidir.")]
        [EmailAddress(ErrorMessage ="E-posta düzgün formatta girilmelidir")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mesaj girilmelidir.")]
        public string Message { get; set; }
    }
}
