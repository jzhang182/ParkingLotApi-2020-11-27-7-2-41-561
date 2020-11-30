using ParkingLotApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotApi.DTOs
{
    public class OrderDTO
    {
        public OrderDTO()
        {
        }

        public OrderDTO(OrderEntity orderEntity)
        {
            ParkingLotName = orderEntity.ParkingLotName;
            PlateNumber = orderEntity.PlateNumber;
        }

        public string ParkingLotName { get; set; }
        public string PlateNumber { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is OrderDTO))
            {
                return false;
            }

            return Equals(this, obj as OrderDTO);
        }

        public bool Equals(OrderDTO @this, OrderDTO other)
        {
            return @this.ParkingLotName == other.ParkingLotName && @this.PlateNumber == other.PlateNumber;
        }
    }
}
