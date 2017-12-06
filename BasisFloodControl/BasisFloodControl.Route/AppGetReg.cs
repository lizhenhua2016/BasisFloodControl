
using ServiceStack;

namespace BasisFloodControl.Route
{
    [Route("/App/AppGetReg","Post", Summary = "获取注册人数")]
    [Api("App")]
    public class RouteAppGetReg
    {
        [ApiMember(IsRequired = true, DataType = "string", Description = "注册手机号")]
        public string Mobile { get; set; }
    }
}