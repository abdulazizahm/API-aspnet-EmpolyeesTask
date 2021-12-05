using BL.Interfaces;
using BL.Repositories;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Shared Props
        private DbContext EC_Context { get; set; }
        #endregion


        #region Props and Data Fields
        private AccountRepository account;
        public AccountRepository Account
        {
            get
            {
                account = account ?? new AccountRepository(EC_Context);
                return account;
            }
        }

        private RoleRepository role;
        public RoleRepository Role
        {
            get
            {
                role = role ?? new RoleRepository(EC_Context);
                return role;
            }
        }

        private EmpolyeeRepository empolyee;
        public EmpolyeeRepository Empolyee
        {
            get
            {
                empolyee = empolyee ?? new EmpolyeeRepository(EC_Context);
                return empolyee;
            }
        }

        


        // Main_CatRepository IUnitOfWork.mainCategory => throw new NotImplementedException();
        #endregion

        #region CTOR

        public UnitOfWork()
        {
            EC_Context = new ApplicationDbContext();
            //EC_Context.Configuration.LazyLoadingEnabled = false;
        }
        #endregion


        #region Methods

        public int Commit()
        {
            return EC_Context.SaveChanges();
        }

        public void Dispose()
        {
            EC_Context.Dispose();
        }
        #endregion
    }
}
