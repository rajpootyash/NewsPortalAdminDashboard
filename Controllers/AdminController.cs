using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using NewsApplication.Models;
using System.IO;
using Newtonsoft;
using NewsApplication.Filters;
using CKEditor;
using System.Web.UI.WebControls;
using System.Configuration;
namespace NewsApplication.Controllers
{
    [SessionCheck]
    public class AdminController : Controller
    {
        Executer executer = new Executer();
        BussinessLayer bussinessLayer = new BussinessLayer();
        DataLayer dataLayer = new DataLayer();
        JavaScriptSerializer js = new JavaScriptSerializer();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> AddMasterCategories()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> AddViewType()
        {
            return View();
        }

        public async Task<string> AddMasterCategorieSave(News news)
        {
            Query<News> query;
            if (!string.IsNullOrEmpty(news.TabTitle))
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "M_NewsCategoriesSave", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }

            query = await Task.Run(() => executer.select<News>("GetM_NewsCategories")).ConfigureAwait(false);


            var data = js.Serialize(query.resultData);
            return data;
        } 
        public async Task<string> AddMasterViewTitleSave(News news)
        {
            Query<News> query;
            if (!string.IsNullOrEmpty(news.ViewTitle))
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "M_NewsViewTitleSave", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }

            query = await Task.Run(() => executer.select<News>("GetM_ViewType")).ConfigureAwait(false);


            var data = js.Serialize(query.resultData);
            return data;
        }
        [HttpGet]
        public async Task<ActionResult> AddMasterSubCategories()
        {
            Query<News> query;
            News obj = new News();
            obj.ListTabTitle = bussinessLayer.CreatDropDown(dataLayer.GetTable("select TabID,TabTitle  from M_NewsCategories where IsActive =1"));

            query = await Task.Run(() => executer.select<News>("GetSubTabTitle")).ConfigureAwait(false);
            obj.NewsList = query.resultData;
            return View(obj);
        }
        [HttpPost]

        public async Task<ActionResult> AddMasterSubCategories(News news)
        {
            News obj = new News();
            obj.ListTabTitle = bussinessLayer.CreatDropDown(dataLayer.GetTable("select TabID,TabTitle  from M_NewsCategories where IsActive =1"));
            Query<News> query;
            if (!string.IsNullOrEmpty(news.SubTabTitle))
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "M_News_SubCategoriesSave", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }

            query = await Task.Run(() => executer.select<News>("GetSubTabTitle")).ConfigureAwait(false);
            obj.NewsList = query.resultData;
            return View(obj);
        }
        public ActionResult AddNewsDetails()
        {
            return View();
        }
        public async Task<ActionResult> AddPoolsOptions()
        {

            Query<News> query;
            News obj = new News();
            obj.ListTabTitle = bussinessLayer.CreatDropDown(dataLayer.GetTable("select TabID,TabTitle  from M_NewsCategories where IsActive =1"));

            query = await Task.Run(() => executer.select<News>("GetSubTabTitle")).ConfigureAwait(false);
            obj.NewsList = query.resultData;
            return View(obj);
        }
        [HttpGet]
        public async Task<ActionResult> AddPoolsQuestionier()
        {
            Query<News> query;
            News obj = new News();
            obj.ListTabTitle = bussinessLayer.CreatDropDown(dataLayer.GetTable("select TabID,TabTitle  from M_NewsCategories where IsActive =1"));

            query = await Task.Run(() => executer.select<News>("GetSubTabTitle")).ConfigureAwait(false);
            obj.NewsList = query.resultData;
            return View(obj);

        }
        [HttpGet]
        public async Task<ActionResult> AddMasterNewsPrioitise()
        {

            News news = new News();
            Query<News> query = await Task.Run(() => executer.select<News>("GetM_NewsPreorities")).ConfigureAwait(false);
            news.NewsList = query.resultData;
            return View(news);
        }
        public async Task<string> SubTabTitleEditDelete(News news)
        {

            Query<News> query;

            query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "UpdateDeleteSubTabTitle", true)).ConfigureAwait(false);
            if (query.isSuccess)
            {
                query.Commit();
            }
            else
            {
                query.RollBack();
            }


            return "";
        }
        public async Task<string> NewsPrioitiseEditDelet(News news)
        {

            Query<News> query;

            query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "UpdateDeletePrioitise", true)).ConfigureAwait(false);
            if (query.isSuccess)
            {
                query.Commit();
            }
            else
            {
                query.RollBack();
            }


            return "";
        }

        public async Task<string> UpdateDeleteNews_TypeTitle(News news)
        {

            Query<News> query;

            query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "UpdateDeleteNewsTypee", true)).ConfigureAwait(false);
            if (query.isSuccess)
            {
                query.Commit();
            }
            else
            {
                query.RollBack();
            }


            return "";
        }

        [HttpPost]
        public async Task<ActionResult> AddMasterNewsPrioitise(News news)
        {

            Query<News> query;
            if (!string.IsNullOrEmpty(news.NewsPreoritieslevel_Title))
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "M_NewsPreoritieslevelSave", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }

            query = await Task.Run(() => executer.select<News>("GetM_NewsPreorities")).ConfigureAwait(false);
            news.NewsList = query.resultData;
            return View(news);

        }
        [HttpGet]
        public async Task<ActionResult> AddNews()

        {
            News news = new News();
            news.ListCategories = bussinessLayer.CreatDropDown(dataLayer.GetTable("select TabID,TabTitle  from M_NewsCategories where IsActive =1"));

            news.ListNewsPriorities = bussinessLayer.CreatDropDown(dataLayer.GetTable("select *from M_NewsPreoritieslevel where IsActive = 1"));
            news.ListNewsType = bussinessLayer.CreatDropDown(dataLayer.GetTable("select News_TypeID,News_TypeTitle from M_News_Types where IsActive =1"));
            news.ListSubCategories = new List<SelectListItem>() { new SelectListItem() { Text = "--Select--", Value = "0" } };/* bussinessLayer.CreatDropDown(dataLayer.GetTable("select News_TypeID,News_TypeTitle from M_News_Types where IsActive =1"));*/


            return View(news);
        }
        [HttpPost]
        public async Task<ActionResult> AddNews(News news)
        {
            String path=ConfigurationManager.AppSettings.Get("filepath");
            String domain = ConfigurationManager.AppSettings.Get("domain");


            Query<News> query = new Query<News>();

            if (news.AudioFile != null)
            {
                //c://file/News//Audio/abc.png

                string directoryPath = path+"Audio/";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                news.Server_AudioFileName = DateTime.Now.ToString("ddMMyyyy_HH_mm_ss_ms") + news.AudioFile.FileName;
                news.Server_AudioPath = Path.Combine(directoryPath + news.Server_AudioFileName);
               news.AudioFile.SaveAs(news.Server_AudioPath);
                news.Url = domain;
                news.Server_AudioPath = "FileUpload/Audio/" +news.Server_AudioFileName;


            }
            if (news.VideoFile != null)
            {
                string directoryPath = path + "Video/";

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                news.Server_VideoFileName = DateTime.Now.ToString("ddMMyyyy_HH_mm_ss_ms") + news.VideoFile.FileName;
                news.Server_VideoPath = Path.Combine(directoryPath + news.Server_VideoFileName);

                news.CoverFile.SaveAs(news.Server_VideoPath);


                news.Url = domain;
                news.Server_VideoPath = "FileUpload/Video/" + news.Server_VideoFileName;


            }
            if (news.CoverFile != null)
            {
                string directoryPath = path + "Image/";


                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                news.Server_CoverFileName = DateTime.Now.ToString("ddMMyyyy_HH_mm_ss_ms") + news.CoverFile.FileName;
                news.Server_CoverPath= Path.Combine(directoryPath + news.Server_CoverFileName);


                
                news.CoverFile.SaveAs(news.Server_CoverPath);

                news.Url = domain;
                news.Server_CoverPath = "FileUpload/Image/" + news.Server_CoverFileName;

            }
            try
            {

                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "tbl_NewsDataSave", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    

                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }
            catch (Exception)
            {

                query.RollBack();
            }
            ModelState.Clear();
            news.ListCategories = bussinessLayer.CreatDropDown(dataLayer.GetTable("select TabID,TabTitle  from M_NewsCategories where IsActive =1"));

            news.ListNewsPriorities = bussinessLayer.CreatDropDown(dataLayer.GetTable("select *from M_NewsPreoritieslevel where IsActive = 1"));
            news.ListNewsType = bussinessLayer.CreatDropDown(dataLayer.GetTable("select SubTabID,SubTabTitle from M_NewsCategories inner join M_News_SubCategories on M_News_SubCategories.TabID = M_NewsCategories.TabID where M_News_SubCategories.IsActive =1 and M_NewsCategories.IsActive =1  "));
            news.ListSubCategories = bussinessLayer.CreatDropDown(dataLayer.GetTable("select News_TypeID,News_TypeTitle from M_News_Types where IsActive =1"));
            return View(news);
        }

        [HttpGet]
        public async Task<ActionResult> AddMasterNewsTypes()
        {
            News news = new News();
            Query<News> query = await Task.Run(() => executer.select<News>("Get_NewsType")).ConfigureAwait(false);
            news.NewsList = query.resultData;
            return View(news);
        }
        [HttpPost]
        public async Task<ActionResult> AddMasterNewsTypes(News news)
        {


            Query<News> query;
            if (!string.IsNullOrEmpty(news.News_TypeTitle))
            {
                query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "M_News_TypesSave", true)).ConfigureAwait(false);
                if (query.isSuccess)
                {
                    query.Commit();
                }
                else
                {
                    query.RollBack();
                }
            }

            query = await Task.Run(() => executer.select<News>("Get_NewsType")).ConfigureAwait(false);
            news.NewsList = query.resultData;
            return View(news);
        }

        [HttpGet]
        public async Task<ActionResult> AddNewNews()
        {
            News news = new News();
            news.ListCategories = bussinessLayer.CreatDropDown(dataLayer.GetTable("select TabID,TabTitle  from M_NewsCategories where IsActive =1"));

            news.ListNewsPriorities = bussinessLayer.CreatDropDown(dataLayer.GetTable("select *from M_NewsPreoritieslevel where IsActive = 1"));
            news.ListNewsType = bussinessLayer.CreatDropDown(dataLayer.GetTable("select News_TypeID,News_TypeTitle from M_News_Types where IsActive =1"));
            news.ListSubCategories = bussinessLayer.CreatDropDown(dataLayer.GetTable("select News_TypeID,News_TypeTitle from M_News_Types where IsActive =1"));
            return View(news);
        }
        [HttpPost]
        public async Task<ActionResult> AddNewNews(News newa)
        {
            return View();
        }

        public async Task<string> UpdateCategories(News news)
        {

            Query<News> query;

            query = await Task.Run(() => executer.InsertAndGetIdentityAsync<News>(news, "UpdateDeleteCategory", true)).ConfigureAwait(false);
            if (query.isSuccess)
            {
                query.Commit();
            }
            else
            {
                query.RollBack();
            }
            return "";
        }

        public string GetSubCategories(string TabID)
        {
            List<SelectListItem> data = bussinessLayer.CreatDropDown(dataLayer.GetTable($"select SubTabID,SubTabTitle from M_News_SubCategories where TabID = {TabID}"));
            return js.Serialize(data);
        }
    }
}