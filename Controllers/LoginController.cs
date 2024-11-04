using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using NewsApplication.Models;

namespace NewsApplication.Controllers
{
    public class LoginController : Controller
    {
        Executer executer=new Executer();
        DataLayer dataLayer = new DataLayer();
      // GET: Login
      [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login log)
        {
            var data = dataLayer.GetTable($"select * from LoginDetails where password = '{log.password}' and username ='{log.username}'");
           if(data.Rows.Count > 0)
            {
                Session["UserName"] = data.Rows[0][1];
               
                return RedirectToAction("AddNews", "Admin");
            }
            else
            {
               return View(log);
            }
           
             
           
        }
    }
}