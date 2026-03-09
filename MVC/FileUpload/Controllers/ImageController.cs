using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FileUpload.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string extension = Path.GetExtension(file.FileName).ToLower();

                if ((extension == ".jpg" || extension == ".png") && file.Length <= 2 * 1024 * 1024)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string filePath = Path.Combine(path, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    ViewBag.Message = "Uploaded";
                }
                else
                {
                    ViewBag.Message = "Invalid File";
                }
            }
            else
            {
                ViewBag.Message = "Invalid File";
            }

            return View("Index");
        }
    }
}