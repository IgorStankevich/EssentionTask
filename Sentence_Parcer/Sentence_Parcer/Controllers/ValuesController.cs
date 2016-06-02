using Sentence_Parcer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Sentence_Parcer.Controllers
{
    public class ValuesController : ApiController
    {
        Parcer _parcer = new Parcer();
                
        // POST api/values
        public string Post([FromBody]string text)
        {
            _parcer.BindToModel(text);
            string xml;
            xml = _parcer.ToXML();
            return xml;
        }

         //PUT api/values/5
        public void Put( [FromBody]string value)
        {
        }


        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
