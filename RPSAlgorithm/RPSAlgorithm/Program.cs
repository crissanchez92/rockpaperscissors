using RPSAlgorithm.Services;
using RPSAlgorithm.Util;
using System.Collections.Generic;

namespace RPSAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser();
            string errormessage;
            var result = parser.StringToCompetitors("[[\"cris\", \"P\"], [\"Gil\", \"R\"], [\"Adri\", \"S\"]]", out errormessage);
        }
    }
}
