using System;
using System.Collections.Generic;
namespace csharp_game_tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            CaseStates[] boardGame = new CaseStates[9];
            List<int> UnPlayedCases = new List<int> {0,1,2,3,4,5,6,7,8};
            Random rand = new Random();
            int userCase, iaCase;

            Console.WriteLine("***** Jeu du Morpion *****");
            Console.WriteLine("───────\n│0│1│2│\n───────\n│3│4│5│\n───────\n│6│7│8│\n───────");
            Console.Write("Entrez une case (0 à 8)\nTapez une touche pour commencer");
            Console.ReadKey(true);
            Console.Clear();
            DisplayBoard(boardGame);       

            while(true)
            {            
                // user
                userCase = GetCase("Entrez la case voulue: ");

                if(IsEmpty(boardGame, userCase, CaseStates.Circle))
                {
                    DisplayBoard(boardGame);
                    UnPlayedCases.Remove(userCase);
                }                    

                if(IsWin(boardGame, CaseStates.Circle))
                {
                    Console.WriteLine("Vous avez gagné");
                    break;
                }

                if(UnPlayedCases.Count == 0)
                {
                    Console.WriteLine("No winner");
                    break;
                }                    

                // ia
                iaCase = rand.Next(0,UnPlayedCases.Count);                

                if(IsEmpty(boardGame, UnPlayedCases[iaCase], CaseStates.Cross))                                
                {
                    DisplayBoard(boardGame);
                    UnPlayedCases.Remove(UnPlayedCases[iaCase]);                       
                }                

                if(IsWin(boardGame, CaseStates.Cross))
                {
                    Console.WriteLine("Vous avez perdu");
                    break;     
                }
            }
        }
        
        enum CaseStates
        {
            Empty, Circle, Cross
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

        static bool IsEmpty(CaseStates[] boardGame, int gridCase, CaseStates player)
        {
            if(boardGame[gridCase] == CaseStates.Empty)
            {
                boardGame[gridCase] = player;
                return true;
            }
            
            return false;
        }

        static void DisplayBoard(CaseStates[] boardGame)
        {
            Console.Clear();
            Console.WriteLine("───────");
            for(int i = 0, max = boardGame.Length; i < max; i++)
            {
                if(i == 3 || i == 6)
                    Console.WriteLine("│\n───────");                
                
                Console.Write("│");
                switch(boardGame[i])
                {
                    case CaseStates.Empty:
                        Console.Write(" ");
                        break;
                    case CaseStates.Circle:
                        Console.Write("O");
                        break;
                    case CaseStates.Cross:
                       Console.Write("X");
                        break;
                }                              
            }            
            Console.WriteLine("│\n───────");
        }
        static bool IsWin(CaseStates[] boardGame, CaseStates player)
        {           
            // test i
            for(int i = 0, size = boardGame.Length; i < size; i+= 3)
            {
                if(boardGame[i] == player && boardGame[i+1] == player && boardGame[i+2] == player)
                    return true;
            }

            // test collumns
            for(int i = 0; i < 3; i++)
            {
                if(boardGame[i] == player && boardGame[i+3] == player && boardGame[i+6] == player)
                    return true;
            }

            // test oblique left
            if(boardGame[0] == player && boardGame[4] == player && boardGame[8] == player)
                return true;

            // test oblique right
            if(boardGame[2] == player && boardGame[4] == player && boardGame[6] == player)
                return true;

            // default
            return false;
        }
    }
}
