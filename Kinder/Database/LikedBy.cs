using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Database
{
    public class LikedBy
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public int UserID { get; set; }
    }
}
