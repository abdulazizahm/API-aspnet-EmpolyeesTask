using BL.AppServices;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class EmployeeController : ApiController
    {
        EmpolyeeAppService _EmpolyeeAppService = new EmpolyeeAppService();
        // GET: api/Employee
        public IHttpActionResult Get()
        {
            return Ok(_EmpolyeeAppService.GetAllEmployee());



        }

        // GET: api/Employee/5
        public IHttpActionResult Get(int id)
        {
            return Ok(_EmpolyeeAppService.GetEmployee(id));
        }

        // POST: api/Employee
       
        public IHttpActionResult Post() //to receive file use httpcontext
        {
            var Employee = new Employee();
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
            #region Fill Data Manually From HttpContext
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Name"]))
            {
                Employee.Name = HttpContext.Current.Request.Form["Name"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["SSN"]))
            {
                Employee.SSN = HttpContext.Current.Request.Form["SSN"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Address"]))
            {
                Employee.Address = HttpContext.Current.Request.Form["Address"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Salary"]))
            {
                Employee.Salary = Convert.ToDouble(HttpContext.Current.Request.Form["Salary"]);
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Date_hired"]))
            {
                try
                {
                    Employee.Date_hired = Convert.ToDateTime(HttpContext.Current.Request.Form["Date_hired"]);
                }
                catch (Exception)
                {
                    Employee.Date_hired = DateTime.Now;
                }
            }
            //if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["User_Id"]))
            //{
            //    Employee.User_ID = HttpContext.Current.Request.Form["User_Id"];
            //}
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Job"]))
            {
                Employee.Job = (JobType)Convert.ToInt32(HttpContext.Current.Request.Form["Job"]);
            }
            #endregion
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (file!=null) 
                {
                    ImageUploaderService imageUploaderService = new ImageUploaderService(file, Directories.Empolyees);
                    Employee.Image = imageUploaderService.GetImageName();
                    _EmpolyeeAppService.SaveNewEmployee(Employee);
                    imageUploaderService.SaveImage();
                    ImageUploaderService.RecreateFolder(Directories.Temp);
                }
                else 
                {
                    Employee.Image = "defualt.jpg";
                    _EmpolyeeAppService.SaveNewEmployee(Employee);
                }
                return CreatedAtRoute("DefaultApi", new { id = Employee.Id }, Employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Employee/5
        public IHttpActionResult Put(int id)//to receive file use httpcontext
        {
            var Employee = new Employee();
            Employee.Id = id;
            ImageUploaderService imageUploaderService = null;
            var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;

            #region FillManually From HttpContext
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Name"]))
            {
                Employee.Name = HttpContext.Current.Request.Form["Name"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["SSN"]))
            {
                Employee.SSN = HttpContext.Current.Request.Form["SSN"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Address"]))
            {
                Employee.Address = HttpContext.Current.Request.Form["Address"];
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Salary"]))
            {
                Employee.Salary = Convert.ToDouble(HttpContext.Current.Request.Form["Salary"]);
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Date_hired"]))
            {
                try
                {
                    Employee.Date_hired = Convert.ToDateTime(HttpContext.Current.Request.Form["Date_hired"]);
                }
                catch (Exception)
                {
                    Employee.Date_hired = DateTime.Now;
                }
            }
            //if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["User_Id"]))
            //{
            //    Employee.User_ID = HttpContext.Current.Request.Form["User_Id"];
            //}
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Job"]))
            {
                Employee.Job = (JobType)Convert.ToInt32(HttpContext.Current.Request.Form["Job"]);
            }
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["Image"]))
            {
                Employee.Image = HttpContext.Current.Request.Form["Image"];
            }
            #endregion

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (file != null)
                {
                    imageUploaderService = new ImageUploaderService(file, Directories.Empolyees);
                    Employee.Image = imageUploaderService.GetImageName();
                }
                _EmpolyeeAppService.UpdateEmployee(Employee);
                imageUploaderService?.SaveImage();
                return Ok("Success Update");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Employee/5
        public IHttpActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string imageName = _EmpolyeeAppService.GetEmployee(id).Image;
                _EmpolyeeAppService.DeleteEmployee(id);
                if (imageName != "default.jpg")
                    ImageUploaderService.DeleteImage(imageName, Directories.Empolyees);
                return StatusCode(HttpStatusCode.NoContent);//204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
