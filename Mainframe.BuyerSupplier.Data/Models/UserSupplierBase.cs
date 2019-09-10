using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class UserSupplierBase
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int SupplierBaseID { get; set; }

        public decimal Distance { get; set; }
    }
}