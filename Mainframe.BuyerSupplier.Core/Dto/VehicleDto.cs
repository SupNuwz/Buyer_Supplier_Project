using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class VehicleDto
    {

        public int ID { get; set; }

        public int SupplierBaseId { get; set; }

        public string DriverContactNo { get; set; }

        public string NumberPlate { get; set; }

        public int VehicleTypeId { get; set; }

        public bool Availability { get; set; }

        public string SupplierBase { get; set; }

        public string VehicleType { get; set; }

        public string ColorCode { get; set; }

        public string MaximumCapacity { get; set; }

    }
}
