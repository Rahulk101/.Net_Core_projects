using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Venkat.Models;

namespace Venkat.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context=context;
        }
        public Employee AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public Employee Delete(int Id)
        {
            Employee employee=_context.Employees.Find(Id);
            if(employee!=null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            // Employee employee=_context.Employees.FirstOrDefault(m=>m.Id==Id);
            // return employee;
            return _context.Employees.Find(Id);
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee=_context.Employees.Find(employeeChanges.Id);
            if(employee!=null)
            {
                // employee.Name=employeeChanges.Name;
                // employee.Email=employeeChanges.Email;
                // employee.Department=employeeChanges.Department;
                _context.Employees.Remove(employee);
                _context.Employees.Add(employeeChanges);
                _context.SaveChanges();
            }
            return employeeChanges;
        }
    }
}