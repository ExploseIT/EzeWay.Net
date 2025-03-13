namespace EzeWay.Models
{
    public class CookieAuthOptions
    {
        public string CookieName { get; set; } = string.Empty;
        public int ExpireTime { get; set; }
        public bool Secure { get; set; }
        public bool HttpOnly { get; set; }
    }
}
