using System.Web.Mvc;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.Controllers
{
    public class BaseController : Controller
    {
        protected readonly VendorRepository VendorRepo;
        protected readonly StateRepository StateRepo;
        protected readonly EmployeeRepository EmployeeRepo;
        protected readonly CityRepository CityRepo;

        public BaseController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDBConnection"].ToString();
            VendorRepo = new VendorRepository(connectionString);
            StateRepo = new StateRepository(connectionString);
            EmployeeRepo = new EmployeeRepository(connectionString);
            CityRepo = new CityRepository(connectionString);
        }
    }
}
