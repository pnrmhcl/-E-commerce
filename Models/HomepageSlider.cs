using System;
using System.Collections.Generic;

#nullable disable

namespace WebUI.Models
{
    public partial class HomepageSlider
    {
        public int Id { get; set; }
        public string PicturePath { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public string Text { get; set; }
    }
}
