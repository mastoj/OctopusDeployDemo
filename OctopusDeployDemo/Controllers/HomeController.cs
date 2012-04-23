using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;

namespace OctopusDeployDemo.Controllers
{
    class SiteInfo
    {
        public string Message { get; set; }
    }

    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            SiteInfo siteInfo = null;
            using (var connection = GetConnection())
            {
                siteInfo = connection.Query<SiteInfo>("select Message from SiteInfo").First();
            }
            ViewBag.Message = siteInfo.Message + " this is awesome! (and local)";

            return View();
        }

        private IDbConnection GetConnection()
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
