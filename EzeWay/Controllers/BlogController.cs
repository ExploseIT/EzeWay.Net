using EzeWay.ef;
using EzeWay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EzeWay.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private dbContext _dbCon;
        private IConfiguration _conf;
        private IWebHostEnvironment _env;


        public BlogController(ILogger<BlogController> logger, IConfiguration conf, IWebHostEnvironment env, dbContext dbCon)
        {
            
            _logger = logger;
            _dbCon = dbCon;
            _conf = conf;
            _env = env;
        }

        [Route("blog/{id}")]
        public IActionResult Index(string id)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            var e_Post = new ent_post(_dbCon);
            _m_App.mPage = new ent_page(_dbCon).doPageReadHome();
            _m_App.mPosts = new ent_post(_dbCon).doPostReadListByPageId(_m_App.mPage?.page_Id, "");
            //_m_App.mPost_About = e_Post.doPostReadAbout();
            
            if (!String.IsNullOrEmpty(id))
            {
                cBlog cBlog = new ent_blog(_dbCon).doBlogReadByUrl(id);
                _m_App._cBlog = cBlog;
            }
            return View(_m_App);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
