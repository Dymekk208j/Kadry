using System.Web.Mvc;
using Kadry.Models;

namespace Kadry.Controllers
{
    public class AdminController : Controller
    {
        private readonly SQLConnection _sqlConnection = new SQLConnection();

        public ActionResult EmployerList()
        {
            var list = _sqlConnection.GetAllEmployeers(); 
            return View(list);
        }

        public ActionResult DetailsEmployer(int id)
        {
            var employer = _sqlConnection.GetEmployer(id);
            return View(employer);
        }

        [HttpPost]
        public ActionResult CreateEmployer(Employeer employer)
        {
            _sqlConnection.CreateEmployer(employer);

            return RedirectToAction("EmployerList");
        }

    }
}