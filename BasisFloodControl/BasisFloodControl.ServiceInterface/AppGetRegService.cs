using System;
using System.Collections.Generic;
using BasisFloodControl.Logic;
using BasisFloodControl.Model;
using BasisFloodControl.Route;
using ServiceStack;

namespace BasisFloodControl.ServiceInterface
{
    public class AppGetRegService: Service
    {
        public IAppGetReg Iag { get; set; }

        public List<UserMobile> Post(RouteAppGetReg request)
        {
            return Iag.GetMobile(request);
        }
    }
}