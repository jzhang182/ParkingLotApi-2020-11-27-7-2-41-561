using ParkingLotApi.DTOs;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public interface IOrderService
    {
        public Task<string> AddAsync(OrderDTO orderDto);
        public Task<OrderEntity> GetAsync(string orderNumber);
        public Task<OrderEntity> UpdateAsync(string orderNumber, OrderUpdateModel orderUpdateModel);
    }

    public class OrderService : IOrderService
    {
        private readonly ParkingLotContext parkingLotContext;

        public OrderService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<string> AddAsync(OrderDTO orderDto)
        {
            var order = new OrderEntity(orderDto);

            await parkingLotContext.Orders.AddAsync(order);
            await parkingLotContext.SaveChangesAsync();
            return order.OrderNumber;
        }

        public async Task<OrderEntity> GetAsync(string orderNumber)
        {
            var order = this.parkingLotContext.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (order == null)
            {
                return null;
            }

            return order;
        }

        public async Task<OrderEntity> UpdateAsync(string orderNumber, OrderUpdateModel orderUpdateModel)
        {
            var order = this.parkingLotContext.Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (order != null && order.IsOpen == true)
            {
                order.IsOpen = orderUpdateModel.IsOpen;
                order.CloseTime = DateTime.Now;
            }

            await this.parkingLotContext.SaveChangesAsync();

            return order;
        }
    }
}
