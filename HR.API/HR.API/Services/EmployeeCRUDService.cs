using HR.API.Models;
using HR.DataAccess.Entities;
using HR.DataAccess.Repository;

namespace HR.API.Services;

public class EmployeeCRUDService : IGenericCRUDService<EmployeeModel>
{
    private readonly IGenericRepository<Employee> _employeeRepository;
    private readonly IGenericRepository<Address> _addressRepository;

    public EmployeeCRUDService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Address> addressRepository)
    {
        _employeeRepository = employeeRepository;
        _addressRepository = addressRepository;
    }

    public async Task<EmployeeModel> Create(EmployeeModel employeeModel)
    {
        var address = await _addressRepository.Get(employeeModel.AddressId);
        var employee = new Employee
        {
            Id = employeeModel.Id,
            FullName = employeeModel.FullName,
            Department = employeeModel.Department,
            Email = employeeModel.Email,
            Salary = employeeModel.Salary
        };
        if(employee is not null)
            employee.Address = address; 

        var createdEmployee = await _employeeRepository.Create(employee);
        var createEmployeeModel = new EmployeeModel
        {
            Id = createdEmployee.Id,
            FullName = createdEmployee.FullName,
            Department = createdEmployee.Department,
            Email = createdEmployee.Email,
            Salary = createdEmployee.Salary,
            AddressId = createdEmployee.Address.Id
        };
        return createEmployeeModel;
    }

    public async Task<bool> Delete(int id)
    {
        return await _employeeRepository.Delete(id);
    }

    public async Task<EmployeeModel> Get(int id)
    {
        var employee = await _employeeRepository.Get(id);
        var model = new EmployeeModel
        {
            Id = employee.Id,
            FullName = employee.FullName,
            Department = employee.Department,
            Email = employee.Email,
            Salary = employee.Salary,
        };
        return model;
    }

    public async Task<IEnumerable<EmployeeModel>> GetAll()
    {
        var employees = await _employeeRepository.GetAll();
        var result = new List<EmployeeModel>();
        foreach(var employee in employees)
        {
            var employeeModel = new EmployeeModel
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Department = employee.Department,
                Email = employee.Email,
                Salary = employee.Salary,
            };
            result.Add(employeeModel);  
        }
        return result;
    }

    public async Task<EmployeeModel> Update(int id, EmployeeModel employeeModel)
    {
        var employee = new Employee
        {
            Id = id,
            FullName = employeeModel.FullName,
            Department = employeeModel.Department,
            Email = employeeModel.Email,
            Salary = employeeModel.Salary,
        };
        var updatedemployee = await _employeeRepository.Update(id, employee);
        var updatedEmployeeModel = new EmployeeModel
        {
            Id = updatedemployee.Id,
            FullName = updatedemployee.FullName,
            Department = updatedemployee.Department,
            Email = updatedemployee.Email,
            Salary = updatedemployee.Salary,
        };
        return updatedEmployeeModel;
    }
}
