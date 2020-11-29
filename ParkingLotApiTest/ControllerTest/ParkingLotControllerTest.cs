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
    public class ParkingLotControllerTest : TestBase
    {
        public ParkingLotControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_add_parking_lot_when_post()
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

            var createResponse = await client.PostAsync($"/parkinglots", content);

            createResponse.EnsureSuccessStatusCode();
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ParkingLotContext>();
            var firstLot = await context.ParkingLots.FirstOrDefaultAsync();
            Assert.Equal(parkingLotDto, new ParkingLotDTO(firstLot));
        }

        [Fact]
        public async Task Should_delete_parking_lot_when_delete()
        {
            var client = GetClient();
            var parkingLotDto = new ParkingLotDTO()
            {
                Name = "myLot2",
                Capacity = 1,
                Location = " ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse = await client.PostAsync($"/parkinglots", content);
            var id = postResponse.Headers.Location.AbsoluteUri.Split("/")[4];

            var response = await client.DeleteAsync($"/parkinglots/{id}");

            response.EnsureSuccessStatusCode();
            var scope = Factory.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var context = scopedServices.GetRequiredService<ParkingLotContext>();
            Assert.Empty(context.ParkingLots.ToList());
        }

        [Fact]
        public async Task Should_query_parking_lot_when_get_by_id()
        {
            var client = GetClient();
            var parkingLotDto = new ParkingLotDTO()
            {
                Name = "myLot3",
                Capacity = 1,
                Location = " ",
            };
            var httpContent = JsonConvert.SerializeObject(parkingLotDto);
            StringContent content = new StringContent(httpContent, Encoding.UTF8, MediaTypeNames.Application.Json);
            var postResponse = await client.PostAsync($"/parkinglots", content);
            var id = postResponse.Headers.Location.AbsoluteUri.Split("/")[4];

            var response = await client.GetAsync($"/parkinglots/{id}");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            ParkingLotDTO actualLot = JsonConvert.DeserializeObject<ParkingLotDTO>(responseString);
            Assert.Equal(parkingLotDto, actualLot);
        }

        [Fact]
        public async Task Should_query_parking_lot_when_get_by_page()
        {
            var client = GetClient();
            var parkingLotDto1 = new ParkingLotDTO()
            {
                Name = "myLot1",
                Capacity = 1,
                Location = " ",
            };
            var parkingLotDto2 = new ParkingLotDTO()
            {
                Name = "myLot2",
                Capacity = 1,
                Location = " ",
            };
            var httpContent1 = JsonConvert.SerializeObject(parkingLotDto1);
            StringContent content1 = new StringContent(httpContent1, Encoding.UTF8, MediaTypeNames.Application.Json);
            var httpContent2 = JsonConvert.SerializeObject(parkingLotDto1);
            StringContent content2 = new StringContent(httpContent2, Encoding.UTF8, MediaTypeNames.Application.Json);
            await client.PostAsync($"/parkinglots", content1);
            await client.PostAsync($"/parkinglots", content2);

            var response = await client.GetAsync($"/parkinglots?pagesize=1&pageindex=1");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<ParkingLotDTO> actualLots = JsonConvert.DeserializeObject<List<ParkingLotDTO>>(responseString);
            Assert.Equal(new List<ParkingLotDTO> { parkingLotDto1 }, actualLots);
        }
    }
}