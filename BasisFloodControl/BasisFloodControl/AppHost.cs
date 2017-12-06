using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BasisFloodControl.Logic;
using Funq;
using ServiceStack;
using ServiceStack.Mvc;
using BasisFloodControl.ServiceInterface;
using Microsoft.SqlServer.Types;
using ServiceStack.Api.Swagger;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.Logging.Log4Net;
using ServiceStack.OrmLite;


namespace BasisFloodControl
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("BasisFloodControl", typeof(MyServices).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
                HandlerFactoryPath = "api",
            });
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());
            Plugins.Add(new SwaggerFeature());
            Plugins.Add(new AuthFeature(() => new UserSession(),
                new IAuthProvider[] {
                    new CustomCredentialsAuthProvider(), //HTML Form post of UserName/Password credentials
                }));
            Plugins.Add(new RegistrationFeature());
            Plugins.Add(new CorsFeature(allowedMethods: "GET, POST"));
            
            LogManager.LogFactory = new Log4NetFactory(configureLog4Net: true);
            ServiceStack.Text.JsConfig.EmitCamelCaseNames = true;  
            SqlServerDialect.Provider.RegisterConverter<SqlGeometry>(new SqlGeometryConverter());
            var connectionString = ConfigurationManager.ConnectionStrings["GrassrootsFloodCtrl"].ConnectionString;
            var connFactory = new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
            container.Register<IDbConnectionFactory>(c => connFactory);

            container.Register<ICacheClient>(new MemoryCacheClient());
            container.Register<ISessionFactory>(c => new SessionFactory(c.Resolve<ICacheClient>()));
            container.Register<IUserAuthRepository>(new OrmLiteAuthRepository(connFactory)
            {
                UseDistinctRoleTables = true
            });
            //Set MVC to use the same Funq IOC as ServiceStack
            ControllerBuilder.Current.SetControllerFactory(new FunqControllerFactory(container));
            
            container.RegisterAs<AppGetRegImpl, IAppGetReg>();
        }
    }
}