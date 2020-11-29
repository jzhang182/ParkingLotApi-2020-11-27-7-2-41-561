using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLotApi.DTOs;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotsController : ControllerBase
    {
        private readonly IParkingLotService parkingLotService;
        public ParkingLotsController(IParkingLotService parkingLotService)
        {
            this.parkingLotService = parkingLotService;
        }

        [HttpPost]
        public async Task<ActionResult<ParkingLotDTO>> AddAsync(ParkingLotDTO parkingLotDto)
        {
            var newLotId = await this.parkingLotService.AddAsync(parkingLotDto);
            if (string.IsNullOrEmpty(newLotId))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { Id = newLotId }, parkingLotDto);
        }

        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }
    }
}
