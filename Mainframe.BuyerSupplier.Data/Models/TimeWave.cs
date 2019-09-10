using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Data.Models
{
    public class TimeWave
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
