using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using TreeViewDemo.Models;

namespace TreeViewDemo.Controllers
{
    public class HomeController : Controller
    {
        UsersContext db;
        public HomeController()
        {
            db = new UsersContext();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            return View();
        }

        public ActionResult Tree()
        {

            return View();
        }

        public ActionResult Image()
        {

            return View();
        }

        public ActionResult Image1()
        {
            return View();
        }

        public ActionResult ImageCrop()
        {
            //List<string> files = new List<string>();
            //string path =@""+ ConfigurationSettings.AppSettings["WebPath"] + "/croppedimages/";
            
            //files = Directory.GetFiles(path, "*.*").ToList();

            List<UserProfile> profiles = db.UserProfiles.ToList();
            

            return View(profiles);
        }

        public ActionResult DisplayImageFromDB(int id)
        {
            UserProfile profile = db.UserProfiles.FirstOrDefault(x => x.UserId == id);
            return File(profile.Image, "image/png");
        }

        [HttpPost]
        public ActionResult ImageCrop(List<string> hdnCroppedImageOutput, string imageSaveOption)
        {
            
            if (hdnCroppedImageOutput != null && hdnCroppedImageOutput.Any())
            {
                if(!string.IsNullOrEmpty(imageSaveOption))
                {
                    switch (imageSaveOption)
                    {
                        case "file":
                            foreach (string imageBase64 in hdnCroppedImageOutput)
                            {
                                Image image = Base64ToImage(imageBase64);
                                string path = Server.MapPath(@"~/croppedimages/");
                                if (!Directory.Exists(path))
                                {
                                    Directory.CreateDirectory(path);
                                }
                                string fileName = DateTime.Now.Ticks.ToString() + ".png";
                                image.Save(path + fileName, System.Drawing.Imaging.ImageFormat.Png);
                            }
                            break;

                        case "db":
                            foreach (string imageBase64 in hdnCroppedImageOutput)
                            {
                                string b64 = imageBase64.Replace("data:image/png;base64,", "");
                                byte[] imageBytes = Convert.FromBase64String(b64);

                                UserProfile profile = new UserProfile();
                                profile.UserName = "amish";
                                profile.Image = imageBytes;
                                db.UserProfiles.Add(profile);
                                db.SaveChanges();
                            }
                            break;
                    }
                }
            }

            return View();
            
        }

        public Image Base64ToImage(string imageBase64)
        {
            imageBase64 = imageBase64.Replace("data:image/png;base64,", "");

            byte[] imageBytes = Convert.FromBase64String(imageBase64);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }


        public ActionResult Map()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
