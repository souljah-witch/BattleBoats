using System.Runtime.Intrinsics.Arm;
namespace battleboats
{
    class Program
    {
        //static vars for checking who wins the game later on
        //Code is kind of laid out wierd, but it works
        //A lot of unneeded WriteLine(), just makes it easier to read when executed
        //big lines of dashes are just to seperate main blocks of code, again easier for me to read
        static int playerscore = 0;
        static int computerscore = 0;
        static void Main(string[] args)
        {
            Menu();
        }
//--------------------------------------------------------------------------------------------
        static void Menu()
        {
            //giving option for starting
            Console.WriteLine("Welcome to Battle Boats!");
            Console.WriteLine("\n");
            Console.WriteLine("Would you like to:" + "\n" +
                " play a new game, read the instructions or quit the game (1, 2, 3)? ");
            Console.WriteLine("\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            //if statements 1 - 4 for game options, calls seperate subroutines within file
            if (choice == 1)
            {
                newgame();
            }
            else if (choice == 2)
            {
                instructions();
                newgame();
            }
            else if (choice == 3)
            {
                quit();
            }
            else
            {
                Console.WriteLine("Invalid Input");
                Menu();
            }
//--------------------------------------------------------------------------------------------
            static void instructions()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;           
                Console.WriteLine(" ");
                Console.WriteLine("Instructions");
                Console.WriteLine("1. You will be prompted to place 5 boats on your grid, which is 8x8, please do not try and enter boats outside of this index.");
                Console.WriteLine("2. Then you will be prompted to guess where the enemies boat is, again this is limited at 8x8 so please do not go outside of these bounds.");
                Console.WriteLine("3. Each turn will be ended after you enter said guess, and the game will end once either you or the computer guess each boat locations (at 5 hits)");
                Console.WriteLine("4. Either a 'H' or a 'M' will be placed on your target tracker after each go, indicating where you have guessed previously.");
                Console.WriteLine("5. Your Fleet Grid will be displayed after the computer takes its turn, again with the same symbols as your target tracker, indicating where the computer has guessed.");
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.White;
            }
//--------------------------------------------------------------------------------------------
            static void quit()
            {
                Console.WriteLine("Goodbye.");
                System.Environment.Exit(0);
            }
//--------------------------------------------------------------------------------------------
            static void newgame()
            {
                //declaring both computer and player grids at the start, for use later
                char[,] playergrid = new char[8, 8];
                char[,] computergrid = new char[8, 8];
                Console.WriteLine("\n");
                Console.WriteLine("Welcome to your new game.");
                Console.WriteLine("\n");
                Console.WriteLine("Your Fleet Grid will always be displayed in yellow and your Target Tracker in Magenta.");
                Console.WriteLine("\n");
                Console.WriteLine("This is your blank Fleet Grid: ");
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("F   A   B   C   D   E   F   G   H  ");
                Console.WriteLine(" ");
                //printing out the player's blank fleet grid
                for (int row = 0; row < playergrid.GetLength(0); row++)
                {
                    Console.WriteLine();
                    Console.Write(row + 1);

                    for (int col = 0; col < playergrid.GetLength(1); col++)
                    {
                        playergrid[row, col] = 'X';
                        Console.Write("   " + playergrid[row, col]);
                        
                    }
                    Console.WriteLine();                   
                }
                Console.ForegroundColor = ConsoleColor.White;
//--------------------------------------------------------------------------------------------
                //taking co-ords from user
                Console.WriteLine("\n");
                Console.WriteLine("You have 5 Boat locations, enter them now. ");
                //variable to check how many boats are placed by the player, limited at 5
                int boatcoordcheck = 0;
                while (boatcoordcheck < 5)
                {
                    //taking input from player and correct for the different indexed arrays by -1
                    Console.WriteLine("Enter x co-ordinate: ");
                    int x = Convert.ToInt32(Console.ReadLine());
                    x = x - 1;
                    Console.WriteLine("Enter y co-ordinate: ");
                    int y = Convert.ToInt32(Console.ReadLine());
                    y = y - 1;
                    //checking if a boat is already at location
                    if (playergrid[x, y] == 'B')
                    {
                        Console.WriteLine("There is already a boat placed there");
                    }
                    else
                    {
                        //replacing index on player grid with a boat if not already there
                        playergrid[x, y] = 'B';
                        //incrementing check var by one
                        boatcoordcheck++;
                    }
                }
//--------------------------------------------------------------------------------------------
                //reprinting grid with boats now in place
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine("\n");
                Console.WriteLine("Your updated Fleet grid is here, with boats now in place.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" ");
                Console.WriteLine("F   A   B   C   D   E   F   G   H  ");
                Console.WriteLine(" ");
                for (int row = 0; row < playergrid.GetLength(0); row++)
                {
                    Console.WriteLine(" ");
                    Console.Write(row + 1);
                    for (int col = 0; col < playergrid.GetLength(1); col++)
                    {
                        Console.Write("   " + playergrid[row, col]);
                    }
                    Console.WriteLine();
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n");
                Console.WriteLine("-----------------------------------------------------");
//--------------------------------------------------------------------------------------------
                //setting the computer grid ready to have boats replaced
                for (int row = 0; row < computergrid.GetLength(0); row++)
                {
                    for (int col = 0; col < computergrid.GetLength(1); col++)
                    {
                        computergrid[row, col] = 'X';
                    }
                }
                //Declaring random number for computer boat locations
                Random rnd = new Random();
                //Generating random numbers for boat locations, placing them in location
                int compcoordcheck = 0;
                while (compcoordcheck < 6)
                {
                    //using randomly generated numbers to allow for unbiased boat location
                    int randomX = rnd.Next(0, 8);
                    int randomY = rnd.Next(0, 8);
                    if (computergrid[randomX, randomY] == 'B')
                    {
                        Console.WriteLine("There is already a boat placed there");
                    }
                    else
                    {
                        //replacing index on computer grid with a boat if not already there
                        computergrid[randomX, randomY] = 'B';
                        //incrementing check var by one
                        compcoordcheck++;
                    }                                   
                }
//---------------------------------------------------------------------------------------------   
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Your blank target tracker: ");
//--------------------------------------------------------------------------------------------
                //Generating a blank target tracker
                char[,] targettracker = new char[8, 8];
                Console.WriteLine("\n");
                Console.WriteLine("T   A   B   C   D   E   F   G   H  ");
                Console.WriteLine(" ");
                for (int row = 0; row < targettracker.GetLength(0); row++)
                {
                    Console.WriteLine(" ");
                    Console.Write(row + 1);
                    for (int col = 0; col < targettracker.GetLength(1); col++)
                    {
                        targettracker[row, col] = 'X';
                        Console.Write("   " + targettracker[row, col]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(" ");
                Console.WriteLine("-----------------------------------------------------");
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("The game begins now.");
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.White;
//--------------------------------------------------------------------------------------------
                //Allowing the player to take their turn
                //while loop to decide who has one once static var reaches 5 in count
                while (playerscore < 5 || computerscore < 5)
                {
//--------------------------------------------------------------------------------------------
                    //taking playing input as guess for boat location, again correcting for the different indexed arrays in place by removing 1
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine("\n");
                    Console.WriteLine("Please enter your guess for the X Co-Ordinate here: ");
                    int playerGuessX = Convert.ToInt32(Console.ReadLine());
                    playerGuessX--;
                    Console.WriteLine("\n");
                    Console.WriteLine("Please enter your guess for the Y Co-Ordinate here: ");
                    int playerGuessY = Convert.ToInt32(Console.ReadLine());
                    playerGuessY--;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
//--------------------------------------------------------------------------------------------
                    //if user enters a co-ordinate already selected previously, return error and rerun co-ordinate checking code
                    if (targettracker[playerGuessX, playerGuessY] == 'M')
                        Console.WriteLine("You have already selected that co-ordinate");
                        
//--------------------------------------------------------------------------------------------
                    if (computergrid[playerGuessY, playerGuessX ] == 'B')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You have hit a boat!");
                        Console.WriteLine();
                        //if player guess matches the boat index in computergrid[], replace index on target tracker with H
                        targettracker[playerGuessY, playerGuessX] = 'H';
                        //incrementing playerscore to stop the game once all boats are hit
                        playerscore++;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
//--------------------------------------------------------------------------------------------
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You missed!");
                        Console.WriteLine();
                        //if player guess does not matche the boat index in computergrid[], replace index on target tracker with M
                        targettracker[playerGuessY, playerGuessX] = 'M';
                        Console.ForegroundColor = ConsoleColor.White;
                    }
//--------------------------------------------------------------------------------------------
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    //printing target tracker after players turn so they can see progress
                    Console.WriteLine("Your Target tracker is displayed here: ");
                    Console.WriteLine("\n");
                    Console.WriteLine("T   A   B   C   D   E   F   G   H  ");
                    Console.WriteLine();
                    for (int row = 0; row < targettracker.GetLength(0); row++)
                    {
                        Console.WriteLine(" ");
                        Console.Write(row + 1);
                        for (int col = 0; col < targettracker.GetLength(1); col++)
                        {
                            Console.Write("   " + targettracker[row, col]);
                        }
                        Console.WriteLine();
                    }
//--------------------------------------------------------------------------------------------
                    Console.WriteLine("\n");
                    Console.WriteLine("-----------------------------------------------------");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("It is the Computer's turn now");
                    //generating random numbers for the computer to 'guess' boat locations
                    Random rnd2 = new Random();
                    int computerX = rnd2.Next(0, 8);
                    int computerY = rnd2.Next(0, 8);
//--------------------------------------------------------------------------------------------
                    if (playergrid[computerY ,computerX] == 'B')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The computer has hit your ship!");
                        //if computer guess matches the boat index in playergrid[], replace index on player's fleet grid with H
                        playergrid[computerY, computerX] = 'H';
                        computerscore++;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
//--------------------------------------------------------------------------------------------
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The computer missed!");
                        //if computer guess does not match the boat index in playergrid[], replace index on player's fleet grid with M
                        playergrid[computerY, computerX] = 'M';
                        Console.ForegroundColor = ConsoleColor.White;
                    }
//--------------------------------------------------------------------------------------------
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    //printing out fleet grid after each computer turn so player can see how close the computer is
                    Console.WriteLine("Your Fleet Grid is displayed here: ");
                    Console.WriteLine("\n");
                    Console.WriteLine("F   A   B   C   D   E   F   G   H  ");
                    Console.WriteLine(" ");
                    for (int row = 0; row < playergrid.GetLength(0); row++)
                    {
                        Console.WriteLine(" ");
                        Console.Write(row + 1);
                        for (int col = 0; col < playergrid.GetLength(1); col++)
                        {
                            Console.Write("   " + playergrid[row, col]);
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine("\n");
//--------------------------------------------------------------------------------------------
                    Console.ForegroundColor = ConsoleColor.White;
                    //ending the game based on value held in either playerscore/computerscore
                    //first one to reach 5 is ran
                    if (playerscore == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("The player has Won, Congrats.");
                        Console.WriteLine("\n");
                        //giving option for the player to carry on, or close the file
                        Console.WriteLine("Do you wish to play a new game, or exit(1, 2)? ");
                        Console.WriteLine("\n");
                        int finalchoice = Convert.ToInt32(Console.ReadLine());
                        if (finalchoice == 1)
                        {
                            newgame();
                        }
                        else
                        {
                            quit();
                        }
                    }
//--------------------------------------------------------------------------------------------
                    if (computerscore == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("The Computer has Won, Unlucky.");
                        Console.WriteLine("\n");
                        Console.WriteLine("Do you wish to play a new game, or exit(1, 2)? ");
                        Console.WriteLine("\n");
                        int finalchoice = Convert.ToInt32(Console.ReadLine());
                        if (finalchoice == 1)
                        {
                            newgame();
                        }
                        else
                        {
                            Console.WriteLine("\n");
                            quit();
                        }
//--------------------------------------------------------------------------------------------
//this took way too long sir please give me a good grade i beg i dont wanna do the write up thing nor comment it 
//i also really dont wanna do error checking properly
//also theres no error checking for different data types entered because i dont actually know how to do that
//first coding project i actually put effort into please
//i can pay

                    }
                }            
            }
        }
    }
}