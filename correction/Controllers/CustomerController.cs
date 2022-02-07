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

                return RedirectToAction("Index", "Home");
            }

            return View();
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

            /*            // utilisation du même code sans les using 
             *            // raison: utiliser l'objet que localement 
             *            // permet d'encadrer seulement à un endroit l'utilisation des objets SQLCo 
             *       
             *            SqlConnection con = new SqlConnection(_dbString);
                        SqlCommand cmd = new SqlCommand(query);

                        // lancement de ma connexion 
                        cmd.Connection = con;

                        // ouverture de ma connexion ( dans le sens j'ouvre la récuperation des data ) 
                        con.Open();

                        SqlDataReader sdr = cmd.ExecuteReader();

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

                        // ferme l'arrivée de donnée. ma requete est terminée 
                        con.Close();*/

            return customers;
        }

    }
}
