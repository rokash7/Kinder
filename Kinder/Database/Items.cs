using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Kinder.Database
{
    class Items
    {
        public int ID { get; set; }
        [MaxLength(10)]
        public string DateOfPurchase { get; set; }
        public ConditionEnum Condition { get; set; }
        public CathegoryEnum Category { get; set; }
        public int UserID { get; set; }
        [MaxLength(20)]
        public string Size { get; set; }
        public int KarmaPrice { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
    }
}
