using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace WebsiteStockManagement.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        [Required]
        public int ProductPrice { get; set; }
        [Required]
        public DateTime ProductDaterange { get; set; }
        [Required]
        public int UsersId { get; set; }

        public virtual User Users { get; set; }
    }
}
