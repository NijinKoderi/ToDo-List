using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoList.BLLayer
{
    public class RegistrationBL
    {
        ToDoListEntities ObjEntity = new ToDoListEntities();

        public string UserRegistration(string userName, string userEmail, string password)
        {
            var Result = "0";
            try
            {
                if (!ObjEntity.tblLogins.Where(a => a.UserName == userEmail).Any())
                {
                    tblEmployee Insert = new tblEmployee { EmployeeName = userName, CreatedOn = DateTime.Now };
                    ObjEntity.tblEmployees.Add(Insert);
                    ObjEntity.SaveChanges();

                    tblLogin Insert1 = new tblLogin { UserName = userEmail, EmployeeID = Insert.EmployeeID, Password =SHA.Encode(password), IsActive = true }; //Password as Encoded Format
                    ObjEntity.tblLogins.Add(Insert1);
                    ObjEntity.SaveChanges();

                    Result = "1";
                }
                else
                {
                    Result = "-101";
                }

            }
            catch(Exception e)
            {
                string msg = e.Message;
                Result = "0";
            }
            return Result;
        }
    }
}