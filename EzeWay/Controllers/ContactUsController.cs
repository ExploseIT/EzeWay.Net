using EzeWay.ef;
using EzeWay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EzeWay.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ILogger<ContactUsController> _logger;
        private dbContext _dbCon;
        private IConfiguration _conf;
        private IWebHostEnvironment _env;


        public ContactUsController(ILogger<ContactUsController> logger, IConfiguration conf, IWebHostEnvironment env, dbContext dbCon)
        {
            
            _logger = logger;
            _dbCon = dbCon;
            _conf = conf;
            _env = env;
        }

        [Route("contact-us")]
        public IActionResult Index()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            var e_Post = new ent_post(_dbCon);
            _m_App.mPage = new ent_page(_dbCon).doPageReadHome();
            _m_App.mPosts = new ent_post(_dbCon).doPostReadListByPageId(_m_App.mPage?.page_Id, "");
            var _cPD = new cPageData();
            _cPD.pd_contents = new ent_pagedata(_dbCon).pdPageContentListByPage("index");
            _m_App._cPageData = _cPD;
            return View(_m_App);
        }

        public IActionResult ContactMessageSent()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            var e_Post = new ent_post(_dbCon);
            _m_App.mPage = new ent_page(_dbCon).doPageReadHome();
            _m_App.mPosts = new ent_post(_dbCon).doPostReadListByPageId(_m_App.mPage?.page_Id, "");
            _m_App.mPost_About = e_Post.doPostReadAbout();
            return View(_m_App);
        }


        [HttpPost]
        public IActionResult ContactPost(cContact p)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            var e_Post = new ent_post(_dbCon);
            var _ctact = new ent_contact(_dbCon).ctactContactUpdate(p);
            return RedirectToAction("ContactMessageSent", new { id = _ctact!.ctactId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
