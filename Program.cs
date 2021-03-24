using System;
using System.Collections.Generic;
namespace csharp_game_tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] boardGame = new string[9];
            List<int> UnPlayedCases = new List<int> {0,1,2,3,4,5,6,7,8};
            Random rand = new Random();
            int userCase, iaCase;

            Console.WriteLine("***** Jeu du Morpion *****");
            Console.WriteLine("───────\n│0│1│2│\n───────\n│3│4│5│\n───────\n│6│7│8│\n───────");
            Console.Write("Entrez une case (0 à 8)\nTapez une touche pour commencer");
            Console.ReadKey(true);
            Console.Clear();
            InitializeBoard(boardGame);     
            DisplayBoard(boardGame);       

            while(true)
            {            
                // user
                userCase = GetCase("Entrez la case voulue: ");

                if(IsEmpty(boardGame, userCase, Player.PlayerOne))
                {
                    DisplayBoard(boardGame);
                    UnPlayedCases.Remove(userCase);
                }                    

                if(IsWin(boardGame).Item1 || UnPlayedCases.Count == 0)
                    break;                

                // ia
                iaCase = rand.Next(0,UnPlayedCases.Count);                

                if(IsEmpty(boardGame, UnPlayedCases[iaCase], Player.PlayerTwo))                                
                {
                    DisplayBoard(boardGame);
                    UnPlayedCases.Remove(UnPlayedCases[iaCase]);                       
                }                

                if(IsWin(boardGame).Item1 || UnPlayedCases.Count == 0)
                    break;     
            }

            if(IsWin(boardGame).Item1)
                Console.WriteLine($"{(IsWin(boardGame).Item2 == Player.PlayerOne ? "Vous avez gagné" : "Vous avez perdu")}");
            else
                Console.WriteLine($"No winner");
        }

        enum Player
        {
            NoPlayer, PlayerOne, PlayerTwo
        }

        static int GetCase(string msg, int min = 0, int max = 8)
        {
            int output = 0;

            // loop while input is not integer and between 0 and 8
            do
            {
                Console.Write(msg);
            } while (!int.TryParse(Console.ReadLine(),out output) || (output < min) || (output > max));

            return output;
        }

        static bool IsEmpty(string[] grid, int gridCase, Player player)
        {
            if(String.Compare(grid[gridCase], " ") == 0)
            {
                grid[gridCase] = player == Player.PlayerOne ? "O" : "X";
                return true;
            }
            
            return false;
        }

        static void DisplayBoard(string[] boardGame)
        {
            Console.Clear();
            Console.WriteLine("───────");
            for(int row = 0, max = boardGame.Length; row < max; row++)
            {
                if(row == 3 || row == 6)
                    Console.WriteLine("│\n───────");                
                
                Console.Write("│");
                Console.Write($"{boardGame[row]}");                
            }            
            Console.WriteLine("│\n───────");
        }

        static void InitializeBoard(string[] boardGame)
        {
            for(int i = 0, max = boardGame.Length; i < max; i++)
                boardGame[i] = " ";
        }

        static (bool, Player) IsWin(string[] boardGame)
        {
            if(boardGame[0] != " ")
            {
                if(boardGame[0] == boardGame[1] && boardGame[0] == boardGame[2])
                    return (true, boardGame[0] == "O" ? Player.PlayerOne : Player.PlayerTwo);
                if(boardGame[0] == boardGame[4] && boardGame[0] == boardGame[8])
                    return (true, boardGame[0] == "O" ? Player.PlayerOne : Player.PlayerTwo);
                if(boardGame[0] == boardGame[3] && boardGame[3] == boardGame[6])
                    return (true, boardGame[0] == "O" ? Player.PlayerOne : Player.PlayerTwo);
            }
            if(boardGame[1] != " ")
            {
                if(boardGame[1] == boardGame[4] && boardGame[1] == boardGame[7])
                    return (true, boardGame[1] == "O" ? Player.PlayerOne : Player.PlayerTwo);
            }
            if(boardGame[2] != " ")
            {
                if(boardGame[2] == boardGame[4] && boardGame[2] == boardGame[6])
                    return (true, boardGame[2] == "O" ? Player.PlayerOne : Player.PlayerTwo);  
                if(boardGame[2] == boardGame[5] && boardGame[2] == boardGame[8])
                    return (true, boardGame[2] == "O" ? Player.PlayerOne : Player.PlayerTwo);
            }
            if(boardGame[3] != " ")
            {
                if(boardGame[3] == boardGame[4] && boardGame[3] == boardGame[5])
                    return (true, boardGame[3] == "O" ? Player.PlayerOne : Player.PlayerTwo);
            }
            if(boardGame[6] != " ")
            {
                if(boardGame[6] == boardGame[7] && boardGame[6] == boardGame[8])
                    return (true, boardGame[6] == "O" ? Player.PlayerOne : Player.PlayerTwo);
            }

            return (false, Player.NoPlayer);
        }
    }
}
