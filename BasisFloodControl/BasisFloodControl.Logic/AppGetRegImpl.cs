using System;
using System.Collections.Generic;
using BasisFloodControl.Model;
using BasisFloodControl.Route;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace BasisFloodControl.Logic
{
    public class AppGetRegImpl: IAppGetReg
    {
        public IDbConnectionFactory DbFactory { get; set; }
        
        public List<UserMobile> GetMobile(RouteAppGetReg request)
        {
            using (var db= DbFactory.Open())
            {
                var list = db.SqlList<UserMobile>("exec sp_AppGetReg @mobile", new {mobile= request.Mobile});
                return list;
            }
        }
    }
}