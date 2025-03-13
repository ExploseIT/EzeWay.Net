using EzeWay.ef;
using Microsoft.AspNetCore.Mvc;

namespace EzeWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private dbContext _dbCon;
        private IConfiguration _conf;
        private IWebHostEnvironment _env;

        public CustomerController(ILogger<CustomerController> logger, IConfiguration conf, IWebHostEnvironment env, dbContext dbCon)
        {
            _logger = logger;
            _dbCon = dbCon;
            _conf = conf;
            _env = env;
        }

        [HttpPost]
        [Route("CustomerSignupAvon")]
        public cCust CustomerSignupAvon(cCust p)
        {
            var _e_cust = new ent_customer(_dbCon);
            var _guid = _e_cust.custNextId();
            p.cust_id = _guid;
            p.cust_avon = true;
            var ret = new ent_customer(_dbCon).custUpdate(p);
            return ret;
        }


        [HttpPost]
        [Route("CustomerExistsEmail")]
        public cCust CustomerExists(cCust p)
        {
            var _e_cust = new ent_customer(_dbCon);
            var ret = new ent_customer(_dbCon).custListByEmail(p);
            return ret;
        }

        [HttpPost]
        [Route("CustomerExistsMobile")]
        public cCust CustomerExistsMobile(cCust p)
        {
            var _e_cust = new ent_customer(_dbCon);
            var ret = new ent_customer(_dbCon).custListByMobile(p);
            return ret;
        }


        [HttpGet]
        [Route("GetCustomers")]
        public IActionResult GetCustomers()
        {
            return Ok();
        }


    }
}
