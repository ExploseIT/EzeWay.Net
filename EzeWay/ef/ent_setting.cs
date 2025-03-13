
using EzeWay.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using EzeWay.ef;

namespace EzeWay.ef
{


    public class ent_setting
    {
        private dbContext? dbCon { get; } = null;


        public ent_setting(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }

        public List<cSetting> getList()
        {
            var ls = this.dbCon?.lSetting;
            List<cSetting> ret = ls!.ToList<cSetting>();

            return ret;
        }

        public string doHide()
        {
            string ret = "";
            ret = "style='display:none'";
            return ret;
        }


        public string getVersion()
        {
            return getVersion(false);
        }

        public string getVersion(bool v)
        {
            string ret = "";
            string format = "{0}";
            if (v)
            {
                format = "v{0}";
            }
            ret = String.Format(format, typeof(cSetting).Assembly.GetName().Version);

            return ret;
        }



        public bool isVersionDifferent()
        {
            bool ret = false;
            string sAppVersion = getVersion();
            string sDBVersion = getValue("Version");
            string sMatch = @"(?<sig>[\d]+\.[\d]+\.[\d]+)\.[\d]+";

            Match match1 = new Regex(sMatch).Match(sAppVersion);
            Match match2 = new Regex(sMatch).Match(sDBVersion);
            if (match1.Success && match2.Success)
            {
                if (String.Compare(match1.Groups["sig"].Value, match2.Groups["sig"].Value, true) != 0)
                {
                    ret = true;
                }

            }

            return ret;
        }

        public string getValue(string name)
        {
            return "value";
        }


        public cSetting doSettingsReadByName(string setName)
        {
            cSetting ret = new cSetting();
            SqlParameter[] lParams = { new SqlParameter("@setName", setName) };
            string cmd = String.Format("Execute spSettingsList '{0}'", setName);
            var mQuery = this.dbCon.lSetting.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<cSetting>();
            if (mEnum != null)
            {
                ret = mEnum.FirstOrDefault<cSetting>()!;
            }
            return ret;
        }

        public List<cSetting> doSettingsList(string setName)
        {
            List<cSetting> ret = null;
            SqlParameter[] lParams = { new SqlParameter("@setName", setName) };
            string cmd = String.Format("Execute spSettingsList '{0}'", setName);
            var mQuery = this.dbCon.lSetting.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<cSetting>();

            ret = mEnum.ToList<cSetting>();

            return ret;
        }

        public List<cSetting> doSettingsListAll()
        {
            List<cSetting> ret = null;
            string cmd = String.Format("Execute spSettingsListAll");
            var mQuery = this.dbCon.lSetting.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<cSetting>();

            ret = mEnum.ToList<cSetting>();

            return ret;
        }

        public List<cSetting> doSettingsUpdateValue(string setName, string setValue)
        {
            List<cSetting> ret = new List<cSetting>();
            string cmd = String.Format("Execute spSettingsUpdateValue '{0}','{1}'", setName, setValue);
            var mQuery = this.dbCon.lSetting.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<cSetting>();

            ret = mEnum.ToList<cSetting>();
            return ret;
        }

        public bool getMustHaveEmail()
        {
            bool ret = false;

            bool.TryParse(getValue("MustHaveEmail"), out ret);

            return ret;
        }

        public bool getMustHaveStaffNo()
        {
            bool ret = false;

            bool.TryParse(getValue("MustHaveStaffNo"), out ret);

            return ret;
        }

        public bool IsUploadLastIfNoChanges()
        {
            bool ret = false;
            string retStr = getValue("UploadLastIfNoChanges");
            bool.TryParse(retStr, out ret);
            return ret;

        }

    }

    [Table("tblSettings")]
    public class cSetting
    {
        [Key]
        public string setName { get; set; } = String.Empty;
        public string setValue { get; set; } = String.Empty;
        public string setDescription { get; set; } = String.Empty;
    }
}