using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteStockManagement.OOP
{
    public class Users :IComparable
    {
        //Creating 5 variables 
        private int userId;
        private string usernameFn = "";
        private string usernameSn = "";
        private string userRole = "";
        private string password = "";
       

        public Users()
        {

        }

        //Creating a construtor with all the class varibles 
        public Users(int userId, string usernameFn, string usernameSn, string userRole, string password)
        {
            this.UserId = userId;
            this.UsernameFn = usernameFn;
            this.UsernameSn = usernameSn;
            this.UserRole = userRole;
            this.Password = password;
        }

        //Creating the getters and setters for the local class varibles
        public int UserId { get => userId; set => userId = value; }
        public string UsernameFn { get => usernameFn; set => usernameFn = value; }
        public string UsernameSn { get => usernameSn; set => usernameSn = value; }
        public string UserRole { get => userRole; set => userRole = value; }
        public string Password { get => password; set => password = value; }

        //Compares this instance with a specified String object and indicates whether this instance precedes, 
        //follows, or appears in the same position in the sort order as the specified string.
        public int CompareTo(object obj)
        {
            return usernameFn.CompareTo(obj.ToString());
        }

        public override string ToString()
        {
            return usernameFn;
        }
    }
}
