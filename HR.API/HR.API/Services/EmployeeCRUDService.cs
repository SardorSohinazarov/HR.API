using HR.API.Models;
using HR.DataAccess;
using HR.DataAccess.Entities;

namespace HR.API.Services
{
    public class EmployeeCRUDService : IGenericCRUDService<EmployeeModel>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeCRUDService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeModel> Create(EmployeeModel employeeModel)
        {
            var employee = new Employee
            {
                Id = employeeModel.Id,
                FullName = employeeModel.FullName,
                Department = employeeModel.Department,
                Email = employeeModel.Email
            };
            var createdEmployee = await _employeeRepository.CreateEmployee(employee);
            var createEmployeeModel = new EmployeeModel
            {
                Id = createdEmployee.Id,
                FullName = createdEmployee.FullName,
                Department = createdEmployee.Department,
                Email = createdEmployee.Email
            };
            return createEmployeeModel;
        }

        public async Task<bool> Delete(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }

        public async Task<EmployeeModel> Get(int id)
        {
            var employee = await _employeeRepository.GetEmployee(id);
            var model = new EmployeeModel
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Department = employee.Department,
                Email = employee.Email
            };
            return model;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAll()
        {
            var employees = await _employeeRepository.GetEmployees();
            var result = new List<EmployeeModel>();
            foreach(var employee in employees)
            {
                var employeeModel = new EmployeeModel
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Department = employee.Department,
                    Email = employee.Email
                };
                result.Add(employeeModel);  
            }
            return result;
        }

        public async Task<EmployeeModel> Update(int id, EmployeeModel employeeModel)
        {
            var employee = new Employee
            {
                Id = employeeModel.Id,
                FullName = employeeModel.FullName,
                Department = employeeModel.Department,
                Email = employeeModel.Email
            };
            var updatedemployee = await _employeeRepository.UpdateEmployee(id, employee);
            var updatedEmployeeModel = new EmployeeModel
            {
                Id = updatedemployee.Id,
                FullName = updatedemployee.FullName,
                Department = updatedemployee.Department,
                Email = updatedemployee.Email
            };
            return updatedEmployeeModel;
        }
    }
}
