using Sentence_Parcer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sentence_Parcer.Controllers
{
    public class HomeController : Controller
    {
        private Parcer _parcer;

        public HomeController()
        {
            _parcer = new Parcer();
        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ParceToXMl()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ParceToCsv()
        {
            return View(new TextViewModel());
        }
        [HttpGet]
        public ActionResult ParceToCsvAndXml()
        {
            return View(new TextViewModel());
        }

        [HttpPost]
        public ActionResult ParceToXml(TextViewModel input)
        {
            if (input.Text == null)
            {
                RedirectToAction("Index");
            }
            else
            {
                _parcer.BindToModel(input.Text);
                _parcer.WriteToXmlDoc();
                input.Xml = _parcer.ToXML();
            }
            return Content(input.Xml, "text/xml");
            //return View("Index");
        }

        public ActionResult GetXml(TextViewModel input)
        {

            input.Xml = _parcer.ToXML();
            return View("GetXml");
        }

        [HttpPost]
        public ActionResult ParceToCsv(TextViewModel input)
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = pathDesktop + "\\mycsvfile.csv";
            if (input.Text == null)
            {
                RedirectToAction("Index");
            }
            else
            {
                _parcer.BindToModel(input.Text);
                _parcer.WriteToCSV();
            }
            System.Diagnostics.Process.Start("http://" + filePath);
            return View("Index");
        }

        [HttpPost]
        public ActionResult ParceToCsvAndXml(TextViewModel input)
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = pathDesktop + "\\mycsvfile.csv";
            if (input.Text == null)
            {
                RedirectToAction("Index");
            }
            else
            {
                _parcer.BindToModel(input.Text);
                _parcer.WriteToCSV();
                _parcer.WriteToXmlDoc();
                System.Diagnostics.Process.Start("http://" + filePath);
                return Content(input.Xml, "text/xml");
            }
            return View("Index");
        }

        //try to get csv rows. Dont use its wrong.
        //public string GetCSV()
        //{
        //    string sentences;
        //    var sb = new StringBuilder();
        //    foreach (var item in _parcer.CSVrows)
        //    {
        //        sb.Append(item);
        //        sb.Append("\n");
        //    }
        //    sentences = sb.ToString();
        //    return sentences;
        //}
    }
}
