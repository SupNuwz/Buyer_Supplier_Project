using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class Delivery
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public string VehicleNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime DeliveryTime { get; set; }
        public int Status { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
    }
}
