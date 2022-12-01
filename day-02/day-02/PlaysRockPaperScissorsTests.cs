using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;

namespace day_02
{
   internal class Game
   {
      public string TheirHand { get; set; }
      public string MyHand { get; set; }
   }

   [TestClass]
   public class PlaysRockPaperScissorsTests
   {
      [TestMethod]
      public void ReadGame_Test1_ReadsTwoHands()
      {
         var subject = new PlaysRockPaperScissors();

         Game result = subject.ReadGame( "A Y" );

         result.TheirHand.Should().Be( "A" );
         result.MyHand.Should().Be( "Y" );
      }

      [DataTestMethod]
      [DataRow( "A Y", "X" )]
      [DataRow( "B X", "X" )]
      [DataRow( "C Z", "X" )]
      public void ReadRiggedGame_Scenario_ReadsRiggedHands(string input, string expected)
      {
         var subject = new PlaysRockPaperScissors();
         Game result = subject.ReadRiggedGame( input );
         
         result.MyHand.Should().Be( expected );
      }


      [DataTestMethod]
      [DataRow( "A Y", 8 )]
      [DataRow( "B X", 1 )]
      [DataRow( "C Z", 6 )]      
      public void GetScore_Scenario_GetsScore(string line, int expected)
      {
         var subject = new PlaysRockPaperScissors();
         var game = subject.ReadGame("A Y");

         int result = subject.GetScore(game);

         result.Should().Be( 8 );
      }

      [DataTestMethod]
      [DataRow( "X", 1 )]
      [DataRow( "Y", 2 )]
      [DataRow( "Z", 3 )]
      public void GetScoreForMyHand_Scenario_GetsScore(string shape, int expected)
      {
         var subject = new PlaysRockPaperScissors();

         int result = subject.GetScoreForMyHand(shape);

         result.Should().Be( expected );
      }

      [DataTestMethod]
      [DataRow( "A", 1 )]
      [DataRow( "B", 2 )]
      [DataRow( "C", 3 )]
      public void GetScoreForTheirHand_Scenario_GetsScore( string shape, int expected )
      {
         var subject = new PlaysRockPaperScissors();

         int result = subject.GetScoreForTheirHand(shape);

         result.Should().Be( expected );
      }

      [DataTestMethod]
      [DataRow( "A Y", 6 )]
      [DataRow( "B X", 0 )]
      [DataRow( "C Z", 3 )]           
      public void GetScoreForGame_Scenario_GetsScore(string line, int expected )
      {
         var subject = new PlaysRockPaperScissors();
         var game = subject.ReadGame(line);

         int result = subject.GetScoreForGame(game);

         result.Should().Be( expected );
      }

      [TestMethod]
      public void GetScoreForGame_RealInput_GetsTotalScore()
      {
         int result = 0;
         var lines = File.ReadAllLines(@"C:\Projects\Git\advent-of-code-2022\day-02\real-input.txt");
         var subject = new PlaysRockPaperScissors();
         foreach(var line in lines)
         {
            var game = subject.ReadGame(line);
            result += subject.GetScore( game );
         }

         result.Should().Be( 13484 );
      }

      [TestMethod]
      public void GetRiggedScoreForGame_RealInput_GetsTotalScore()
      {
         int result = 0;
         var lines = File.ReadAllLines(@"C:\Projects\Git\advent-of-code-2022\day-02\real-input.txt");
         var subject = new PlaysRockPaperScissors();
         foreach ( var line in lines )
         {
            var game = subject.ReadRiggedGame(line);
            result += subject.GetScore( game );
         }

         result.Should().Be( 13433 );
      }




   }

   internal class PlaysRockPaperScissors
   {
      public PlaysRockPaperScissors()
      {
      }

      internal int GetScore( Game game ) => GetScoreForMyHand( game.MyHand ) + GetScoreForGame( game );      

      internal int GetScoreForMyHand( string shape ) => (int)shape[0] - 87;

      internal int GetScoreForTheirHand( string shape ) => (int)shape[0] - 64;

      internal Game ReadGame( string line )
      {
         var splitLine = line.Split(' ');
         var result = new Game();
         result.TheirHand = splitLine[0];
         result.MyHand = splitLine[1];
         return result;
      }

      internal Game ReadRiggedGame( string line )
      {
         var unriggedGame = ReadGame(line);
         var result = new Game();
         result.TheirHand = unriggedGame.TheirHand;
         result.MyHand = GetRiggedHand( unriggedGame );
         return result;
      }

      private string GetRiggedHand( Game game )
      {
         if(game.MyHand == "X") //loss
         {
            switch ( game.TheirHand )
            {
               case "A":
                  return "Z";
               case "B":
                  return "X";
               case "C":
                  return "Y";
               default:
                  break;
            }
         }
         if ( game.MyHand == "Y" ) //tie
         {
            var newChar = (char)( (int)game.TheirHand[0] + 23 );
            return newChar.ToString();
         }
         if(game.MyHand == "Z") //win
         {
            switch ( game.TheirHand )
            {
               case "A":
                  return "Y";
               case "B":
                  return "Z";
               case "C":
                  return "X";
               default:
                  break;
            }
         }
         throw new ArgumentException( "Shouldn't be able to get here, what's in my hand?" );
      }

      internal int GetScoreForGame( Game game )
      {
         int myHand = GetScoreForMyHand(game.MyHand);
         int theirHand = GetScoreForTheirHand(game.TheirHand);

         int difference = myHand - theirHand;


         // 2 - 1 paper beats rock 1
         // 3 - 2 scissors beats paper 1
         // 1 - 3 rock beats scissors -2 
         if ( difference == 1 || difference == -2 )
         {
            return 6;
         }

         // 1 - 2 rock loses -1
         // 2 - 3 paper loses -1
         // 3 - 1 scissors loses 2 
         if ( difference == -1 || difference == 2 )
         {
            return 0;
         }

         if ( difference == 0 )
         {
            return 3;
         }

         throw new ArgumentException( "we got a difference you didn't expect" );
      }

      
   }
}
