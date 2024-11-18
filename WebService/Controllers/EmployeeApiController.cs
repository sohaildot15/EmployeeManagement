using EmployeeManagement.DAL.Models;
using EmployeeManagement.DAL.Repositories;
using System.Net;
using System.Web.Http;

public class EmployeeApiController : ApiController
{
    private readonly EmployeeRepository _employeeRepo;

    public EmployeeApiController()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDBConnection"].ToString();
        _employeeRepo = new EmployeeRepository(connectionString);
    }

    // GET api/EmployeeApi
    [HttpGet]
    public IHttpActionResult GetEmployees()
    {
        var employees = _employeeRepo.GetAllEmployees();
        if (employees == null)
        {
            return NotFound();
        }
        return Ok(employees);
    }

    // GET api/EmployeeApi/{id}
    [HttpGet]
    public IHttpActionResult GetEmployeeById(int id)
    {
        var employee = _employeeRepo.GetEmployeeById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    // POST api/EmployeeApi
    [HttpPost]
    public IHttpActionResult AddEmployee([FromBody] Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var existingEmployee = _employeeRepo.GetEmployeeById(employee.EmployeeCode);
        if (existingEmployee != null)
        {
            return Content(HttpStatusCode.Conflict, "Employee with the given EmployeeCode already exists.");
        }
        _employeeRepo.AddEmployee(employee);
        return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeCode }, employee);
    }

    // PUT api/EmployeeApi/{id}
    [HttpPut]
    public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
    {
        if (id != employee.EmployeeCode)
        {
            return BadRequest();
        }

        var existingEmployee = _employeeRepo.GetEmployeeById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        _employeeRepo.UpdateEmployee(employee);
        return Content(HttpStatusCode.OK, "Employee with the given EmployeeCode updated successfully.");
    }

    // DELETE api/EmployeeApi/{id}
    [HttpDelete]
    public IHttpActionResult DeleteEmployee(int id)
    {
        var employee = _employeeRepo.GetEmployeeById(id);
        if (employee == null)
        {
            return NotFound();
        }

        _employeeRepo.DeleteEmployee(id);
        return Content(HttpStatusCode.OK, "Employee with the given EmployeeCode deleted successfully.");
    }
}
