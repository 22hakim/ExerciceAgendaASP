using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using correction.Models;
using correction.Data;


namespace correction.Controllers
{
    public class BrokerController : Controller
    {
        private readonly agendaContext _dbConnect;

        public BrokerController(agendaContext connexionDb)
        {
            _dbConnect = connexionDb;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Add(Broker broker)
        {
            if (ModelState.IsValid)
            {
                _dbConnect.Brokers.Add(broker);
                _dbConnect.SaveChanges();

                TempData["success"] = "le courtier a bien été ajouté";

                return RedirectToAction("Index","Home");

            }
            return View();
        }
    }
}
