using RPSClientApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Web.Mvc;

namespace RPSClientApp.Controllers
{
    public class SamplesController : Controller
    {
        // GET: Samples
        [HttpGet]
        public ActionResult SampleIndex()
        {
            SamplesModel model = new SamplesModel();
            model.Samples = new List<string>
            {
                "[[[\"Matt\",\"S\"][\"Peter\",\"R\"]],[[\"Mary\",\"S\"][\"John\",\"P\"]]]",
                "[[[\"Matt\",\"S\"][\"Peter\",\"R\"]],[[\"Mary\",\"S\"][\"Paul\",\"P\"]]]",
                "[[[\"Matt\",\"S\"][\"Peter\",\"R\"]],[[\"Julie\",\"S\"][\"John\",\"P\"]]]",
                "[[[\"Matt\",\"S\"][\"Mark\",\"R\"]],[[\"Mary\",\"S\"][\"John\",\"P\"]]]"
            };
            return View(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DownloadSample(string sample)
        {
            byte[] bytesArray = Encoding.ASCII.GetBytes(sample);
            var stream = new MemoryStream(bytesArray);
            return File(stream, "text/plain", "sample.txt");
        }
    }
}