using EmployeesTables.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace EmployeesTables.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync("https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code={key}");

            var employees = JsonConvert.DeserializeObject<List<Employee>>(response);

            // Order employees by total time worked
            employees.Sort((a, b) => a.TotalTimeWorked.CompareTo(b.TotalTimeWorked));

            return View(employees);
        }

    //private readonly ILogger<HomeController> _logger;

    //    public HomeController(ILogger<HomeController> logger)
    //    {
    //        _logger = logger;
    //    }

    //    //public IActionResult Index()
    //    //{
    //    //    return View();
    //    //}

    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
   }


}
