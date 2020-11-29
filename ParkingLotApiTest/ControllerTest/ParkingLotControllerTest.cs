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
    public class ParkingLotControllerTest : TestBase
    {
        private readonly ParkingLotContext context;
        public ParkingLotControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            context = scopedServices.GetRequiredService<ParkingLotContext>();
        }

        [Fact]
        public async Task Should_add_parking_lot_when_post()
        {
            var client = GetClient();
            var parkingLotDto = new ParkingLotDTO()
            {
                Name = "myLot",
                Capacity = 1,
                Location = " ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);

            var createResponse = await client.PostAsync($"/parkinglots", content);

            createResponse.EnsureSuccessStatusCode();
            Assert.Single(context.ParkingLots.ToList());
            var firstLot = await context.ParkingLots.FirstOrDefaultAsync();
            Assert.Equal(parkingLotDto, new ParkingLotDTO(firstLot));
        }
    }
}