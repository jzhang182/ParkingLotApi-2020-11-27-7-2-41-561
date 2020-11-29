﻿using ParkingLotApi.DTOs;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApi.Services
{
    public interface IParkingLotService
    {
        public Task<string> AddAsync(ParkingLotDTO parkingLotDto);
        //public Task<List<ParkingLotDTO>> GetAllAsync(int? pageSize, int? pageIndex);
        //public Task<ParkingLotDTO> GetByIdAsync(string id);
        //public Task<ParkingLotDTO> GetByNameAsync(string lotName);
        //public Task DeleteAsync(string id);
        //public Task UpdateAsync(string id, ParkingLotUpdateDTO parkingLotUpdateDto);
    }

    public class ParkingLotService : IParkingLotService
    {
        private readonly ParkingLotContext parkingLotContext;

        public ParkingLotService(ParkingLotContext parkingLotContext)
        {
            this.parkingLotContext = parkingLotContext;
        }

        public async Task<string> AddAsync(ParkingLotDTO parkingLotDto)
        {
            var parkingLot = new ParkingLotEntity(parkingLotDto);
            if (parkingLotContext.ParkingLots.Any(p => p.Name == parkingLot.Name))
            {
                return string.Empty;
            }

            if (parkingLotDto.Capacity < 0)
            {
                return string.Empty;
            }

            await parkingLotContext.ParkingLots.AddAsync(parkingLot);
            await parkingLotContext.SaveChangesAsync();
            return parkingLot.Id;
        }
    }
}
