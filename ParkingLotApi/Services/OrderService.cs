using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotApi.Services
{
    public interface IOrderService
    {
    }

    public class OrderService : IOrderService
    {
        private readonly ParkingLotContext parkingLotContext;

        public OrderService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }
    }
}
