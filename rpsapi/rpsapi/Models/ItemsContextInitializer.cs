using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace rpsapi.Models
{
    public class ItemsContextInitializer : DropCreateDatabaseIfModelChanges<RPSContext>
    {
        protected override void Seed(RPSContext context)
        {
            var competitors = new List<Competitor>();

            competitors.Add(new Competitor { Name = "test", Points = 1 });

            competitors.ForEach(p => context.Competitors.Add(p));
            context.SaveChanges();
        }
    }
}