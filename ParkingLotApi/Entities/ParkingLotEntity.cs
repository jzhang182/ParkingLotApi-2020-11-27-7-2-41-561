using ParkingLotApi.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApi.Entities
{
    public class ParkingLotEntity
    {
        public ParkingLotEntity()
        {
        }

        public ParkingLotEntity(ParkingLotDTO parkingLotDto)
        {
            Name = parkingLotDto.Name;
            Capacity = parkingLotDto.Capacity;
            Location = parkingLotDto.Location;
            PositionsLeft = Capacity;
        }

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public int PositionsLeft { get; set; }
    }
}
