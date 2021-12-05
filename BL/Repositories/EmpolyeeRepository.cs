using BL.Bases;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class EmpolyeeRepository : BaseRepository<Employee>
    {
        public EmpolyeeRepository(DbContext db) : base(db)
        {
        }

        #region CRUB

        public List<Employee> GetAllEmployees()
        {
            return GetAll().ToList();
        }

        public bool InsertEmployee(Employee Employee)
        {
            return Insert(Employee);
        }
        public void UpdateEmployee(Employee Employee)
        {
            Update(Employee);
        }
        public void DeleteEmployee(int id)
        {
            Delete(id);
        }

        public bool CheckEmployeeExists(Employee Employee)
        {
            return GetAny(b => b.Id == Employee.Id);
        }
        public Employee GetEmployeeById(int id)
        {
            return GetFirstOrDefault(b => b.Id == id);
        }
        #endregion
    }
}
