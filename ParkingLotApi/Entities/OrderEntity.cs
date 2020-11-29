using ParkingLotApi.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Entities
{
    public class OrderEntity
    {
        public OrderEntity()
        {
        }

        public OrderEntity(OrderDTO orderDto)
        {
            ParkingLotName = orderDto.ParkingLotName;
            PlateNumber = orderDto.PlateNumber;
            CreationTime = DateTime.Now;
            CloseTime = null;
            IsOpen = true;
        }

        [Key] 
        public string OrderNumber { get; set; } = Guid.NewGuid().ToString("N");
        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public bool IsOpen { get; set; }
    }
}
