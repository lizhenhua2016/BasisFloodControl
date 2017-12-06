using ServiceStack;

namespace BasisFloodControl
{
    public class UserSession: AuthUserSession
    {
        public string username { get; set; }
        public string mobile { get; set; }
    }
}