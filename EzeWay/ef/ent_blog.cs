

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EzeWay.ef;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System;
using EzeWay.Models;

namespace EzeWay.ef
{
    public class ent_blog
    {
        private dbContext? dbCon { get; } = null;
        private Guid? userId { get; } = null;
        public ent_blog()
        {
        }

        public ent_blog(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }


        public cBlog doBlogReadById(Guid id)
        {

            cBlog ret = new cBlog();

            SqlParameter[] lParams = {
                new SqlParameter("@blog_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, id)

            };

            var sp = "spBlogReadById @blog_id";

            var retSP = this.dbCon?.lBlog.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null && retSP.Count() > 0)
            {
                ret = retSP?.FirstOrDefault()!;
            }

            return ret;
        }

        public cBlog doBlogReadByUrl(string url)
        {

            cBlog ret = new cBlog();

            SqlParameter[] lParams = {
                new SqlParameter("@blog_url", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, url)

            };

            var sp = "spBlogReadByUrl @blog_url";

            var retSP = this.dbCon?.lBlog.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null && retSP.Count() > 0)
            {
                ret = retSP?.FirstOrDefault()!;
            }

            return ret;
        }

        public List<cBlog> doBlogList(bool is_local, bool is_all)
        {
            List<cBlog> ret = new List<cBlog>();

            SqlParameter[] lParams = {
                new SqlParameter("@is_local", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, is_local)
                , new SqlParameter("@is_all", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, is_all)
            };

            var sp = "spBlogList @is_local, @is_all";

            var retSP = this.dbCon?.lBlog.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null && retSP.Count() > 0)
            {
                ret = retSP?.ToList()!;
            }

            return ret;
        }

        public List<cBlogStatus> doBlogStatusList()
        {
            List<cBlogStatus> ret = new List<cBlogStatus>();

            SqlParameter[] lParams = { };

            var sp = "spBlogStatusList";

            var retSP = this.dbCon?.lBlogStatus.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null && retSP.Count() > 0)
            {
                ret = retSP?.ToList()!;
            }

            return ret;
        }

        public cBlog doBlogUpdate(cBlog p)
        {

            cBlog ret = new cBlog();

            SqlParameter[] lParams = {
                new SqlParameter("@blog_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_id)
                , new SqlParameter("@blog_author", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_author)
                , new SqlParameter("@blog_dt_created", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_dt_created)
                , new SqlParameter("@blog_dt_modified", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_dt_modified)
                , new SqlParameter("@blog_dt_display", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_dt_display)
                , new SqlParameter("@blog_content", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_content)
                , new SqlParameter("@blog_title", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_title)
                , new SqlParameter("@blog_url", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_url)
                , new SqlParameter("@blog_status", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blog_status)
                , new SqlParameter("@blgo_index", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.blgo_index)
            };

            var sp = "spBlogUpdate @blog_id, @blog_author, @blog_dt_created,@blog_dt_modified,@blog_dt_display,@blog_content,@blog_title,@blog_url,@blog_status,@blgo_index";


            var retSP = this.dbCon?.lBlog.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null && retSP.Count() > 0)
            {
                ret = retSP?.FirstOrDefault()!;
            }

            return ret;
        }

    }


    public class cBlog
    {
        [Key]
        public Guid blog_id { get; set; } = Guid.Empty;
        public Guid blog_author { get; set; } = Guid.Empty;
        public DateTime blog_dt_created { get; set; } = DateTime.Now;
        public DateTime blog_dt_modified { get; set; } = DateTime.Now;
        public DateTime blog_dt_display { get; set; } = DateTime.Now;
        public string blog_content { get; set; } = "";
        public string blog_title { get; set; } = "";
        public string blog_url { get; set; } = "";
        public string blog_status { get; set; } = "blgs_draft";
        public string blgs_name { get; set; } = "";
        public int blgo_index { get; set; } = 999999;

        public string blog_content_SanitizedShort(int len)
        {
            string ret = "";
            string content = "";
            Regex reg = new Regex(@">(?<cap>[\\w\\s\\W][^><]+)<", RegexOptions.IgnoreCase);
            MatchCollection matches = reg.Matches(this.blog_content);
            if (matches.Count > 0)
            {
                content = matches[0].Groups["cap"].Value;

                if (content.Length > len)
                {
                    ret = $"{content.Substring(0, len)}...";
                }
                else
                {
                    ret = content;
                }
            }
            return ret;
        }

        public string blog_title_Short(int len)
        {
            string ret = "";
            string title = "";

            title = this.blog_title;

            if (title.Length > len)
            {
                ret = $"{title.Substring(0, len)}...";
            }
            else
            {
                ret = title;
            }

            return ret;
        }


        public void blogLocaliseUrls(string url, string vdir)
        {
            this.blog_content = Regex.Replace(this.blog_content, Regex.Escape(url)+"/"+vdir, "[url]/"+vdir, RegexOptions.IgnoreCase);
            this.blog_content = Regex.Replace(this.blog_content, Regex.Escape(url), "[url]", RegexOptions.IgnoreCase);
        }

        public string blogTitleToUrl()
        {
            string ret = "";
            // Match any character that is not a letter or digit and replace it with an underscore
            Regex reg = new Regex("[^a-zA-Z0-9]", RegexOptions.IgnoreCase);
            string make_under = reg.Replace($"{blog_title.ToLower()}", "_");

            // Collapse two or more consecutive underscores into a single underscore
            make_under = Regex.Replace(make_under, "_+", "_");

            ret = $"{make_under}";

            if (("" + this.blog_url).Length > 2)
            {
                ret = this.blog_url;
            }
            else
            {
                this.blog_url = ret;
            }
            return ret;
        }
    }

    public class cBlogStatus
    {
        [Key]
        public string blgs_id { get; set; } = "";
        public string blgs_name { get; set; } = "";
        public int blgs_order { get; set; } = 0;
    }



    public static class ImageHelper
    {
        public static string ProcessImageUrls(mApp _mApp, string content)
        {
            if (string.IsNullOrEmpty(content)) return content;

            // Regex pattern to match <img> tags
            string imgPattern = @"<img\s+[^>]*src=['""]([^'""]+)['""]";

            return Regex.Replace(content, imgPattern, match =>
            {
                string imgTag = match.Value; // Full <img> tag
                string imgUrl = match.Groups[1].Value; // Extracted URL

                // Check if URL is absolute (starts with http:// or https://)
                if (imgUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                {
                    return imgTag; // Leave absolute URLs unchanged
                }
                else
                {
                    // Convert relative URLs using @Url.Content()
                    //string newUrl = $"{_mApp.mUrl}{imgUrl}";
                    string newUrl = ("" + imgUrl).Replace("[url]",_mApp.mUrl);
                    return imgTag.Replace(imgUrl, newUrl);
                }
            }, RegexOptions.IgnoreCase);
        }
    }

}
