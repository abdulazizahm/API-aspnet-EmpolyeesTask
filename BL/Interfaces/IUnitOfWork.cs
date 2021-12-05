using BL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
        AccountRepository Account { get; }
        RoleRepository Role { get; }
        EmpolyeeRepository Empolyee { get; }


        int Commit();
    }
}
