using System;
using System.Collections.Generic;

namespace ATM
{
	class Program
	{
		static void Main(string[] args)
		{
			int[] denominations = { 10, 50, 100 };
			int[] payouts = { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

			foreach (int payout in payouts)
			{
				Console.WriteLine($"For {payout} EUR, the available payout denominations would be:");

				List<List<int>> combinations = GetCombinations(payout, denominations);

				if (combinations.Count == 0)
				{
					Console.WriteLine("No possible combinations.");
				}
				else
				{
					foreach (List<int> combination in combinations)
					{
						Dictionary<int, int> denominationCount = CountDenominations(combination);
						foreach (var pair in denominationCount)
						{
							Console.Write($"{pair.Value} x {pair.Key} EUR ");
						}
						Console.WriteLine();
					}
				}

				Console.WriteLine();
			}
		}

		static List<List<int>> GetCombinations(int amount, int[] denominations)
		{
			List<List<int>> result = new List<List<int>>();
			GetCombinationsRecursive(amount, denominations, 0, new List<int>(), result);
			return result;
		}

		static void GetCombinationsRecursive(int amount, int[] denominations, int index, List<int> currentCombination, List<List<int>> result)
		{
			if (amount == 0)
			{
				result.Add(new List<int>(currentCombination));
				return;
			}

			if (amount < 0 || index == denominations.Length)
				return;

			// Include current denomination
			currentCombination.Add(denominations[index]);
			GetCombinationsRecursive(amount - denominations[index], denominations, index, currentCombination, result);
			currentCombination.RemoveAt(currentCombination.Count - 1);

			// Skip current denomination
			GetCombinationsRecursive(amount, denominations, index + 1, currentCombination, result);
		}

		static Dictionary<int, int> CountDenominations(List<int> denominations)
		{
			Dictionary<int, int> count = new Dictionary<int, int>();
			foreach (int denomination in denominations)
			{
				if (count.ContainsKey(denomination))
					count[denomination]++;
				else
					count[denomination] = 1;
			}
			return count;
		}
	}
}
