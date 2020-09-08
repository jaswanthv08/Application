using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClaimsManagementSystem.Models
{
    public class UserCredential
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [ForeignKey("Role")]
        [Display(Name = "Role")]
        public int Role_Id { get; set; }
        public Role Role { get; set; }
        public string Status { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Constant { get; set; }
    }
}