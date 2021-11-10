using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Kinder.Database
{
    public class Items
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(10)]
        public string DateOfPurchase { get; set; }
        public ConditionEnum Condition { get; set; }
        public CathegoryEnum Category { get; set; }
        [Required]
        [MaxLength(20)]
        public string Size { get; set; }
        public int KarmaPrice { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public List<LikedBy> LikedBy { get; set; }
    }
}
