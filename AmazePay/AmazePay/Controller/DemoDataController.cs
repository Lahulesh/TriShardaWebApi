using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazePay.Controller
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DemoDataController : ControllerBase
    {
        [HttpGet]
        [Route("/demoData")]
        public IActionResult GetDemoData()
        {
            Random random = new Random();
            int randomNumber = random.Next();
            double randomDouble = random.NextDouble();

            string[] firstNames = { "John", "Jane", "Bob", "Alice", "Charlie", "Emma", "David", "Olivia" };
            string[] lastNames = { "Smith", "Johnson", "Brown", "Lee", "Taylor", "Wilson", "Clark", "Walker" };

            string randomFirstName = firstNames[random.Next(firstNames.Length)];
            string randomLastName = lastNames[random.Next(lastNames.Length)];

            var dummy = new
            {
                Name = "John Doeing",
                Email = "john.doe@example.com",
                Number = randomNumber,
                NumberDouble = randomDouble,
                RandomFirstName = firstNames[random.Next(firstNames.Length)],
                RandomLastName = lastNames[random.Next(lastNames.Length)],
            };
            return Ok(dummy);
        }
    }
}
