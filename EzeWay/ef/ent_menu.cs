

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using EzeWay.ef;
using EzeWay.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace EzeWay.ef
{
    [Table("tblMenu")]
    public class ent_menu
    {
        private dbContext? dbCon { get; } = null;
        private Guid? userId { get; } = null;
        //private List<ent_menu>? mList { get; set; } = null;
        public ent_menu()
        {
        }

        public ent_menu(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }


        public ent_menu? doMenuReadById(Guid menu_id)
        {
            ent_menu? ret = null;

            SqlParameter[] lParams = {
                new SqlParameter("@menu_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, menu_id)
            };

            var sp = "spMenuReadById @menu_id";

            var retSP = this.dbCon?.lMenu.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null && retSP.Count() > 0)
            {
                ret = retSP?.FirstOrDefault()!;
            }


            return ret;
        }

        public ent_menu? doMenuReadNew()
        {
            ent_menu? ret = null;

            SqlParameter[] lParams = {
                new SqlParameter("@menu_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, menu_id)
            };

            var sp = "spMenuReadById @menu_id";

            var retSP = this.dbCon?.lMenu.FromSqlRaw(sp, lParams).AsEnumerable();

            ret = retSP?.FirstOrDefault();

            return ret;
        }

        public List<ent_menu>? doMenuReadList()
        {
            List<ent_menu>? ret = null;

            SqlParameter[] lParams = { };

            string sp = "spMenuReadList";

            var retSP = this.dbCon?.lMenu.FromSqlRaw(sp, lParams).AsEnumerable();
            ret = retSP?.ToList();

            return ret;
        }

        public ent_menu? doMenuUpdate(ent_menu p)
        {
            ent_menu? ret = null;

            SqlParameter[] lParams = {
                new SqlParameter("@menu_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.menu_id)
                , new SqlParameter("@menu_parent", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.menu_parent)
                , new SqlParameter("@menu_title", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.menu_title)
                , new SqlParameter("@menu_url", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.menu_url)
                , new SqlParameter("@menu_status", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.menu_status)
                , new SqlParameter("@menu_type", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.menu_type)
                , new SqlParameter("@menu_order", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.menu_order)
            };


            string sp = "spMenuUpdateById @menu_id,@menu_parent,@menu_title,@menu_url,@menu_status,@menu_type,@menu_order";

            var retSP = this.dbCon?.lMenu.FromSqlRaw(sp, lParams).AsEnumerable();

            ret = retSP?.FirstOrDefault();

            return ret;
        }

        public void doUpdateUrl()
        {
            if (String.IsNullOrEmpty(menu_url))
            {
                menu_url = doGenerateUrl();
            }
        }

        public string doGenerateUrl()
        {
            string ret = "";
            int tLen = (menu_title + "").Length;
            bool ldflag = true;

            for (int c = 0; c < tLen; c++)
            {
                if (Char.IsLetterOrDigit(menu_title[c]))
                {
                    if (!ldflag)
                    {
                        if (ret != "")
                        {
                            ret += "-";
                        }
                    }
                    ret += menu_title[c];
                    ldflag = true;
                }
                else
                {
                    ldflag = false;
                }
            }
            ret = ret.ToLower();
            /*
            If Not urls Is Nothing Then
                Dim reps As Integer = 0
                urls(count) = ret
                If count > 0 Then
                    For cnt As Integer = 0 To count -1
                        If ret = urls(cnt) Then
                            reps += 1
                        End If
                    Next
                End If
                If reps > 0 Then
                    reps += 1
                    ret += "-" + reps.ToString()
                End If
            End If
            */
            return ret;
        }

        public string doDisplayStatus()
        {
            string? ret = "";
            foreach (var opt in menu_status_options)
            {
                if (opt.spKey.Equals(menu_status))
                {
                    ret = opt.spValue;
                    break;
                }
            }
            return ret;
        }

        public string doDisplayType()
        {
            string? ret = "";
            foreach (var opt in menu_type_options)
            {
                if (opt.spKey.Equals(menu_type))
                {
                    ret = opt.spValue;
                    break;
                }
            }
            return ret;
        }

        public string doDisplayParent()
        {
            string? ret = "";
            List<ent_menu>? mList = doMenuReadList();
            if (menu_parent != null)
            {
                foreach (var m in mList!)
                {
                    if (m.menu_parent.Equals(menu_parent))
                    {
                        ret = m.menu_title;
                        break;
                    }
                }
            }
            return ret;
        }
        public List<sPair> doMenuParentOptions()
        {
            List<sPair> ret = new List<sPair>();
            List<ent_menu>? mList = doMenuReadList();
            if (mList != null && mList.Count > 0)
            {
                foreach (var opt in mList)
                {
                    if (this.menu_id != opt.menu_id)
                    {
                        ret.Add(new sPair(opt.menu_id.ToString()!, opt.menu_title!));
                    }
                }
            }

            return ret;
        }

        public List<sPair> menu_status_options = new List<sPair> {
            new sPair(  "menustatus_draft", "Draft" ),
            new sPair( "menustatus_public", "Public" )
        };

        public List<sPair> menu_type_options = new List<sPair> {
            new sPair("menutype_normal", "Normal")
        };


        [Key]

        public Guid? menu_id { get; set; }
        public Guid? menu_parent { get; set; }
        public string? menu_title { get; set; }
        public string? menu_url { get; set; }
        public string? menu_status  { get; set; }
        public string? menu_type { get; set; }
        public int? menu_order { get; set; }

    }

}
