using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TodoList.BLLayer;
namespace TodoList.Models
{
    public class Login
    {
        LoginBL objLogin = new LoginBL();

        public Int64 UserID { get; set; }
        [Required(ErrorMessage = "Please Enter username")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        public LoginDetails CheckLogin()
        {
            return objLogin.CheckLogin(UserID, UserName, Password);
        }
    }


}