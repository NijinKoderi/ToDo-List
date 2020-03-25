using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoList.Models;

namespace TodoList.BLLayer
{
    public class LoginDetails
    {
        public Int64 EmployeeID { get; set; }
        public string EmployeeName { get; set; }
    }
    public class LoginBL
    {
        ToDoListEntities ObjEntity = new ToDoListEntities();


        public LoginDetails CheckLogin(Int64 UserID, string UserName, string Password)
        {

            string encryptedPassword = SHA.Encode(Password);
            LoginDetails Data = new LoginDetails();
            var Status = (from a in ObjEntity.tblLogins.Where(a => a.UserName == UserName && a.Password == encryptedPassword) select a).ToList();
            if (Status.Count == 1)
            {
                var Result = ObjEntity.spCheckLogin(UserName, SHA.Encode(Password)).SingleOrDefault();
                Data = new LoginDetails { EmployeeID = Result.EmployeeID, EmployeeName = Result.EmployeeName };
            }
            return Data;
        }
    }

}