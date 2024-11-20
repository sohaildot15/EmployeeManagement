using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : BaseController
    {
        public ActionResult Index()
        {

            var employees = EmployeeRepo.GetAllEmployees();
            foreach (var employee in employees)
            {
                employee.StateName = StateRepo.GetStateNameById(employee.StateID);
                employee.CityName = CityRepo.GetCityNameById(employee.StateID);
            }
            return View(employees);
        }

        [HttpGet]
        public ActionResult GetEmployeeDetail(int id)
        {
            var employee = EmployeeRepo.GetEmployeeById(id);
            if (employee == null)
                return HttpNotFound();

            employee.StateName = StateRepo.GetStateNameById(employee.StateID);
            employee.CityName = CityRepo.GetCityNameById(employee.CityID);
            return PartialView("_EmployeeDetailsPartial", employee);
        }

        [HttpGet]
        public ActionResult AddEditEmployee(int? id)
        {
            Employee employee = id.HasValue ? EmployeeRepo.GetEmployeeById(id.Value) : new Employee();
            PopulateDropdowns(employee);
            return PartialView("_AddEditEmployeePartial", employee);
        }

        [HttpPost]
        public ActionResult AddEditEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropdowns(employee);
                return PartialView("_AddEditEmployeePartial", employee);
            }

            if (employee.EmployeeCode > 0)
            {
                EmployeeRepo.UpdateEmployee(employee);
            }
            else
            {
                EmployeeRepo.AddEmployee(employee);
            }

            return Json(new { success = true });
        }

        private void PopulateDropdowns(Employee employee)
        {
            var vendors = VendorRepo.GetAllVendors();
            ViewBag.Vendors = new SelectList(vendors, "VendorCode", "VendorName", employee?.VendorCode);

            var states = StateRepo.GetAllStates();
            ViewBag.States = new SelectList(states, "StateID", "StateName", employee?.StateID);

            if (employee?.StateID > 0)
            {
                var cities = CityRepo.GetCitiesByState(employee.StateID);
                ViewBag.Cities = new SelectList(cities, "CityID", "CityName", employee?.CityID);
            }
            else
            {
                ViewBag.Cities = new SelectList(Enumerable.Empty<object>(), "CityID", "CityName");
            }
        }

        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            var employee = EmployeeRepo.GetEmployeeById(id);
            if (employee == null)
            {
                return Json(new { success = false, message = "Employee not found." });
            }

            EmployeeRepo.DeleteEmployee(id);
            return Json(new { success = true });
        }

        public JsonResult GetCitiesByState(int stateId)
        {
            var cities = CityRepo.GetCitiesByState(stateId);
            return Json(cities.Select(c => new { c.CityID, c.CityName }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateTaskStatus(int id, string taskStatus)
        {
            try
            {
                EmployeeRepo.UpdateTaskStatus(id, taskStatus);
                return Json(new { success = true, message = "Task status updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }

        [HttpPost]
        public ActionResult ApproveTasks(int[] ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    EmployeeRepo.UpdateTaskStatus(id, "Completed");
                }

                return Json(new { success = true, message = "Selected tasks approved successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }



    }
}