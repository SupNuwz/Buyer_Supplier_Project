using System;
using System.Collections.Generic;
using System.Text;

namespace Mainframe.BuyerSupplier.Core.Dto
{
    public class UserCreationInitialDataDto
    {
        public List<SupplierStandardInventoryDto> StandardInventoryList { get; set; }
        public List<SupplierBaseDto> SupplierBaseList { get; set; }
        public List<DeliverySlotsDto> DeliverySlotList { get; set; }
        public List<ZoneDto> ZoneList { get; set; }

    }
}
