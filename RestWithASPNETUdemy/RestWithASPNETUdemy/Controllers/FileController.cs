using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business.Implementation;

namespace RestWithASPNETUdemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Authorize("Bearer")]
    public class FileController : ControllerBase
    {
        private IFileBusiness _fileBusiness; 
        public FileController(IFileBusiness fileBusiness)
        {
            this._fileBusiness = fileBusiness;
        }

        [HttpGet]
        public IActionResult GetPDFFile()
        {
            byte[] buffer = this._fileBusiness.GetPDFFile();
            if (buffer != null)
            {
                this.HttpContext.Response.ContentType = "application/pdf";
                this.HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                this.HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }



    }
}