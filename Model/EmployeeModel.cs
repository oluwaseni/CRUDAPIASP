using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnMyOwn.Model
{
    public class EmployeeModel
    {
        [Key]
        public int CardId { get; set; }
        [Column(TypeName ="nvarchar(100)")]
        [Required]
        public string Username { get; set; }
        [Column(TypeName = "varchar(16)")]
        [Required]
        public string CardNumber { get; set; }
        [Column(TypeName = "varchar(5)")]
        [Required]
        public string ExpirationDate { get; set; }
        [Column(TypeName = "varchar(3)")]
        [Required]
        public string CVV { get; set; }
    }
}
