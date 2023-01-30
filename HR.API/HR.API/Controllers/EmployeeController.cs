using HR.API.Models;
using HR.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericCRUDService<EmployeeModel> _employeeSvc;

        public EmployeeController(IGenericCRUDService<EmployeeModel> employeeSvc)
        {
            _employeeSvc = employeeSvc;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeSvc.GetAll());
        }

        [HttpGet("employee")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _employeeSvc.Get(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmployeeModel employee)
        {
            var createdEmployee = await _employeeSvc.Create(employee);
            var routeValue = new {id =  createdEmployee.Id};    
            return CreatedAtRoute(routeValue, createdEmployee);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]EmployeeModel employee)
        {
            return Ok(await _employeeSvc.Update(id, employee));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _employeeSvc.Delete(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}
