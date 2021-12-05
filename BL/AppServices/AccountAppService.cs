using AutoMapper;
using BL.Bases;
using BL.DTO;
using DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.AppServices
{
    public class AccountAppService : BaseAppService
    {
        public ApplicationUser Find(string username, string password)
        {
            return TheUnitOfWork.Account.Find(username, password);
        }

        public ApplicationUser Find(string username)
        {
            return TheUnitOfWork.Account.Find(username);
        }

  

        public ApplicationUser FindById(string id)
        {
            return TheUnitOfWork.Account.Where(u => u.Id == id).FirstOrDefault();
        }


        public IdentityResult Register(RegisterDto userModel)
        {
            ApplicationUser user = Mapper.Map<ApplicationUser>(userModel);
            return TheUnitOfWork.Account.Register(user);
        }

    }
}
