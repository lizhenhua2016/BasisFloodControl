using System.Collections.Generic;
using BasisFloodControl.Model;
using BasisFloodControl.Route;

namespace BasisFloodControl.Logic
{
    public interface IAppGetReg
    {
        List<UserMobile> GetMobile(RouteAppGetReg request);
    }
}