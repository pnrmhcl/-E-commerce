using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class SliderController : Controller
    {
        private EnstrumanShopContext database;

        public SliderController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            List<HomepageSlider> sliders = database.HomepageSliders.ToList();
            return View(sliders);
        }

        public IActionResult Delete(int id)
        {
            HomepageSlider slider = database.HomepageSliders.Where(i => i.Id == id).FirstOrDefault();
            if(slider!=null)
            {
                database.HomepageSliders.Remove(slider);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(HomepageSlider slider,IFormFile file)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "home", fileName));
            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            slider.PicturePath = fileName;
            database.HomepageSliders.Add(slider);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
