using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EzeWay.ef
{
    public class ent_contact
    {
        private dbContext? dbCon { get; } = null;

        Exception? exc = null;

        public ent_contact(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }

        public cContact? ctactContactUpdate(cContact p)
        {
            cContact? ret = null;


                try
                {
                    SqlParameter[] lParams = {
                new SqlParameter("@ctactId", SqlDbType.Int, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.ctactId)
                , new SqlParameter("@ctactName", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.ctactName)
                , new SqlParameter("@ctactPhone", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.ctactPhone)
                , new SqlParameter("@ctactEmail", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.ctactEmail)
                , new SqlParameter("@ctactSubject", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.ctactSubject)
                , new SqlParameter("@ctactMessage", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.ctactMessage)
                , new SqlParameter("@ctactDTMessage", SqlDbType.DateTime, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.ctactDTMessage)

            };

                    string sp = "exec spContactUpdate @ctactId, @ctactName, @ctactPhone, @ctactEmail, @ctactSubject, @ctactMessage, @ctactDTMessage";

                    var retSP = this.dbCon?.lContact.FromSqlRaw(sp, lParams).AsEnumerable();

                    if (retSP != null)
                    {
                        ret = retSP?.FirstOrDefault<cContact>()!;
                    }
                }
                catch (Exception ex)
                {
                    exc = ex;
                }


            return ret;

        }
    }

    public class cContact
    {
        [Key]
        public int ctactId { get; set; } = 0;
        public string ctactName { get; set; } = "";
        public string ctactEmail { get; set; } = "";
        public string ctactPhone { get; set; } = "";
        public string ctactSubject { get; set; } = "";
        public string ctactMessage { get; set; } = "";
        public DateTime ctactDTMessage { get; set; } = DateTime.Now;
    }

}
