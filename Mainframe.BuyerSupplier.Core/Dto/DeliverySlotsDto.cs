using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class DeliverySlotsDto
    {
        public int ID { get; set; }

        public string SlotName { get; set; }

        public string FirstWaveTime { get; set; }

        public string SecondWaveTime { get; set; }

        public string StartTime { get; set; }

        public string CutoffTime { get; set; }

        public string CountdownTime { get; set; }

        public string OrderAcceptTime { get; set; }

        public string OrderCofirmTime { get; set; }

        public string DispatchesConfirmTime { get; set; }

        public string EndTime { get; set; }
    }
}
