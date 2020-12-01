using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkingLotApi.DTOs;
using ParkingLotApi.Entities;
using ParkingLotApi.Services;

namespace ParkingLotApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IParkingLotService parkingLotService;
        public OrdersController(IOrderService orderService, IParkingLotService parkingLotService)
        {
            this.orderService = orderService;
            this.parkingLotService = parkingLotService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> AddAsync(OrderDTO orderDto)
        {
            var orderNumber = await this.orderService.AddAsync(orderDto);
            if (orderNumber == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { orderNumber }, orderNumber);
        }

        [HttpGet("{orderNumber}")]
        public async Task<ActionResult<OrderDTO>> Get(string orderNumber)
        {
            var target = await this.orderService.GetAsync(orderNumber);
            if (target == null)
            {
                return NotFound();
            }

            return Ok(target);
        }

        [HttpPatch("{orderNumber}")]
        public async Task<ActionResult<OrderEntity>> UpdateAsync(string orderNumber, OrderUpdateModel orderUpdateModel)
        {
            var order = await this.orderService.GetAsync(orderNumber);

            if (order == null)
            {
                return NotFound();
            }

            if (order.IsOpen != true)
            {
                return BadRequest();
            }

            var result = await this.orderService.UpdateAsync(orderNumber, orderUpdateModel);

            return result;
        }
    }
}
