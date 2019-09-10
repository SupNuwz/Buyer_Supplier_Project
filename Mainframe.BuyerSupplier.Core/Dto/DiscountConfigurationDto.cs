using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class DiscountConfigurationDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public decimal Rate { get; set; }
    }
}
