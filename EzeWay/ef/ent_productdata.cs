

using EzeWay.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace EzeWay.ef
{
    public class ent_productdata : cIsDbNull
    {

        private List<cProductData>? _l_ProductsData { get; set; } = null;
        private dbContext? dbCon { get; } = null;
        
        Exception? exc = null;

        public ent_productdata() { }

        public ent_productdata(dbContext dbCon)
        {
            this.dbCon = dbCon;
        }

        public Dictionary<Guid, List<cProductData>> prdProductDataList(bool bIsLive, mApp _mApp)
        {
            Dictionary<Guid, List<cProductData>> ret = new();

            ret = prdProductDataList(bIsLive);

            foreach (var _prdSource in ret)
            {
                foreach (var _prd in _prdSource.Value)
                {
                   //_mApp.mProducts.Add(_prd2);
                }
            }

            return ret;
        }


        public Dictionary<Guid, List<cProductData>> prdProductDataList(bool bIsLive)
        {
            Dictionary<Guid, List<cProductData>> ret = new();

            try
            {
                SqlParameter[] lParams = {
            new SqlParameter("@IsLive", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, bIsLive)
        };

                string sp = "exec spProductDataList @IsLive";

                var retSP = this.dbCon?.lProductData.FromSqlRaw(sp, lParams).AsEnumerable();

                if (retSP != null)
                {
                    // Group the list by prdSource and convert it into a dictionary
                    ret = retSP
                        .GroupBy(product => product.prdSource)
                        .ToDictionary(group => group.Key, group => group.ToList());
                }
            }
            catch (Exception ex)
            {
                exc = ex;
            }

            return ret;
        }


        public cProductData? prdProductDataFromUrlId(string urlId)
        {
            cProductData? ret = null;
            ret = _l_ProductsData?.Where(p => String.Compare(p.prdUrl,urlId) == 0).FirstOrDefault();
            return ret;
        }

        public cProductData? prdProductDataReadById(Guid prdId)
        {
            cProductData? ret = null;


                try
                {
                    SqlParameter[] lParams = {
                    new SqlParameter("@prdId", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, prdId)
                };

                    string sp = "exec spProductDataReadById @prdId";

                    var retSP = this.dbCon?.lProductData.FromSqlRaw(sp, lParams).AsEnumerable();

                    if (retSP != null)
                    {
                        ret = retSP?.FirstOrDefault<cProductData>()!;
                    }
                }
                catch (Exception ex)
                {
                    exc = ex;
                }

            return ret;
        }

        public cProductData? prdProductDataReadByUrl(string prdUrl)
        {
            cProductData? ret = null;

            try
            {
                SqlParameter[] lParams = {
                    new SqlParameter("@prdUrl", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, prdUrl)
                };

                string sp = "exec spProductDataReadByUrl @prdUrl";

                var retSP = this.dbCon?.lProductData.FromSqlRaw(sp, lParams).AsEnumerable();

                if (retSP != null)
                {
                    ret = retSP?.FirstOrDefault<cProductData>()!;
                }
            }
            catch (Exception ex)
            {
                exc = ex;
            }

            return ret;
        }
        public cProductData? prdProductDataUpdate(cProductData p)
        {
            cProductData? ret = null;

            if (p.prdId == Guid.Empty)
            {
                ret = p;
            }
            else
            {
                try
                {
                    SqlParameter[] lParams = {
                new SqlParameter("@prdId", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdId)
                , new SqlParameter("@prdEnabled", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdEnabled)
                , new SqlParameter("@prdFeatured", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdFeatured)
                , new SqlParameter("@prdIsLive", SqlDbType.Bit, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdIsLive)
                , new SqlParameter("@prdTitle", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbEmptyString(p.prdTitle))
                , new SqlParameter("@prdSource", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdSource)
                , new SqlParameter("@prdCategory", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdCategory)
                , new SqlParameter("@prdUrlCompany", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbEmptyString(p.prdUrlCompany))
                , new SqlParameter("@prdUrl", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbEmptyString(p.prdUrl))
                , new SqlParameter("@prdProductId", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbEmptyString(p.prdProductId))
                , new SqlParameter("@prdImageUrl", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbEmptyString(p.prdImageUrl))
                , new SqlParameter("@prdDesc", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbEmptyString(p.prdDesc))
                , new SqlParameter("@prdPriceUK", SqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdPriceUK)
                , new SqlParameter("@prdPriceEuro", SqlDbType.Decimal, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbNull(p.prdPriceEuro))
                , new SqlParameter("@prdSpecialOfferId", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, p.prdSpecialOfferId)
                , new SqlParameter("@prdCTA", SqlDbType.NVarChar, 0, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Current, IsDbEmptyString(p.prdCTA))
            };

                    string sp = "exec spProductDataUpdate @prdId ,@prdEnabled, @prdFeatured, @prdIsLive, @prdTitle, @prdSource,@prdCategory,@prdUrlCompany,@prdUrl,@prdProductId,@prdImageUrl,@prdDesc,@prdPriceUK,@prdPriceEuro,@prdSpecialOfferId,@prdCTA";

                    var retSP = this.dbCon?.lProductData.FromSqlRaw(sp, lParams).AsEnumerable();

                    if (retSP != null)
                    {
                        ret = retSP?.FirstOrDefault<cProductData>()!;
                    }
                }
                catch (Exception ex)
                {
                    exc = ex;
                }
            }

            return ret;

        }

        public List<cProductSource>? prdsProductSourceList()
        {
            List<cProductSource>? ret = null;

            try
            {
                SqlParameter[] lParams = { };

                string sp = "exec spProductSourceList";

                var retSP = this.dbCon?.lProductSource.FromSqlRaw(sp, lParams).AsEnumerable();

                if (retSP != null)
                {
                    ret = retSP?.ToList<cProductSource>()!;
                }
            }
            catch (Exception ex)
            {
                exc = ex;
            }

            return ret;
        }

        public List<cProductCat>? prdcProductCatList()
        {
            List<cProductCat>? ret = null;

            try
            {
                SqlParameter[] lParams = { };

                string sp = "exec spProductCatList";

                var retSP = this.dbCon?.lProductCat.FromSqlRaw(sp, lParams).AsEnumerable();

                if (retSP != null)
                {
                    ret = retSP?.ToList<cProductCat>()!;
                }
            }
            catch (Exception ex)
            {
                exc = ex;
            }

            return ret;
        }

    }

    public class cProductData
    {
        [Key]
        public Guid prdId { get; set; } = Guid.Empty;
        public bool prdEnabled { get; set; } = true;
        public bool prdFeatured { get; set; } = false;
        public bool prdIsLive { get; set; } = false;
        public string prdTitle { get; set; } = "";
        public Guid prdSource { get; set; } = Guid.Empty;
        public Guid prdCategory { get; set; } = Guid.Empty;
        public string prdUrlCompany { get; set; } = "";
        public string prdUrl { get; set; } = "";
        public string prdProductId { get; set; } = "";
        public string prdImageUrl { get; set; } = "";
        public string prdDesc { get; set; } = "";
        public double prdPriceUK { get; set; } = 0.0;
        public double? prdPriceEuro { get; set; } = null;
        public Guid prdSpecialOfferId { get; set; } = Guid.Empty;
        public string prdCTA { get; set; } = "";
        public string prdsName { get; set; } = "";
        public string prdsDesc { get; set; } = "";
        public DateTime prdsDTAdded { get; set; } = DateTime.Now;
        public string prdcName { get; set; } = "";
        public string prdcDesc { get; set; } = "";
        public DateTime prdcDTAdded { get; set; } = DateTime.Now;

        public string prdEnabled_CB()
        {
            string ret = "";

            if (prdEnabled == true)
            {
                ret = "checked";
            }
            return ret;
        }

        public string prdFeatured_CB()
        {
            string ret = "";

            if (prdFeatured == true)
            {
                ret = "checked";
            }
            return ret;
        }

        public string prdIsLive_CB()
        {
            string ret = "";

            if (prdIsLive == true)
            {
                ret = "checked";
            }
            return ret;
        }

        public string prdUrlDisplay()
        {
            string ret = "";

            return ret;
        }

        public string prdTitleAndSourceToUrl()
        {
            string ret = "";
            // Match any character that is not a letter or digit and replace it with an underscore
            Regex reg = new Regex("[^a-zA-Z0-9]", RegexOptions.IgnoreCase);
            string make_under = reg.Replace($"{prdTitle.ToLower()}", "_");

            // Collapse two or more consecutive underscores into a single underscore
            make_under = Regex.Replace(make_under, "_+", "_");

            // Combine prdSource with the processed title
            ret = $"{prdsName.ToLower()}_{make_under}";
            prdUrl = ret;
            return ret;
        }


        public string prdDescDisplay()
        {
            string ret = "";

            ret = "" + prdDesc;

            ret = ret.Replace("\r\n", "<br/>");

            return ret;
        }

        public string prdTitleDisplay()
        {
            string ret = "";

            ret = prdTitle;

            return ret;
        }

        public string prdUrlRelDisplay()
        {
            string ret = "";
            ret = $"~/{prdsName}/{prdUrl}".ToLower();
            return ret;
        }
        public string prdImageUrlDisplay()
        {
            string ret = "";

            ret = $"~/EzeWay_data/{prdImageUrl}";
            return ret;
        }

        public string prdPriceUKDisplay()
        {
            string ret = String.Empty;
            ret = prdPriceUK.ToString("C", Cultures.UnitedKingdom);
            return ret;
        }

        public string prdPriceEuroDisplay()
        {
            string ret = String.Empty;
            if (prdPriceEuro != null)
            {
                ret = prdPriceEuro?.ToString("\u20AC0.00")!;
            }
            return ret;
        }

    }

    public class cProductSource
    {
        [Key]
        public Guid prdsId { get; set; } = Guid.Empty;
        public string prdsName { get; set; } = "";
        public DateTime prdsDTAdded { get; set; } = DateTime.MinValue;
    }

    public class cProductCat
    {
        [Key]
        public Guid prdcId { get; set; } = Guid.Empty;
        public string prdcName { get; set; } = "";
        public string prdcDesc { get; set; } = "";
        public DateTime prdcDTAdded { get; set; } = DateTime.MinValue;
    }

    public enum PrdTypes
    {
        Prd_Default = 0,
        Prd_Featured
    }

}

