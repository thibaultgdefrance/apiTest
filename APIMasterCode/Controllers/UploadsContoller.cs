using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace APIMasterCode.Controllers
{
    public class UploadsContoller:ApiController
    {
        [Route("api/files/upload")]
        public async Task<string> Post()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}