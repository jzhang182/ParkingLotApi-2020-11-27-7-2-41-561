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
    }
}
