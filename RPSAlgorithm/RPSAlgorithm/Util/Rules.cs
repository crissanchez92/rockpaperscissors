namespace RPSAlgorithm.Util
{
    public class Rules
    {
        //  R P S
        //R t f t
        //P t t f
        //S f t t
        public bool[,] RPSRules = new bool[3, 3];

        public Rules()
        {
            RPSRules[(int)StrategyEnum.R, (int)StrategyEnum.R] = true;
            RPSRules[(int)StrategyEnum.R, (int)StrategyEnum.P] = false;
            RPSRules[(int)StrategyEnum.R, (int)StrategyEnum.S] = true;
            RPSRules[(int)StrategyEnum.P, (int)StrategyEnum.R] = true;
            RPSRules[(int)StrategyEnum.P, (int)StrategyEnum.P] = true;
            RPSRules[(int)StrategyEnum.P, (int)StrategyEnum.S] = false;
            RPSRules[(int)StrategyEnum.S, (int)StrategyEnum.R] = false;
            RPSRules[(int)StrategyEnum.S, (int)StrategyEnum.P] = true;
            RPSRules[(int)StrategyEnum.S, (int)StrategyEnum.S] = true;
        }

        /// <summary>
        /// Verifies if compA wins against compB
        /// </summary>
        /// <param name="compA"></param>
        /// <param name="compB"></param>
        /// <returns></returns>
        public bool GetRuleResult(StrategyEnum compA, StrategyEnum compB)
        {
            try
            {
                return RPSRules[(int)compA, (int)compB];
            }
            catch
            {
                throw;
            }
        }
    }
}
