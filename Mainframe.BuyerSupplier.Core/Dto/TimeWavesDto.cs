using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class TimeWavesDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public TimeSpan Time { get; set; }

        public string Description { get; set; }
    }
}
