
using EzeWay.ef;
using EzeWay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EzeWay.Controllers
{
    public class AvonController : Controller
    {
        private readonly ILogger<AvonController> _logger;
        private dbContext _dbCon;
        private IConfiguration _conf;
        private IWebHostEnvironment _env;


        public AvonController(ILogger<AvonController> logger, IConfiguration conf, IWebHostEnvironment env, dbContext dbCon)
        {
            _logger = logger;
            _dbCon = dbCon;
            _conf = conf;
            _env = env;
        }

        [Route("avon/{id?}")]
        public IActionResult Index(string id)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);

            /*
            var _cPD = new cPageData();
            _cPD.pd_contents = new ent_pagedata(_dbCon).pdPageContentListByPage("index");
            _m_App._cPageData = _cPD;
            */

            return View(_m_App);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
