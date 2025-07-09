using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LoginDashboardApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // This attribute ensures only authenticated users can access this controller
    public class DashboardController : ControllerBase
    {
        [HttpGet("chartdata")]
        public IActionResult GetChartData()
        {
            // Dummy data for the chart
            var random = new Random();
            var data = new
            {
                labels = new[] { "January", "February", "March", "April", "May", "June", "July" },
                datasets = new[]
                {
                    new {
                        label = "Sample Dataset 1",
                        data = Enumerable.Range(0, 7).Select(_ => random.Next(0, 100)).ToArray(),
                        backgroundColor = "rgba(255, 99, 132, 0.2)",
                        borderColor = "rgba(255, 99, 132, 1)",
                        borderWidth = 1
                    },
                    new {
                        label = "Sample Dataset 2",
                        data = Enumerable.Range(0, 7).Select(_ => random.Next(0, 100)).ToArray(),
                        backgroundColor = "rgba(54, 162, 235, 0.2)",
                        borderColor = "rgba(54, 162, 235, 1)",
                        borderWidth = 1
                    }
                }
            };
            return Ok(data);
        }
    }
}
