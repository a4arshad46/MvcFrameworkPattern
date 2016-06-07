/**************************************************************************************************************/
/* Class Name     : EmployeesService.cs                                                        */
/* Designed BY    : Arshad Ashraf                                                                              */
/* Created BY     : Arshad Ashraf                                                                              */
/* Creation Date  : 10.04.2016 10:44 AM                                                                        */
/* Description    : EmployeesService implement IEmployeesService && IRepository*/
/*                 To Handle handle CRUD Operations on Employees                                         */
/***************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data.Core;
using ProjectName.Data.Models;

namespace ProjectName.Data.Services
{
    public class EmployeesService : EntityService<Employees>, IEmployeesService
    {
        #region Members
        private readonly MVCDevFrameWorkConnection oMVCDevFrameWorkConnection;
        private IDbSet<Employees> oEmployeesDbSet;// Represent DB Set Table For Employees
        #endregion

        #region Properties

        #region Property :: CurriculumDbSet
        /// <summary>
        ///  Get Training Institute Employees DBSet Object
        /// </summary>
        private IDbSet<Employees> EmployeesDbSet
        {
            get
            {
                if (oEmployeesDbSet == null)
                {
                    oEmployeesDbSet = oMVCDevFrameWorkConnection.Set<Employees>();
                }
                return oEmployeesDbSet;
            }
        }
        #endregion

        #endregion

        #region Constructor :: oEmployeeServiceConnectionIntialization
        /// <summary>
        /// EmployeesService Constructor Initialize MVCDevFrameWorkConnection Entities Connection
        /// </summary>
        /// <param name="oEmployeeServiceConnectionIntialization"></param>
        public EmployeesService(DbContext oEmployeeServiceConnectionIntialization)
            : base(oEmployeeServiceConnectionIntialization)
        {
            oMVCDevFrameWorkConnection = (oMVCDevFrameWorkConnection ?? (MVCDevFrameWorkConnection)oEmployeeServiceConnectionIntialization);
        }
        #endregion

        #region :: Methods ::

        #region Method :: IPagedList<Employees> :: IlGetAllEmployees
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <param name="nPageIndex">Page Index</param>
        /// <param name="nPageSize">Page Size</param>
        /// <param name="sColumnName">Column Name</param>
        /// <param name="sColumnOrder">Column Order</param>
        /// <returns>IPaged List Of Employees</returns>
        public IPagedList<Employees> IlGetAllEmployees(int nPageIndex, int nPageSize, string sColumnName, string sColumnOrder)
        {
            //do a left outer join.
            #region Build Left Join Query And Keep All Query Source As IQueryable To Avoid Any Immediate Execution DataBase
            var lstEmployees = (from e in this.EmployeesDbSet
                                             orderby e.EmployeeName
                                             select new
                                             {
                                                 employeeID = e.EmployeeID,
                                                 employeeName = e.EmployeeName
                                             });
            #endregion
            #region Execute The Query And Return Page Result
            var oTempEmployeesPagedResult = new PagedList<dynamic>(lstEmployees, nPageIndex - 1, nPageSize, sColumnName, sColumnOrder);
            return new PagedList<Employees>(oTempEmployeesPagedResult.Select(oEmplyeePagedResult => new Employees
            {
                EmployeeID = oEmplyeePagedResult.employeeID,
                EmployeeName = oEmplyeePagedResult.employeeName

            }), oTempEmployeesPagedResult.PageIndex, oTempEmployeesPagedResult.PageSize, oTempEmployeesPagedResult.TotalCount);
            #endregion
        }
        #endregion

        #region Method :: dynamic :: olGetAllEmployees
        /// <summary>
        /// Get all employees
        /// </summary>
        /// <param name="nPageIndex">Page Index</param>
        /// <param name="nPageSize">Page Size</param>
        /// <param name="sColumnName">Column Name</param>
        /// <param name="sColumnOrder">Column Order</param>
        /// <returns>dynmic Employees</returns>
        public dynamic olGetAllEmployees(int nPageIndex, int nPageSize, string sColumnName, string sColumnOrder)
        {
            IPagedList<Employees> lstEmployees = this.IlGetAllEmployees(nPageIndex, nPageSize, sColumnName, sColumnOrder);
            var lstEmployeeDataSource = from oEmployeePagedResult in lstEmployees
                                          select new
                                          {
                                              EmployeeID = oEmployeePagedResult.EmployeeID,
                                              EmployeeName = oEmployeePagedResult.EmployeeName,

                                              TotalCount = lstEmployees.TotalCount
                                          };
            return lstEmployees;
        }
        #endregion

        #region Method :: Employees :: oGetEmployeeById
        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="nId">Employee ID</param>
        /// <returns>Employees</returns>
        public Employees oGetEmployeeById(decimal nId)
        {
            Employees oEmployees = null;
            try
            {// Start Try 
                oEmployees = this.oMVCDevFrameWorkConnection.Employees.Where(x => x.EmployeeID == nId).FirstOrDefault();
                var objCurriculumMainSection = new Employees
                {
                    EmployeeID = oEmployees.EmployeeID,
                    EmployeeName = oEmployees.EmployeeName
                };

                return objCurriculumMainSection;
            }//End Try 
            catch (DbEntityValidationException oDbEntityValidationException)
            {//Start Catch
                throw new Exception(strExceptionLogger(oDbEntityValidationException), oDbEntityValidationException);
            }//End Catch         
        }
        #endregion

        #region Method :: void :: vInsertEmployee
        /// <summary>
        /// Insert Employee
        /// </summary>
        /// <param name="oEmployee">Employee Entity</param>
        /// <param name="nDoneBy">Inserted User ID</param>
        public void vInsertEmployee(Employees oEmployee, string nDoneBy)
        {
            #region Check oEmployee Null Value

            if (oEmployee == null)
            {
                throw new ArgumentNullException("oEmployee Entity Is Null");
            }

            #endregion

            #region Try Block

            try
            {// Start Try

                #region Insert CurriculumMainSection

                oEmployee.Created_By = nDoneBy;
                oEmployee.Created_Date = DateTime.Now.Date;
                this.oEmployeesDbSet.Add(oEmployee);

                this.oMVCDevFrameWorkConnection.SaveChanges();

                #endregion

            }//End try 

            #endregion

            #region Catch Block
            catch (DbEntityValidationException oDbEntityValidationException)
            {// Start Catch 
                throw new Exception(strExceptionLogger(oDbEntityValidationException), oDbEntityValidationException);
            }//End Catch 

            #endregion
        }
        #endregion

        #region Method :: void :: vUpdateEmployee
        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="oEmployee">Employee Entity</param>
        /// <param name="nDoneBy">Updated User ID</param>
        public void vUpdateEmployee(Employees oEmployee, string nDoneBy)
        {
            #region Try Block
            try
            {// Start try

                #region Check oEmployee Value

                if (oEmployee == null)
                {
                    throw new ArgumentNullException("oEmployee Entity Is Null");
                }

                #endregion

                #region Update Default oEmployees

                Employees oEmployees = new Employees();
                oEmployees = this.oMVCDevFrameWorkConnection.Employees.Find(oEmployee.EmployeeID);
                oEmployees.EmployeeID = oEmployees.EmployeeID;
                oEmployees.EmployeeName = oEmployees.EmployeeName;
                oEmployees.Modified_By = nDoneBy;
                oEmployees.Modified_Date= DateTime.Now.Date;
                this.oEmployeesDbSet.Attach(oEmployees);
                this.oMVCDevFrameWorkConnection.Entry(oEmployees).State = EntityState.Modified;

                #endregion

            }// End try 
            #endregion

            #region Catch Block
            catch (DbEntityValidationException oDbEntityValidationException)
            {// Start Catch
                throw new Exception(strExceptionLogger(oDbEntityValidationException), oDbEntityValidationException);
            }//End Catch 
            #endregion
        }
        #endregion

        #region Method :: int :: nDeleteEmployee
        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="sIds">Employee ID</param>
        public int nDeleteEmployee(string sIds)
        {
            int operationResult = 0;
            #region Try Block
            try
            {// Start True

                #region Check sIds Value
                if (sIds == null)
                {
                    throw new ArgumentNullException("sIds Is Null");
                }
                #endregion

                #region Multiple Employee Delete
                if (sIds.Split(',').Length > 1)
                {// Start If

                    #region Split Ids
                    char[] cArrdelimiterChars = { ',' }; //Splitting the array of string using a ',' character.
                    string[] sArrwords = sIds.Split(cArrdelimiterChars);
                    List<decimal> lids = new List<decimal>();//Creates a list if decimal to store the IDs from the string array.
                    foreach (string sItem in sArrwords)//Fill the 'ids[]' list using the 'words[]' array of string.
                    {// Start For each
                        lids.Add(decimal.Parse(sItem));//Adds the IDs inside the 'ids' list of decimal.
                    }//End for each 
                    #endregion

                    #region The Deleted Employee Records By Ids
                    IQueryable<Employees> lstEmployeesToBeDeleted = this.EmployeesDbSet.Where(x => lids.Contains(x.EmployeeID)); //Get all objects with same IDs in the List above.
                    #endregion

                    #region  Remove a List of Curriculum Main Section
                    this.oMVCDevFrameWorkConnection.Employees.RemoveRange(lstEmployeesToBeDeleted);
                    foreach (var oEmployeeToBeDeleted in lstEmployeesToBeDeleted.ToList())
                    {// Start foreach

                        if (oEmployeeToBeDeleted != null)
                        {
                            this.oMVCDevFrameWorkConnection.Entry(oEmployeeToBeDeleted).State = EntityState.Deleted;
                            operationResult = 1;
                        }
                        else
                        {
                            operationResult = -1;//Child record exists
                            return operationResult;
                        }


                    }//end foreach
                    #endregion

                }
                #endregion//End if

                #region Single Employee Delete
                else
                {// Start Else

                    #region Fetch Employee Entity
                    decimal dEmployeeID = Convert.ToInt32(sIds);
                    Employees oEmployees = this.oMVCDevFrameWorkConnection.Employees.Where(p => p.EmployeeID == dEmployeeID).FirstOrDefault();// Delete From Default Table
                    #endregion

                    #region Delete Employee Entity

                    if (oEmployees != null)
                    {
                        this.oMVCDevFrameWorkConnection.Employees.Remove(oEmployees);
                        this.oMVCDevFrameWorkConnection.Entry(oEmployees).State = EntityState.Deleted;
                        operationResult = 1;
                    }
                    else
                    {
                        operationResult = -1;//Child record exists
                        return operationResult;
                    }


                    #endregion

                }//end else 
                #endregion
            }//End Try 
            #endregion

            #region Catch Block

            catch (DbEntityValidationException oDbEntityValidationException)
            {// Start Catch
                throw new Exception(strExceptionLogger(oDbEntityValidationException), oDbEntityValidationException);
            }//End Catch
            #endregion
            return operationResult;
        }
        #endregion 

        #endregion

    }
}
