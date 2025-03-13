
using System.Text;

namespace EzeWay.ef
{
    public class ent_featuredproduct
    {
        private dbContext? dbCon { get; } = null;

        private List<c_featuredproduct>? _fpList = null;

        public ent_featuredproduct(dbContext dbCon)
        {
            this.dbCon = dbCon;
            _fpList = new List<c_featuredproduct>();
            _fpList.Add(new c_featuredproduct
            {
                fpCategory = "Avon products"
                , fpImageUrl = "~/EzeWay_data/Avon/0019.svg"
                , fpDesc = ["Discover the latest in beauty and skincare."]
                , fpCTA = "Shop Avon"
                , fpId = "avon_cat00_0019"
            });
            _fpList.Add(new c_featuredproduct
            {
                fpCategory = "Avon products"
                , fpImageUrl = "~/EzeWay_data/Avon/0020.svg"
                , fpDesc = ["Discover the latest in beauty and skincare."]
                , fpCTA = "Shop Avon"
                , fpId = "avon_cat00_0020"
            });
            _fpList.Add(new c_featuredproduct
            {
                fpCategory = "Avon products"
                , fpImageUrl = "~/EzeWay_data/Avon/0097.svg"
                , fpDesc = ["Discover the latest in beauty and skincare."]
                , fpCTA = "Shop Avon"
                , fpId = "avon_cat00_0097"
            });
            _fpList.Add(new c_featuredproduct
            {
                fpCategory = "Avon products"
                , fpImageUrl = "~/EzeWay_data/Avon/0098.svg"
                , fpDesc = ["Discover the latest in beauty and skincare."]
                , fpCTA = "Shop Avon"
                , fpId = "avon_cat00_0098"
                , fpEnabled = false
            });
        }

        public List<c_featuredproduct>? fpFeaturedProductList()
        {
            return _fpList;
        }
    }

    public class c_featuredproduct
    {
        public c_featuredproduct() { }

        public string fpDescDisplay()
        {
            string ret = "";
            if (fpDesc != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (string _desc in fpDesc)
                {
                    sb.Append(_desc);
                }
                ret = sb.ToString();
            }
            return ret;

        }

        public string fpCategory { get; set; } = "";
        public string fpImageUrl { get; set; } = "";
        public List<string>? fpDesc { get; set; } = null;
        public string fpCTA { get; set; } = "";
        public bool fpEnabled { get; set; } = true;
        public string fpId { get; set; } = "";
    }
}
