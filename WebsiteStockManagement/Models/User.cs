using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebsiteStockManagement.Models
{
    public partial class User
    {
        public User()
        {
            Products = new HashSet<Product>();
        }

        public User(string usersFirstname, string usersSurname, string usersPassword, string usersRole)
        {
            UsersFirstname = usersFirstname;
            UsersSurname = usersSurname;
            UsersPassword = usersPassword;
            UsersRole = usersRole;
        }

        public int UsersId { get; set; }
        public string UsersFirstname { get; set; }
        [Required]
        public string UsersSurname { get; set; }
        [Required]
        public string UsersPassword { get; set; }
        [Required]
        public string UsersRole { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
