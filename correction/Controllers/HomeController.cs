using correction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using correction.Data;


namespace correction.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly agendaContext _dbConnect;

        public HomeController(ILogger<HomeController> logger, agendaContext connexionDb)
        {
            _dbConnect = connexionDb;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var AppointmentViewModel = from Appointment in _dbConnect.Appointments
                                       join Broker in _dbConnect.Brokers on Appointment.IdBroker equals Broker.IdBroker
                                       join Customer in _dbConnect.Customers on Appointment.IdCustomer equals Customer.IdCustomer

                                       select new AppointmentViewModel
                                       {
                                           AppointmentVm = Appointment,
                                           BrokerInfo = Broker,
                                           CustomerInfo = Customer
                                       };
            return View(AppointmentViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}