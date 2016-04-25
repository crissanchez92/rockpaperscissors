using RPSAlgorithm.Services;
using RPSAlgorithm.Util;
using rpsapi.Models;
using System.Linq;
using System.Web.Http;

namespace rpsapi.Controllers
{
    [RoutePrefix("api/championship")]
    public class ChampionshipController : ApiController
    {
        private RPSContext db = new RPSContext();
        private static int pointsFirst = 3;
        private static int pointsSecond = 1;
        private static int countDefault = 10;

        [Route("top")]
        [HttpPost]
        public IHttpActionResult GetTop(int? count = null)
        {
            return Json(new { players = db.Competitors.Take(count ?? countDefault).OrderByDescending(c => c.Points).Select(c => c.Name) });
        }

        [Route("result")]
        [HttpPost]
        public IHttpActionResult PostResult(string first, string second)
        {
            return SaveWinnersOnDB(first, second);
        }

        [Route("reset")]
        [HttpDelete]
        public IHttpActionResult Reset()
        {
            db.Competitors.RemoveRange(db.Competitors);
            db.SaveChanges();
            return Ok();
        }

        [Route("new")]
        [HttpPost]
        public IHttpActionResult NewMatch(NewChampionshipModel model)
        {
            RPSSolver solver = new RPSSolver();
            Parser parser = new Parser();
            CompetitorData secondPlace = new CompetitorData();
            string errorMessage;
            var championship = parser.StringToCompetitors(model.Data, out errorMessage);
            if (string.IsNullOrEmpty(errorMessage))
            {
                CompetitorData[] finalists = solver.Solve(championship);
                return Ok(new { winner = new string[] { finalists[0].Name, finalists[0].Strategy.ToString() } });
            }
            return Json(new { status = "Error", message = errorMessage });
        }

        private IHttpActionResult SaveWinnersOnDB(string first, string second)
        {
            Competitor firstDb = db.Competitors.Where(c => string.Compare(c.Name, first, true) == 0).FirstOrDefault(),
                               seconDb = db.Competitors.Where(c => string.Compare(c.Name, second, true) == 0).FirstOrDefault();
            if (firstDb != null)
                firstDb.Points += pointsFirst;
            else
                db.Competitors.Add(new Competitor { Name = first, Points = pointsFirst });
            if (seconDb != null)
                seconDb.Points += pointsSecond;
            else
                db.Competitors.Add(new Competitor { Name = second, Points = pointsSecond });
            db.SaveChanges();
            return Json(new { status = "success" });
        }
    }
}
