using RPSClientApp.Models;
using System.Web.Mvc;
using WebApiClient.HttpClientUtil;
using RPSAlgorithm.Services;
using RPSAlgorithm.Util;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Text;

namespace RPSClientApp.Controllers
{
    public class ChampionshipController : Controller
    {
        // GET: Championship
        [HttpGet]
        public ActionResult ChampForm()
        {
            ChampionshipModel model = new ChampionshipModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadFile(HttpPostedFileBase file)
        {
            ChampionshipModel model = new ChampionshipModel();
            if (file.ContentLength > 0)
            {
                byte[] fileContentBuff = new byte[file.ContentLength];
                file.InputStream.Read(fileContentBuff, 0, file.ContentLength);
                StringBuilder builder = new StringBuilder();
                foreach (byte character in fileContentBuff)
                    builder.Append((char)character);
                model.Input = builder.ToString();
            }
            return await ExecuteChampionship(model);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Execute(ChampionshipModel model)
        {
            return await ExecuteChampionship(model);
        }

        private async Task<ActionResult> ExecuteChampionship(ChampionshipModel model)
        {
            RPSSolver solver = new RPSSolver();
            Parser parser = new Parser();
            CompetitorData secondPlace = new CompetitorData();
            string errorMessage;
            bool result = false;
            var championship = parser.StringToCompetitors(model.Input, out errorMessage);
            if (string.IsNullOrEmpty(errorMessage))
            {
                CompetitorData[] finalists = solver.Solve(championship);
                model.Winner = finalists[0].Name;
                model.Strategy = finalists[0].Strategy.ToString();
                secondPlace = finalists[1];
                ApiClient client = new ApiClient();
                result = await client.SaveResult(model.Winner, secondPlace.Name);
            }
            else
                model.ErrorMessage = errorMessage;

            return View("Execute", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Reset()
        {
            ApiClient client = new ApiClient();
            bool result = await client.Reset();
            ViewBag.Message = result ? "System was started over" : "An error occurred while trying to start over";
            return View();
        }
    }
}