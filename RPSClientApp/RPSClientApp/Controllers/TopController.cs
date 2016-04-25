using RPSClientApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApiClient.HttpClientUtil;

namespace RPSClientApp.Controllers
{
    public class TopController : Controller
    {
        // GET: Top
        [HttpGet]
        public ActionResult TopForm()
        {
            TopModel model = new TopModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> GetTop(int? count)
        {
            TopModel model = new TopModel();
            ApiClient client = new ApiClient();
            List<string> players = await client.GetTop(count);
            model.Players = players;
            return View(model);
        }
    }
}