using RPSAlgorithm.Util;
using System.Collections.Generic;

namespace RPSAlgorithm.Services
{
    public class RPSSolver
    {
        protected Rules rules;

        public RPSSolver()
        {
            rules = new Rules();
        }

        /// <summary>
        /// Proccess the championship and returns the first and second place
        /// </summary>
        /// <param name="championship"></param>
        /// <returns></returns>
        public CompetitorData[] Solve(List<CompetitorData> championship)
        {
            CompetitorData[] result = new CompetitorData[2]; //this will contain the first and second place
            try
            {
                if (championship != null)
                {
                    int playerA_index = 0, playerB_index = 1;
                    while (championship.Count > 1) //Always the last player is the winner
                    {
                        if (playerB_index >= championship.Count)
                        {
                            //The last playerB was the last in the list
                            //The indexes now point the first elementsin the list and runs the second round
                            playerA_index = 0;
                            playerB_index = 1;
                        }
                        CompetitorData playerA = championship[playerA_index++],
                                       playerB = championship[playerB_index++];
                        bool playerAWins = rules.GetRuleResult(playerA.Strategy, playerB.Strategy);
                        if (playerAWins) //player A wins
                            championship.Remove(playerB);
                        else //player B wins
                            championship.Remove(playerA);
                        if (championship.Count == 1) //The championship has ended up
                            SetWinners(result, playerAWins, playerA, playerB);
                    }
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Builds the result object with place 1 and place 2
        /// </summary>
        /// <param name="result"></param>
        /// <param name="playerAWins"></param>
        /// <param name="playerA"></param>
        /// <param name="playerB"></param>
        private void SetWinners(CompetitorData[] result, bool playerAWins, CompetitorData playerA, CompetitorData playerB)
        {
            try
            {
                if (playerAWins)
                {
                    result[0] = playerA;
                    result[1] = playerB;
                }
                else
                {
                    result[0] = playerB;
                    result[1] = playerA;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
