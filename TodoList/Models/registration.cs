using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoList.BLLayer;

namespace TodoList.Models
{
    public class Registration
    {
        RegistrationBL Obj = new RegistrationBL();
        public string UserRegistration(string userName, string userEmail, string password)
        {
            return Obj.UserRegistration(userName, userEmail, password);
        }
    }
}