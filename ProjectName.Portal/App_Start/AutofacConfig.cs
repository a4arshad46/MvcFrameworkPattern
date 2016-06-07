/**************************************************************************************************/
/* Class Name    : AutofacConfig.cs                                                               */
/* Designed BY   : Arshad Ashraf                                                                  */
/* Created BY    : Arshad Ashraf                                                                  */
/* Creation Date : 30.04.2016 04:03 PM                                                            */
/* Description   : Here we are registering our classes and its service resolver which allow us to */
/*                 inject the  controller with our service interface                              */
/**************************************************************************************************/


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ProjectName.Data.Models;
using ProjectName.Data.Services;

namespace ProjectName.Portal.App_Start
{
    public class AutofacConfig
    {
        #region Method :: RegisterComponents

        /// <summary>
        /// Register Components 
        /// Will will register all our services to resolve them by Autofac library
        /// </summary>
        public static void RegisterComponents()
        {

            #region Services Container

            var builder = new ContainerBuilder();

            #endregion

            #region Register Controllers

            builder.RegisterControllers(Assembly.GetExecutingAssembly()); // Register Controller Dynamically  

            #endregion

            #region Generics Service Registartions

            #region Connection Registration
            builder.RegisterType(typeof(MVCDevFrameWorkConnection)).As(typeof(DbContext)).InstancePerLifetimeScope();

            #endregion

            #endregion

            #region Custom Service Registration
            #region EmployeeService 
            builder.RegisterType<EmployeesService>().As<IEmployeesService>().InstancePerLifetimeScope();
            #endregion 
            #endregion

            #region Register Service Controllers
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion
        }

        #endregion
    }
}