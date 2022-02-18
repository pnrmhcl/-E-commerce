using System;
using System.Collections.Generic;

#nullable disable

namespace WebUI.Models
{
    public partial class ContactMessage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
