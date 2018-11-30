using System.Web.Mvc;
using Kadry.Models;

namespace Kadry.Controllers
{
    public class AdminController : Controller
    {
        private readonly SQLConnection _sqlConnection = new SQLConnection();

        // GET: Admin
        public ActionResult EmployeerList()
        {
            var list = _sqlConnection.GetAllEmployeers(); 
            return View(list);
        }

        public ActionResult DetailsEmployeer(int id)
        {
            var employer = _sqlConnection.GetEmployer(id);
            return View(employer);
        }
    }
}