using BL.AppServices;
using BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AccountController : ApiController
    {
        AccountAppService account = new AccountAppService();
        [HttpPost]
        public IHttpActionResult Register(RegisterDto registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                account.Register(registerViewModel);
                return Created("", "Register Success =>" + registerViewModel.Username);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
