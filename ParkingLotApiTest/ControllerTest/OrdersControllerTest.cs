using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ParkingLotApi;
using ParkingLotApi.DTOs;
using ParkingLotApi.Entities;
using ParkingLotApi.Repository;
using Xunit;

namespace ParkingLotApiTest.ControllerTest
{
    [Collection("ControllerTest")]
    public class OrdersControllerTest : TestBase
    {
        public OrdersControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_add_order_when_post()
        {
            var client = GetClient();
            var parkingLotDto = new ParkingLotDTO()
            {
                Name = "myLot1",
                Capacity = 1,
                Location = " ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync($"/parkinglots", content);
            var orderDto = new OrderDTO()
            {
                ParkingLotName = "myLot1",
                PlateNumber = "234",
            };
            var httpContent2 = JsonConvert.SerializeObject(orderDto);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            var createResponse = await client.PostAsync($"/orders", content2);

            createResponse.EnsureSuccessStatusCode();
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ParkingLotContext>();
            var firstOrder = await context.Orders.FirstOrDefaultAsync();

            Assert.Equal(orderDto, new OrderDTO(firstOrder));
        }
    }
}
