using HR.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HR.DataAccess.Repository;

public class EmployeeRepository : IGenericRepository<Employee>
{
    private readonly AppDbContext _appDbContext;
    public EmployeeRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Employee> Create(Employee employee)
    {
        await _appDbContext.Employees.AddAsync(employee);
        await _appDbContext.SaveChangesAsync();
        return employee;
    }

    public async Task<bool> Delete(int id)
    {
        var employee = await _appDbContext.Employees.FindAsync(id);
        if (employee is null)
            return false;
        _appDbContext.Employees.Remove(employee);
        _appDbContext.SaveChanges();
        return true;
    }

    public async Task<Employee> Get(int id)
    {
        return await _appDbContext.Employees.FindAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _appDbContext.Employees.ToListAsync();
    }
    public async Task<Employee> Update(int id, Employee employee)
    {
        var updatedEmployee = _appDbContext.Employees.Attach(employee);
        updatedEmployee.State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
        return employee;
    }
}
