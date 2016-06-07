/************************************************************************************************************/
/* Class Name     : EmployeesBindModel.cs                                                        */
/* Designed BY    : Arshad Ashraf                                                                           */
/* Created BY     : Arshad Ashraf                                                                           */
/* Creation Date  : 28.04.2016 10:10 AM                                                                     */
/* Description    : Model binding in the ASP.NET MVC framework is simple. Your action                       */
/* methods need  data, and the incoming HTTP request carries the data you need.                             */
/* The catch is that the data is embedded into POST-ed EducationalLanguages values, and possibly the URL    */
/* itself. Enter the DefaultModelBinder, which can magically convert EducationalLanguages values and route  */
/* data into objects. Model binders allow your controller code to remain cleanly separated                  */
/* from the dirtiness of interrogating the request and its associated environment.                          */
/* Plus its gives you the ability to do custom validations on the model                                     */
/************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectName.Data.Models
{
    #region class :: EmployeesBindModel
    /// <summary>
    /// EmployeesBindModel Types Bind Model
    /// Model binding in the ASP.NET MVC framework is simple. Your action
    /// methods need  data, and the incoming HTTP request carries the data you need. 
    ///The catch is that the data is embedded into POST-ed EmployeesBindModel values, and possibly the URL 
    ///itself. Enter the DefaultModelBinder, which can magically convert EmployeesBindModel values and route 
    ///data into objects. Model binders allow your controller code to remain cleanly separated 
    ///from the dirtiness of interrogating the request and its associated environment.            
    ///Plus its gives you the ability to do custom validations on the model  
    /// </summary>
    public class EmployeesBindModel : DefaultModelBinder
    {
        #region enum :: enumEmployeePageMode
        /// <summary>
        /// Page Mode Represent The Current Page Mode 
        /// </summary>
        public enum enumEmployeePageMode
        {
            /// <summary>
            /// Add New Employee Action
            /// </summary>
            Add = 1,
            /// <summary>
            /// Edit Employee Action
            /// </summary>
            Edit = 2
        }
        #endregion

        #region Property :: EmployeePageMode
        /// <summary>
        /// Get Or Set Employee PageMode
        /// </summary>
        public enumEmployeePageMode EmployeePageMode
        {
            get;
            set;
        }
        #endregion

        #region Method :: BindModel

        /// <summary>
        /// BindModel This Function Will Do Custom Binding On Employees Properties 
        /// </summary>
        /// <param name="controllerContext">Encapsulates inEducationalLanguagesation about an HTTP request that matches specified System.Web.Routing.RouteBase</param>
        /// <param name="bindingContext">Provides the context in which a model binder functions.</param>
        /// <returns>Object Of Employees To Bind It</returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            #region Variables Declerations

            HttpRequestBase oCurrentRequest = null;
            string sActionName = string.Empty;
            string sEmployeeName = string.Empty;
            decimal? nEmployeeID;

            Employees oEmployees = null;

            #endregion

            #region Get Current Request Informations

            oCurrentRequest = controllerContext.HttpContext.Request;// Get the current request object 
            sActionName = controllerContext.RouteData.Values["action"].ToString();
            oEmployees = base.BindModel(controllerContext, bindingContext) as Employees;

            #endregion

            #region Specify Employee Page Mode

            if (sActionName.Trim().Equals("JAddEmployee"))
            {
                this.EmployeePageMode = enumEmployeePageMode.Add;
                bindingContext.ModelState.Remove("ID");
            }
            else if (sActionName.Trim().Equals("JEditEmployee"))
            {
                this.EmployeePageMode = enumEmployeePageMode.Edit;
            }

            #endregion

            #region Get Employee Model Properties Values From Current Http Request

            sEmployeeName = oEmployees.EmployeeName;

            #endregion

            #region Bind Employee ID only in Edit Mode

            if (this.EmployeePageMode.Equals(enumEmployeePageMode.Edit))
            {// Start If
                nEmployeeID = (!string.IsNullOrEmpty(oEmployees.EmployeeID.ToString())) ? Convert.ToInt32(oEmployees.EmployeeID) : 0;
                if (nEmployeeID == 0)
                {
                    bindingContext.ModelState.AddModelError("ID", " Is Required");
                }
            }//End Else  

            #endregion

            #region Validate Employee Model Properties Before Do The Binding

            if (sEmployeeName == null)
            {
                bindingContext.ModelState.AddModelError("sEmployeeName", "Employee Name Is Required");
            }

            #endregion

            return oEmployees;
        }
        #endregion
    }
    #endregion
}
