using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class ZoneDto
    {
        public int ID { get; set; }
        public int SupplierBaseID { get; set; }
        public string SupplierBaseName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
