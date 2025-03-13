

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EzeWay.ef;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static EzeWay.ef.dbContext;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace EzeWay.ef
{
    [Table("tblPost")]
    public class ent_post
    {
        private dbContext? dbCon { get; } = null;

        public ent_post()
        {

        }

        public ent_post(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }

        public ent_post(IFormCollection form)
        {
            //this.post_id = form["post_id"].;
        }

        public ent_post? doPostUpdate(ent_post p)
        {
            ent_post? ret = null;

            SqlParameter[] lParams = {
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_id)
                , new SqlParameter("@post_page_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_page_id)
                , new SqlParameter("@post_author", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_author)
                , new SqlParameter("@post_date", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_date)
                , new SqlParameter("@post_content", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_content)
                , new SqlParameter("@post_title", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_title)
                , new SqlParameter("@post_excerpt", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_excerpt)
                , new SqlParameter("@post_status", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_status)
                , new SqlParameter("@post_comment_status", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_comment_status)
                , new SqlParameter("@post_name", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_name)
                , new SqlParameter("@post_type", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.post_type)
            };

            string sp = "spPostUpdateById @post_id,@post_page_id,@post_author,@post_date,@post_content,@post_title,@post_excerpt,@post_status,@post_comment_status,@post_name,@post_type";

            var retSP = this.dbCon?.lPost.FromSqlRaw(sp, lParams).AsEnumerable<ent_post>();

            ret = retSP?.FirstOrDefault();

            return ret;
        }


        public ent_post? doPostReadById(Guid post_id)
        {
            ent_post? ret = null;

            SqlParameter[] lParams = {
                new SqlParameter("@post_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, post_id)
            };

            string sp = "spPostReadById @post_id";
            var retSP = this.dbCon?.lPost.FromSqlRaw(sp, lParams).AsEnumerable<ent_post>();

            ret = retSP?.FirstOrDefault();

            return ret;
        }

        public ent_post? doPostReadAbout()
        {
            ent_post? ret = null;


            SqlParameter[] lParams = {
                new SqlParameter("@post_type", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, "posttype_about")
            };

            string sp = "spPostReadByType @post_type";

            var retSP = this.dbCon?.lPost.FromSqlRaw(sp, lParams).AsEnumerable<ent_post>();

            ret = retSP?.FirstOrDefault();

            return ret;
        }

        public List<ent_post>? doPostReadListByPageId(Guid? pageId, string post_type)
        {
            List<ent_post>? ret = null;

            SqlParameter[] lParams = {
                new SqlParameter("@post_page_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, pageId)
                , new SqlParameter("@post_type", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, post_type)
            };

            string sp = "spPostReadListByPageId @post_page_id,@post_type";

            var retSP = this.dbCon?.lPost.FromSqlRaw(sp, lParams).AsEnumerable<ent_post>();

            ret = retSP?.ToList();

            return ret;
        }

        public ent_post? doPostReadInitial()
        {
            ent_post? ret = null;
            string cmd = String.Format("Execute spPostReadInitial");
            var mQuery = this.dbCon?.lPost.FromSqlRaw(cmd);
            var mEnum = mQuery?.AsEnumerable<ent_post>();
            ret = mEnum?.FirstOrDefault();

            return ret;
        }



        public ent_post? doPostReadNew()
        {
            ent_post? ret = null;
            Guid postId = Guid.Empty;
            string cmd = String.Format("Execute spPostReadById '{0}'", postId);
            var mQuery = this.dbCon?.lPost.FromSqlRaw(cmd);
            var mEnum = mQuery?.AsEnumerable<ent_post>();

            ret = mEnum?.FirstOrDefault<ent_post>();

            return ret;
        }



        public string doDisplayContent()
        {
            string ret = "";
            string crlf = Environment.NewLine;
            string con = post_content!;
            con = (con + "").Replace(crlf, "<br />");
            ret = con;
            return ret;
        }

        public string? doDisplayDatePostFormat()
        {
            string? ret = "";
            if (post_date != null)
            {
                ret = post_date?.ToString("dd/MM/yyyy hh:mm");
            }
            return ret;
        }

        public string? getPostDateAsString()
        {
            string? ret = "";
            if (post_date != null)
            {
                ret = post_date?.ToString("dd/MM/yyyy hh:mm");
            }
            return ret;
        }

        public string getPostContentSome(int length)
        {
            string ret = "";
            int len = (post_content + "").Length;
            if (len > 0)
            {
                int slen = len < length ? len : length;
                ret = (post_content + "").Substring(0, length);
            }
            return ret;
        }
        public string getPostStatus()
        {
            string? ret = "";
            foreach(var opt in post_status_options)
            {
                if (opt[0].Equals(post_status))
                {
                    ret = opt[1];
                    break;
                }
            }
            return ret;
        }

        public string getPostType()
        {
            string? ret = "";
            foreach (var opt in post_type_options)
            {
                if (opt[0].Equals(post_type))
                {
                    ret = opt[1];
                    break;
                }
            }
            return ret;
        }

        public string[][] post_status_options = new String[][] { new String [] {  "poststatus_draft", "Draft" },
            new String [] { "poststatus_public", "Public" }
        };
        public string[][] post_type_options = new String[][] { new String [] {  "posttype_post", "Post" }
            ,new String [] { "posttype_initial", "Initial" }
            ,new String [] { "posttype_about", "About" }
        };

        [Key]
        public Guid post_id { get; set; }
		public Guid? post_author { get; set; }
        public DateTime? post_date { get; set; }
        public string? post_content { get; set; }
        public string? post_title { get; set; }
        public string? post_excerpt { get; set; }
        public string? post_status { get; set; }
        public string? post_comment_status { get; set; }
        public string? post_name { get; set; }
        public string? post_type { get; set; }
        public Guid? post_page_id { get; set; }
        public string? post_page_title { get; set; }
    }
}
