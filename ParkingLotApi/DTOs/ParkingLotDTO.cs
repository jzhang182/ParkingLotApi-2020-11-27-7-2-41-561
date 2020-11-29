using ParkingLotApi.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotApi.DTOs
{
    public class ParkingLotDTO
    {
        public ParkingLotDTO() 
        { 
        }

        public ParkingLotDTO(ParkingLotEntity parkingLotEntity)
        {
            Name = parkingLotEntity.Name;
            Capacity = parkingLotEntity.Capacity;
            Location = parkingLotEntity.Location;
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is ParkingLotDTO))
            {
                return false;
            }

            return Equals(this, obj as ParkingLotDTO);
        }

        public bool Equals(ParkingLotDTO @this, ParkingLotDTO other)
        {
            return @this.Name == other.Name && @this.Capacity == other.Capacity && @this.Location == other.Location;
        }
    }
}
