using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDWithAjaxJQuery
{
    public class CustomerWiseBonusRateVM
    {
        public int ID { get; set; }
        public long CustomerID { get; set; }
        public string Name { get; set; }
        public decimal? BonusRate { get; set; }
        public DateTime? BonusDate { get; set; }
    }
}