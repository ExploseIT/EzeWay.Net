
using EzeWay.ef;
using EzeWay.Models;
using Microsoft.AspNetCore.Mvc;

namespace EzeWay.Controllers
{
    public class AvonBookRequestController : Controller
    {
        private readonly ILogger<AvonBookRequestController> _logger;
        private dbContext _dbCon;
        private IConfiguration _conf;
        private IWebHostEnvironment _env;


        public AvonBookRequestController(ILogger<AvonBookRequestController> logger, IConfiguration conf, IWebHostEnvironment env, dbContext dbCon)
        {
            _logger = logger;
            _dbCon = dbCon;
            _conf = conf;
            _env = env;
        }


        public IActionResult Index()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            return View(_m_App);
        }
    }
}
