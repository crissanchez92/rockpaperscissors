using RPSAlgorithm.Util;
using System.Collections.Generic;

namespace RPSAlgorithm.Services
{
    public class Parser
    {

        /// <summary>
        /// Validates if the number is 2^n
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        protected bool IsPowerOfTwo(int number)
        {
            try
            {
                double remainder = number;
                while (remainder > 1)
                {
                    if (remainder % 2 != 0)
                        return false;
                    remainder /= 2;
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converts a string into a List of competitors
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CompetitorData> StringToCompetitors(string input, out string errorMessage)
        {
            try
            {
                List<CompetitorData> result = new List<CompetitorData>();
                int bracketsCount = 0, playerCount = 0, index = 0;
                string name = string.Empty;
                errorMessage = string.Empty;
                StrategyEnum strategy = StrategyEnum.Invalid;
                if (!string.IsNullOrEmpty(input))
                {
                    while(index < input.Length)
                    {
                        if (input[index] == '[')
                            bracketsCount++;
                        else if (input[index] == ']')
                            bracketsCount--;
                        index++;
                        if (bracketsCount > 1)
                        {
                            if (input[index] == '\"' && playerCount == 0)
                            {
                                index++; //Ignores the opening " symbol
                                while (input[index] != '\"') //Name field
                                    name = string.Concat(name, input[index++]);
                                if (!string.IsNullOrEmpty(name)) //Valid name field
                                    playerCount++;
                                else
                                {
                                    errorMessage = "Player name field error";//Throw exception, incomplete player field
                                    return result;
                                }
                                index++; //Ignores the closing " symbol
                            }
                            if(input[index] == '\"' && playerCount == 1)
                            {
                                //Strategy value
                                index++;//Ignores the opening " symbol
                                strategy = ParseStrategy(input[index++]);
                                if (strategy == StrategyEnum.Invalid)
                                {
                                    errorMessage = string.Format("Invalid strategy {0}", input[index - 1]);//Throw exception, invalid strategy field
                                    return result;
                                }
                                else
                                {
                                    result.Add(new CompetitorData { Name = name, Strategy = strategy });
                                    strategy = StrategyEnum.Invalid;
                                    name = string.Empty;
                                    playerCount = 0;
                                }
                            }
                        }
                    }
                    if (bracketsCount > 0)
                        errorMessage = "Brackets mismatch";//Throw exception, brackets mismatch  
                }
                if (!IsPowerOfTwo(result.Count))
                    errorMessage = "The amount of competitors is not valid, must be 2^n";
                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Tries to parse the strategy into a valid one
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private StrategyEnum ParseStrategy(char input)
        {
            switch (input)
            {
                case 'R':
                    return StrategyEnum.R;
                case 'P':
                    return StrategyEnum.P;
                case 'S':
                    return StrategyEnum.S;
                default:
                    return StrategyEnum.Invalid;
            }
        }
    }
}
