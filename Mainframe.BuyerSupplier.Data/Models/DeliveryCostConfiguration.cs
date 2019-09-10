using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class DeliveryCostConfiguration
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BaseFare { get; set; }

        public int BaseDistance { get; set; }

        public int AdditionalRate { get; set; }


        public int BaseLocationID { get; set; }

        public bool IsDeleted { get; set; }


    }
}

