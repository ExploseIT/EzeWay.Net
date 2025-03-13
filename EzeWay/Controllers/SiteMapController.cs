using EzeWay.ef;
using EzeWay.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace EzeWay.Controllers
{
    public class SiteMapController : Controller
    {
        private readonly ILogger<SiteMapController> _logger;
        private dbContext _dbCon;
        private IConfiguration _conf;
        private IWebHostEnvironment _env;


        public SiteMapController(ILogger<SiteMapController> logger, IConfiguration conf, IWebHostEnvironment env, dbContext dbCon)
        {

            _logger = logger;
            _dbCon = dbCon;
            _conf = conf;
            _env = env;
        }

        public IActionResult Index()
        {
            HttpContext _hc = this.HttpContext;
            mApp _mApp = new mApp(_hc, this.Request, this.RouteData, _dbCon, _conf, _env);
            var _blogs = _mApp._cBlogs;
            string _mUrl = _mApp.mUrl;
            var _prdll = _mApp._cProductDatas;

            List<SitemapUrl> blog_sul = new List<SitemapUrl>();
            foreach (var _blog in _blogs!)
            {
                blog_sul.Add(new SitemapUrl { Loc = $"{_mUrl}/blog/{_blog.blog_url}", Priority = 0.8, Lastmod = DateOnly.FromDateTime(_blog.blog_dt_display) });
            }

            
            // Define the sitemap content
            var sitemapData = new Dictionary<string, List<SitemapUrl>>
            {
                ["Blogs"] = blog_sul,

                ["Other Pages"] = new List<SitemapUrl>
            {
                new SitemapUrl { Loc = $"{_mUrl}/privacy", Priority = 0.5 },
                new SitemapUrl { Loc = $"{_mUrl}/contact-us", Priority = 0.5 }
            }
            };

            
            foreach (List<cProductData> _prdl in _prdll!.Values!)
            {
                var prd_sm = new List<SitemapUrl>();

                string prdSource = $"{_prdl!.First().prdsName} Products";

                foreach (cProductData _prd in _prdl)
                {
                    prd_sm.Add(new SitemapUrl { Loc = $"{_mUrl}/product/{_prd.prdUrl}", Priority = 0.8 });
                }
                sitemapData.Add(prdSource, prd_sm);
            }


            // Generate XML
            var xml = GenerateSitemapXml(sitemapData);

            // Return XML content
            return Content(xml, "application/xml; charset=utf-8");
        }

        private string GenerateSitemapXml(Dictionary<string, List<SitemapUrl>> sitemapData)
        {
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false,
                Encoding = System.Text.Encoding.UTF8
            };

            using (var stringWriter = new Utf8StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xmlWriter.WriteStartDocument();

                    // Write the root element with the namespace
                    xmlWriter.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                    foreach (var section in sitemapData)
                    {
                        // Write section comment
                        xmlWriter.WriteComment(section.Key);

                        // Write URLs in the section
                        foreach (var url in section.Value)
                        {
                            xmlWriter.WriteStartElement("url");

                            xmlWriter.WriteElementString("loc", url.Loc);
                            xmlWriter.WriteElementString("priority", url.Priority.ToString("F1"));
                            xmlWriter.WriteElementString("lastmod", url.Lastmod.ToString("yyyy-MM-dd"));
                            xmlWriter.WriteEndElement(); // </url>
                        }
                    }

                    xmlWriter.WriteEndElement(); // </urlset>
                    xmlWriter.WriteEndDocument();
                }

                return stringWriter.ToString();
            }
        }

        // SitemapUrl Model
        public class SitemapUrl
        {
            public string Loc { get; set; }
            public double Priority { get; set; }
            public DateOnly Lastmod { get; set; } = DateOnly.Parse("2025-02-01");
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
