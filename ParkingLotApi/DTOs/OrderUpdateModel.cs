using ParkingLotApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.DTOs
{
    public class OrderUpdateModel
    {
        private OrderEntity order;

        public OrderUpdateModel()
        {
        }

        public OrderUpdateModel(OrderEntity order)
        {
            this.order = order;
        }

        public bool IsOpen { get; set; }
    }
}
