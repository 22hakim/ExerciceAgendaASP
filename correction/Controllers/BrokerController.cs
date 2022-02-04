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
            IEnumerable<Broker> BrokerList = _dbConnect.Brokers;
            return View(BrokerList);
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

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var broker = _dbConnect.Brokers.Find(id);

            if (broker == null)
            {
                return NotFound();
            }

            return View(broker);
        }
    }
}
