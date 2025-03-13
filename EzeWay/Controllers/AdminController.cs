
using EzeWay.ef;
using EzeWay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Web;
using System.Reflection.Metadata;

namespace EzeWay.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private dbContext _dbCon;
        private IConfiguration _conf;
        private IWebHostEnvironment _env;

        public AdminController(ILogger<AdminController> logger, IConfiguration conf, IWebHostEnvironment env, dbContext dbCon)
        {

            _logger = logger;
            _dbCon = dbCon;
            _conf = conf;
            _env = env;
        }

        [HttpPost]
        public IActionResult BlogPost(cBlog _blog)
        {

            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            _blog.blogTitleToUrl();
            _blog = new ent_blog(_dbCon).doBlogUpdate(_blog);
            _m_App._cBlog = _blog;
            return RedirectToAction("BlogEdit", new { id = _blog.blog_id });
        }

        public IActionResult BlogList()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);

            return View(_m_App);
        }

        [Route("admin/blog-edit/{id?}")]
        public IActionResult BlogEdit(string id)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            Guid gid = Guid.Empty;
            if (Guid.TryParse(id, out gid))
            { 
                cBlog _blog = new ent_blog(_dbCon).doBlogReadById(gid);
                _m_App._cBlog = _blog;
            }
            /*
            var _cPD = new cPageData();
            _cPD.pd_contents = new ent_pagedata(_dbCon).pdPageContentListAll();
            _m_App._cPageData = _cPD;
            */
            return View(_m_App);
        }

        public IActionResult ProductDataItem(Guid id)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);

            cProductData _prd = new cProductData();
            if (id != Guid.Empty)
            {
                _prd = new ent_productdata(_dbCon).prdProductDataReadById(id)!;
            }
            
            _m_App._prdsProductSources = new ent_productdata(_dbCon).prdsProductSourceList();
            _m_App._prdcProductCats = new ent_productdata(_dbCon).prdcProductCatList();
                
            return View(_m_App);
        }

        [HttpPost]
        public IActionResult ProductDataItemPost(cProductData p)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            
            var _e_prd = new ent_productdata(_dbCon);
            p.prdTitleAndSourceToUrl();
            var _prd = _e_prd.prdProductDataUpdate(p);
            //_m_App._prdProductData = _prd;
            _m_App._prdsProductSources = _e_prd.prdsProductSourceList();
            _m_App._prdcProductCats = _e_prd.prdcProductCatList();
            return RedirectToAction("ProductDataItem", new { id = _prd!.prdId });
        }

        public IActionResult ProductDataList()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);

            return View(_m_App);
        }


        [HttpPost]
        public IActionResult AppContentPost(cPageContent _cpd)
        {
            // Use the model data
            var name = _cpd.pc_name;
            var value = _cpd.pc_value;

            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);

            _m_App._cPageContent = new ent_pagedata(_dbCon).pdPageContentUpdate(_cpd);
            return RedirectToAction("AppContent",  new { id = _cpd.pc_id });
        }

        public IActionResult AppContent(int id)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            /*
            cPageContent pc = new cPageContent();
            if (id != 0)
            {
                pc = new ent_pagedata(_dbCon).pdPageContentReadById(id)!;
            }
            _m_App._cPageContent = pc;
            var _cPD = new cPageData();
            var _e_PD = new ent_pagedata(_dbCon);
            _cPD.pd_contents = _e_PD.pdPageContentListAll();
            _m_App._cPageDatas = _e_PD.pdPageDataList();
            _m_App._cPageData = _cPD;
            */
            return View(_m_App);
        }

        public IActionResult AppContents()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            var _cPD = new cPageData();
            _cPD.pd_contents = new ent_pagedata(_dbCon).pdPageContentListAll();
            _m_App._cPageData = _cPD;
            return View(_m_App);
        }

        public IActionResult Index()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            /*
            var _cPD = new cPageData();
            _cPD.pd_contents = new ent_pagedata(_dbCon).pdPageContentListAll();
            _m_App._cPageData = _cPD;
            */
            return View(_m_App);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext _hc = this.HttpContext;
            string _method = _hc.Request.Method;
            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            /*
            var _cPD = new cPageData();
            _cPD.pd_contents = new ent_pagedata(_dbCon).pdPageContentListAll();
            _m_App._cPageData = _cPD;
            */
            return View(_m_App);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(ent_user? _mUser)
        {
            HttpContext _hc = this.HttpContext;
            string? returnUrl = null;
            
            Exception? exc = null;
            mApp? _m_App = null;
            try
            {
                var pqs = HttpUtility.ParseQueryString(this.Request.QueryString.Value ?? "");
                if (pqs.HasKeys())
                {
                    returnUrl = (pqs["ReturnUrl"]! + "").ToLower()!;
                    if (returnUrl.IndexOf("/admin") > 0)
                    {
                        //returnUrl = Regex.Replace(returnUrl, "/supatee_core", "");
                    }
                }
                
                _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
                /*
                var _cPD = new cPageData();
                _cPD.pd_contents = new ent_pagedata(_dbCon).pdPageContentListAll();
                _m_App._cPageData = _cPD;
                */

                ent_user e_user = new ent_user(_m_App.getDBContext());
                var mUser = e_user.doUserReadByUsernamePwd(_mUser!);
                if (mUser != null)
                {
                    var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, mUser.user_id.ToString()),
                new Claim(ClaimTypes.Name, mUser.user_username!),
            };

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);
                    //var ticket = new AuthenticationTicket(principal, "BasicAuthentication");
                    await this.HttpContext.SignInAsync(principal);

                    this.HttpContext.Response.Redirect(this.HttpContext.Request.PathBase + "/Admin/");
                }
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                exc = ex;
            }

            return View(_m_App);
        }

        public async Task<IActionResult> LogoutAsync(ent_user? _mUser)
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);

            await this.HttpContext.SignOutAsync();

            this.HttpContext.Response.Redirect(this.HttpContext.Request.PathBase + "/Admin/");

            return View("Login", _m_App);
        }


        public IActionResult Logout()
        {
            HttpContext _hc = this.HttpContext;

            mApp _m_App = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            return View(_m_App);
        }
    }
}
