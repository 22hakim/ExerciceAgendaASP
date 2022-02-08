using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using correction.Data;
using correction.Models;
using System.Dynamic;

namespace correction.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly agendaContext _dbConnect;

        public AppointmentController(agendaContext connexionDb)
        {
            _dbConnect = connexionDb;
        }
        // GET: AppointmentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AppointmentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppointmentController/Add
        public ActionResult Add()
        {
            ViewBag.Customers = GetCustomers();
            ViewBag.Brokers = GetBrokers();
            return View();
        }

        // POST: AppointmentController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Appointment a)
        {

            if(a.Subject != null && a.DateHour != null)
            {
                _dbConnect.Add(a);
                _dbConnect.SaveChanges();

                TempData["success"] = "le rendez vous a bien été ajouté";

                return RedirectToAction("Index","Home");
            }
            

            ViewBag.Customers = GetCustomers();
            ViewBag.Brokers = GetBrokers();
            return View();
        }

        private IEnumerable<Broker> GetBrokers()
        {
            return _dbConnect.Brokers;

        }

        private IEnumerable<Customer> GetCustomers()
        {
            return _dbConnect.Customers;
        }



        // GET: AppointmentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
