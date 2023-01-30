﻿using HR.DataAccess;
using HR.DataAccess.Entities;

namespace HR.DataAccess
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee> GetEmployee(int id);
        public Task<Employee> CreateEmployee(Employee employee);
        public Task<Employee> UpdateEmployee(int id, Employee employee);
        public Task<bool> DeleteEmployee(int id);
    }
}