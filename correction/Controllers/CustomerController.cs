using Microsoft.AspNetCore.Mvc;
using correction.Models;
using correction.Data;
using Microsoft.Data.SqlClient;

namespace correction.Controllers
{
    public class CustomerController : Controller
    {
        private readonly agendaContext _dbConnect;
        private readonly string _dbString;

        public CustomerController(agendaContext connexionDb)
        {
            _dbConnect = connexionDb;
            _dbString = "Server=localhost;Database=agenda;Trusted_Connection=True";
        }

        public IActionResult Index()
        {
            IEnumerable<Customer> customers = GetAllCutomers();
            return View(customers);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dbConnect.Add(customer);
                _dbConnect.SaveChanges();

                TempData["success"] = "le courtier a bien été ajouté";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var customer = _dbConnect.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var customer = _dbConnect.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            // j'utilise l'ID trouvé dans l'url pour savoir quel broker modifier   
            customer.IdCustomer = id;

            if (ModelState.IsValid)
            {
                _dbConnect.Customers.Update(customer);
                _dbConnect.SaveChanges();

                TempData["success"] = "le client a bien été modifié";

                return RedirectToAction("Index");

            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            // si j'ai pas d'id je retourne a la liste des customers
            if (id == null || id == 0)
            {
                return RedirectToAction("Index");
            }
            var customer = _dbConnect.Customers.Find(id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer cus, int id)
        {
            cus.IdCustomer = id;

            _dbConnect.Customers.Remove(cus);
            _dbConnect.SaveChanges();
             
            TempData["success"] = "le client a bien été supprimé";

            return RedirectToAction("Index");
        }

        public IEnumerable<Customer> GetAllCutomers()
        {
            List<Customer> customers = new List<Customer>();
            string query = "SELECT * FROM Customers ORDER BY lastname";
            using (SqlConnection con = new SqlConnection(_dbString))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    // lancement de ma connexion 
                    cmd.Connection = con;
                    // ouverture de ma connexion ( dans le sens j'ouvre la récuperation des data ) 
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new Customer
                            {
                                IdCustomer = Convert.ToInt32(sdr["idCustomer"]),
                                Firstname = Convert.ToString(sdr["firstname"]),
                                Lastname = Convert.ToString(sdr["lastname"]),
                                Mail = Convert.ToString(sdr["mail"]),
                                PhoneNumber = Convert.ToString(sdr["phoneNumber"]),
                                Budget = Convert.ToInt32(sdr["budget"])
                            });
                        }
                    }

                    // ferme l'arrivée de donnée. ma requete est terminée 
                    con.Close();
                }
            }


            return customers;
        }
    }
}
