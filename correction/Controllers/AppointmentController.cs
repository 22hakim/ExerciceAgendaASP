using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using correction.Data;
using correction.Models;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace correction.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly agendaContext _dbConnect;

        public AppointmentController(agendaContext connexionDb)
        {
            _dbConnect = connexionDb;
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

            a.DateHour = makeDate(a.DateHour);

            if(chekDate(a.DateHour, a.IdBroker))
            {
                TempData["success"] = "Un rendez vous existe deja a cette heure ci";
                ViewBag.Customers = GetCustomers();
                ViewBag.Brokers = GetBrokers();
                return View();
            }

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
        public ActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var a = _dbConnect.Appointments.Find(id);

            if(a == null)
            {
                TempData["success"] = "Aucun rendez vous trouvé";

                return RedirectToAction("Index", "Home");
            }

            _dbConnect.Appointments.Remove(a);
            _dbConnect.SaveChanges();

            TempData["success"] = "le rendez vous a bien été supprimé";

            return RedirectToAction("Index", "Home");
        }



        public static DateTime makeDate(DateTime date)
        {
            int min = date.Minute;
            min = (min > 30) ? 30 : 00;

            return new DateTime(date.Year, date.Month, date.Day, date.Hour, min, 00);

        }

        // retourne vrai si la date existe deja 
        public bool chekDate(DateTime date,int idBroker)
        {
            return _dbConnect.Brokers.Include(b => b.Appointments).FirstOrDefault(x => x.IdBroker == idBroker).Appointments.Any(x => x.DateHour == date);
        }
    }
}
