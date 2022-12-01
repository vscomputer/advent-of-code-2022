using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;
using System.Linq;

namespace day_01
{
   [TestClass]
   public class CountsCaloriesTests
   {
      [TestMethod]
      public void GetElves_TestInput_ReturnsFiveElves()
      {
         var subject = new CountsCalories();

         List<int> result = CountsCalories.GetElves(@"C:\Projects\Homework\advent-of-code-2022\day-01\test-input.txt");

         result.Count.Should().Be( 5 );
      }

      [TestMethod]
      public void GetElves_TestInput_FirstElfHas6000()
      {
         var subject = new CountsCalories();

         List<int> result = CountsCalories.GetElves(@"C:\Projects\Homework\advent-of-code-2022\day-01\test-input.txt");

         result[0].Should().Be( 6000 );
      }

      [TestMethod]
      public void GetElves_TestInput_LastElfHas10000()
      {
         var subject = new CountsCalories();

         List<int> result = CountsCalories.GetElves(@"C:\Projects\Homework\advent-of-code-2022\day-01\test-input.txt");

         result[4].Should().Be( 10000 );
      }

      [TestMethod]
      public void GetHighestCalorieElf_TestInput_Gets24000()
      {
         var subject = new CountsCalories();

         List<int> elves = CountsCalories.GetElves(@"C:\Projects\Homework\advent-of-code-2022\day-01\test-input.txt");
         int result = subject.GetHighestCalorieElf(elves);

         result.Should().Be( 24000 );
      }

      [TestMethod]
      public void GetHighestCalorieElf_RealInput_GetsTheAnswer()
      {
         var subject = new CountsCalories();

         List<int> elves = CountsCalories.GetElves(@"C:\Projects\Homework\advent-of-code-2022\day-01\real-input.txt");
         int result = subject.GetHighestCalorieElf(elves);

         result.Should().Be( 71506 );
      }

      [TestMethod]
      public void GetSumOfTopThreeElves_TestInput_Gets45000()
      {
         var subject = new CountsCalories();

         List<int> elves = CountsCalories.GetElves(@"C:\Projects\Homework\advent-of-code-2022\day-01\test-input.txt");
         int result = subject.GetSumOfTopThreeElves(elves);

         result.Should().Be( 45000 );
      }

      [TestMethod]
      public void GetSumOfTopThreeElves_RealInput_GetsTheAnswer()
      {
         var subject = new CountsCalories();

         List<int> elves = CountsCalories.GetElves(@"C:\Projects\Homework\advent-of-code-2022\day-01\real-input.txt");
         int result = subject.GetSumOfTopThreeElves(elves);

         result.Should().Be( -1 );
      }
   }

   internal class CountsCalories
   {
      public CountsCalories()
      {
      }

      internal static List<int> GetElves( string input )
      {
         var lines = File.ReadAllLines(input);
         var result = new List<int>();

         int caloriesByElf = 0;

         foreach (var line in lines)
         {            
            if(string.IsNullOrWhiteSpace(line))
            {
               result.Add( caloriesByElf );
               caloriesByElf = 0;
            }
            else
            {
               int caloriesByLine = 0;
               int.TryParse( line, out caloriesByLine );
               caloriesByElf += caloriesByLine;
            }
         }
         result.Add( caloriesByElf );

         return result;
      }

      internal int GetHighestCalorieElf( List<int> elves ) => elves.Max();
      internal int GetSumOfTopThreeElves( List<int> elves )
      {
         var sortedElves = elves.OrderByDescending(e => e).ToList();
         return sortedElves[0] + sortedElves[1] + sortedElves[2];
      }
   }
}
