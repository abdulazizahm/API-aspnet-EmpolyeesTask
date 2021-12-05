using AutoMapper;
using BL.Bases;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class EmpolyeeAppService : BaseAppService
    {
        #region CURD
       
        public List<Employee> GetAllEmployee()
        {
            return Mapper.Map<List<Employee>>(TheUnitOfWork.Empolyee.GetAllEmployees());
        }
        public Employee GetEmployee(int id)
        {
            return TheUnitOfWork.Empolyee.GetEmployeeById(id);
        }



        public bool SaveNewEmployee(Employee Employee)
        {
            bool result = false;
            var empolyee = Mapper.Map<Employee>(Employee);
            if (TheUnitOfWork.Empolyee.Insert(empolyee))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateEmployee(Employee Employee)
        {
            TheUnitOfWork.Empolyee.Update(Employee);
            TheUnitOfWork.Commit();
            return true;
        }


        public bool DeleteEmployee(int id)
        {
            TheUnitOfWork.Empolyee.Delete(id);
            bool result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckEmployeeExists(Employee Employee)
        {
            //Employee Employee = Mapper.Map<Employee>(Employee);
            return TheUnitOfWork.Empolyee.CheckEmployeeExists(Employee);
        }
        #endregion
    }
}
