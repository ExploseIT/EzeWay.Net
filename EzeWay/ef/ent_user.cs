using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;
using EzeWay.ef;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using EzeWay.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace EzeWay.ef
{
    [Table("tblUser")]
    public class ent_user
    {
        private dbContext? dbCon { get; } = null;

        public ent_user(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }


        public ent_user()
        {

        }

        public ent_user? doUserReadByUsernameId(ent_user mUser)
        {
            ent_user? ret = null;
         
            string cmd = String.Format("Execute spUserReadByUsernameId '{0}','{1}'", mUser.user_id, mUser.user_username);
            var mQuery = this.dbCon!.lUser.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<ent_user>();

            ret = mEnum.FirstOrDefault<ent_user>();
            return ret;

        }
        public ent_user? doUserReadByUsernamePwd(ent_user mUser)
        {
            ent_user? ret = null;
            string baSaltStr = new ent_setting(dbCon!).doSettingsReadByName("PasswordSalt")!.setValue!;
            byte[] baSalt = System.Text.Encoding.Unicode.GetBytes(baSaltStr);
            string sPwdHash = getPasswordHash(mUser.user_pwd!, baSalt);

            string cmd = String.Format("Execute spUserReadByUsernamePwd '{0}','{1}'", mUser.user_username, sPwdHash);
            var mQuery = this.dbCon!.lUser.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<ent_user>();

            ret = mEnum.FirstOrDefault<ent_user>();
            return ret;

        }

        public ent_user? doUserUpdatePwdById(Guid userId, string pwd)
        {
            ent_user? ret = null;
            string baSaltStr = new ent_setting(dbCon!).doSettingsReadByName("PasswordSalt")!.setValue!;
            byte[] baSalt = System.Text.Encoding.Unicode.GetBytes(baSaltStr);
            string userPwd = getPasswordHash(pwd, baSalt);

            string cmd = String.Format("Execute spUserUpdatePwdById '{0}','{1}'", userId, userPwd);
            var mQuery = this.dbCon.lUser.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<ent_user>();

            ret = mEnum.FirstOrDefault<ent_user>();
            return ret;

        }

        public ent_user doUserReadById(string userId)
        {
            ent_user? ret = null;

            string cmd = String.Format("Execute spUserReadByid '{0}'", userId);
            var mQuery = this.dbCon?.lUser.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<ent_user>();

            ret = mEnum.FirstOrDefault<ent_user>();
            return ret;
        }

        public ent_user? doUserReadIsRegTest()
        {
            ent_user? ret = null;
            string cmd = String.Format("Execute spUserReadIsRegTest");
            var mQuery = this.dbCon?.lUser.FromSqlRaw(cmd);
            var mEnum = mQuery.AsEnumerable<ent_user>();

            ret = mEnum.FirstOrDefault<ent_user>();
            return ret;

        }

        public List<ent_title> doUserTitleList()
        {
            List<ent_title> ret = new List<ent_title>();

            SqlParameter[] lParams = { };

            string sp = "spUserTitleList ";

            var retSP = this.dbCon?.lTitle.FromSqlRaw(sp, lParams).AsEnumerable();

            ret = retSP?.ToList()!;
            return ret;
        }

        private string getPasswordHash(string pwd, byte[] baSalt)
        {
            string ret = "";

            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.


            //var ps = m_App._m_settings_all.FirstOrDefault<ent_setting>(s => s.setName.ToLower().Equals("passwordsalt"));
            //if (ps.setValue != null && ps.setValue.Length < 3)
            //{
            //salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            //var _m_settings = new ent_setting(m_App.getContextDB());
            //string strSalt = Convert.ToBase64String(salt);
            //byte[] bSalt = Convert.FromBase64String(strSalt);
            //_m_settings.doSettingsUpdateValue("PasswordSalt", strSalt);
            //}
            //else
            //{
            //salt = Convert.FromBase64String(ps.setValue);
            //}

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            ret = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pwd!,
                salt: baSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            //Guid userId = Guid.Parse("2379a598-b843-48b2-bccd-4b166502ec63");
            //ent_user _m_ent_user = new ent_user(m_App.getContextDB(), m_App);
            //_m_ent_user.doUserUpdatePwdById(userId, pwd);
            return ret;
        }



        [Key]
        public Guid user_id { get; set; }
        public string? user_username { get; set; }
        public string? user_firstname { get; set; }
        public string? user_lastname { get; set; }
        public string? user_title { get; set; }
        public string? user_pwd { get; set; }
        public string? user_email { get; set; }
        public string? user_phone { get; set; }
        public bool user_isreg { get; set; }
        public DateTime? user_dtreg { get; set; }
        public DateTime? user_dtdob { get; set; }
        public bool? user_regtest { get; set; }
        public bool? user_rememberme { get; set; }

    }

    public class ent_user_success
    {
        public bool success { get; set; } = false;
        public Exception exception { get; set; } = null;
        public string message { get; set; } = "";
        public ent_user? user { get; set; }
        public List<ent_user> userlist { get; set; } = null;
    }

    public class ent_userandroles_success
    {
        public string getFullName()
        {
            string ret = "";
            if (user != null)
            {
                ret = String.Format("{0} {1}", user.user_firstname, user.user_lastname);
            }
            return ret;
        }
        public bool success { get; set; } = false;
        public Exception? exception { get; set; } = null;
        public ent_user? user { get; set; } = null;
        public List<ent_userrole>? rolelist { get; set; } = null;
    }

    [Table("tblTitle")]
    public class ent_title
    {
        [Key]
        public string? tit_name { get; set; } = null;
        public string? tit_value { get; set; } = null;
        public int? tit_index { get; set; } = null;
    }

}