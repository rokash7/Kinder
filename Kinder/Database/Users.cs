using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kinder.Database
{
    public class Users
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(15)]
        [Column(TypeName = "varchar(10)")]
        public string PhoneNumber { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        public int KarmaPoints { get; set; }
        [Required]
        [MaxLength(10)]
        public string RegDate { get; set; }
        public List<Items> UserItems { get; set; }
    }
}
