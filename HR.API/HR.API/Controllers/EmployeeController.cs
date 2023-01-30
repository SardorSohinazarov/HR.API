using HR.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeRepository.GetEmployees());
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _employeeRepository.GetEmployee(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Employee employee)
        {
            var createdEmployee = await _employeeRepository.CreateEmployee(employee);
            var routeValue = new {id =  createdEmployee.Id};    
            return CreatedAtRoute(routeValue, createdEmployee);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]Employee employee)
        {
            return Ok(await _employeeRepository.UpdateEmployee(id, employee));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _employeeRepository.DeleteEmployee(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}
