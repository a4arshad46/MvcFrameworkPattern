/*****************************************************************************************************************/
/* Class Name     : IEmployeesService.cs                                                                         */
/* Designed BY    : Arshad Ashraf                                                                                */
/* Created BY     : Arshad Ashraf                                                                                */
/* Creation Date  : 10.04.2016 10:23 AM                                                                          */
/* Description    : IEmployeesService customized service implement CRUD Operations Signatures                    */
/*****************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Core;
using ProjectName.Data.Models;

namespace ProjectName.Data.Services
{
    #region :: interface :: IEmployeesService
    public interface IEmployeesService
    {
        #region Method :: Ilist Get All Employees
        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <param name="nPageIndex"></param>
        /// <param name="nPageSize"></param>
        /// <param name="sColumnName"></param>
        /// <param name="sColumnOrder"></param>
        /// <returns>Paged List Of Employees</returns>
        IPagedList<Employees> IlGetAllEmployees(int nPageIndex, int nPageSize, string sColumnName, string sColumnOrder);
        #endregion

        #region Method :: GetEmployeeById
        /// <summary>
        /// Get Employee By Id
        /// </summary>
        /// <param name="nId">Employee Id Primary Key</param>
        /// <returns>Employees object</returns>
        Employees oGetEmployeeById(decimal nId);
        #endregion

        #region Method :: Insert Employee
        /// <summary>
        /// Insert Employee in database 
        /// </summary>
        /// <param name="oEmployee">represent oEmployee entity on our ORM data model</param>
        void vInsertEmployee(Employees oEmployee, string nDoneBy);
        #endregion

        #region Method :: Update Employee
        /// <summary>
        /// Update Employee in database 
        /// </summary>
        /// <param name="oEmployee">represent oEmployee entity on our ORM data model</param>
        void vUpdateEmployee(Employees oEmployee, string nDoneBy);
        #endregion

        #region Method :: Delete Employee
        /// <summary>
        /// Delete Employee entity from database
        /// </summary>
        /// <param name="sIds">Employee Id Primary Key</param>
        /// <returns>0 or 1 </returns>
        int nDeleteEmployee(string sIds);
        #endregion
    } 
    #endregion
}
