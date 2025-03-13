
namespace EzeWay.Models
{
    public class mUtils
    {

        public DateTime GetDateTimeLocal()
        {
            DateTime ret;

            string retStr = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            DateTime.TryParse(retStr, out ret);
            return ret;
        }

        public DateTime GetDateTimeLocal(DateTime dt)
        {
            DateTime ret;

            string retStr = dt.ToString("yyyy-MM-ddTHH:mm:ss");
            DateTime.TryParse(retStr, out ret);
            return ret;
        }

        public string GetDateTimeToString(DateTime dt)
        {
            string retStr = dt.ToString("yyyy-MM-ddTHH:mm:ss");
            return retStr;
        }
    }
}
