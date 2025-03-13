
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EzeWay.ef
{
    public class ent_customer
    {
        private dbContext? dbCon { get; } = null;

        public ent_customer(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }

        public cCust custUpdate(cCust p)
        {
            cCust ret = new cCust();
            SqlParameter[] lParams = {
                new SqlParameter("@cust_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_id)
                ,new SqlParameter("@cust_firstname", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_firstname)
                , new SqlParameter("@cust_lastname", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_lastname)
                , new SqlParameter("@cust_email", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_email)
                , new SqlParameter("@cust_mobile", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_mobile)
                , new SqlParameter("@cust_pwd", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_pwd)
                , new SqlParameter("@cust_signup_dt", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_signup_dt)
                
                , new SqlParameter("@cust_browserdetails", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_browserdetails)

                , new SqlParameter("@cust_avon", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, 0)
                , new SqlParameter("@cust_vivamk", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, 0)
                , new SqlParameter("@cust_offers", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, 0)

                //, new SqlParameter("@cust_avon", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_avon)
                //, new SqlParameter("@cust_vivamk", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_vivamk)
                //, new SqlParameter("@cust_offers", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_offers)
            };

            string sp = "Execute spCustomerUpdate @cust_id,@cust_firstname,@cust_lastname,@cust_email,@cust_mobile,@cust_pwd," +
                "@cust_signup_dt,@cust_browserdetails,@cust_avon,@cust_vivamk,@cust_offers";

            var retSP = this.dbCon?.lCustomer.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null)
            {
                ret = retSP?.FirstOrDefault<cCust>()!;
            }
            return ret;
        }

        public cCust custReadById(cCust p)
        {

            cCust ret = new cCust();
            SqlParameter[] lParams = {
                new SqlParameter("@cust_id", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_id)
            };

            string sp = "Execute spCustomerReadById @cust_id";

            var retSP = this.dbCon?.lCustomer.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null)
            {
                ret = retSP?.FirstOrDefault<cCust>()!;
            }
            return ret;
        }

        public cCust custListByEmail(cCust p)
        {

            cCust ret = new cCust();
            SqlParameter[] lParams = {
                new SqlParameter("@cust_email", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_email)
            };

            string sp = "Execute spCustomerListByEmail @cust_email";

            var retSP = this.dbCon?.lCustomer.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null)
            {
                ret = retSP?.FirstOrDefault<cCust>()!;
            }
            return ret;
        }

        public cCust custListByMobile(cCust p)
        {

            cCust ret = new cCust();
            SqlParameter[] lParams = {
                new SqlParameter("@cust_mobile", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_mobile)
            };

            string sp = "Execute spCustomerListByMobile @cust_mobile";

            var retSP = this.dbCon?.lCustomer.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null)
            {
                ret = retSP?.FirstOrDefault<cCust>()!;
            }
            return ret;
        }


        public cCust custListByEmailOrMobile(cCust p)
        {

            cCust ret = new cCust();
            SqlParameter[] lParams = {
                new SqlParameter("@cust_email", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_email)
                , new SqlParameter("@cust_mobile", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.cust_mobile)
            };

            string sp = "Execute spCustomerListByEmailOrMobile @cust_email,@cust_mobile";

            var retSP = this.dbCon?.lCustomer.FromSqlRaw(sp, lParams).AsEnumerable();

            if (retSP != null)
            {
                ret = retSP?.FirstOrDefault<cCust>()!;
            }
            return ret;
        }

        public Guid custNextId()
        {

            Guid ret = Guid.Empty;
            SqlParameter[] lParams = {
            };

            FormattableString sp = $"Execute spCustomerNextId";

            var retSP = this.dbCon?.Database.SqlQuery<Guid>(sp).ToList();  

            if (retSP != null)
            {
                ret = (Guid)(retSP?.FirstOrDefault<Guid>())!;
            }
            return ret;
        }

    }

    public class cCust
    {
        [Key]
        public Guid cust_id { get; set; }
        public string cust_firstname { get; set; } = "";
        public string? cust_lastname { get; set; } = "";
        public string? cust_email { get; set; } = "";
        public string? cust_mobile { get; set; } = "";
        public string? cust_pwd { get; set; } = "";
        public DateTime cust_signup_dt { get; set; } = DateTime.Now;
        public string cust_browserdetails { get; set; } = "";
        public bool cust_avon { get; set; } = false;
        public bool cust_vivamk { get; set; } = false;
        public bool cust_offers { get; set; } = false;
    }

}


