using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnekGame
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int[] xPosition = new int[99];
            xPosition[0] = 30;
            int[] yPosition = new int[99];
            yPosition[0] = 20;
            int appleXDim = 10;
            int appleYDim = 10;
            bool gameIsOn = true;
            bool isWallHit = false;
            bool isAppleEaten = false;
            decimal snekSpeed = 150m;
            int applesEaten = 0;
            string snekDirection = "";
            Random random = new Random();
            
            //Start screen

            //Draw Borders
            BuildWall();

            //Draw Snek
            Console.SetCursorPosition(xPosition[0], yPosition[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((char)2);
            Console.CursorVisible = false;
            //Draw First Apple
            SetApplePosition(random, out appleXDim, out appleYDim);
            PaintApple(appleXDim, appleYDim);

            //Snek Move
            ConsoleKey command = Console.ReadKey().Key;
            ConsoleKey quit = Console.ReadKey().Key;
            Console.SetCursorPosition(10, 0);
            Console.Write("Apples Eaten: ");
            
            do
            {
                switch (command)
                {
                    case ConsoleKey.UpArrow:
                        if (snekDirection != "d")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            yPosition[0]--;
                            snekDirection = "u";
                        }
                        if (snekDirection == "d")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            yPosition[0]++;
                            snekDirection = "d";
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (snekDirection != "u")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            yPosition[0]++;
                            snekDirection = "d";
                        }
                        if(snekDirection == "u")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            yPosition[0]--;
                            snekDirection = "u";
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (snekDirection != "r")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            xPosition[0]--;
                            snekDirection = "l";
                        }
                        if (snekDirection == "r")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            xPosition[0]++;
                            snekDirection = "r";
                        }
                        break;

                    case ConsoleKey.RightArrow:
                        if (snekDirection != "l")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            xPosition[0]++;
                            snekDirection = "r";                           
                        }
                        if (snekDirection == "l")
                        {
                            Console.SetCursorPosition(xPosition[0], yPosition[0]);
                            Console.Write(' ');
                            xPosition[0]--;
                            snekDirection = "l";
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.SetCursorPosition(30, 20);
                        Console.Write("You pressed quit");
                        gameIsOn = false;
                        break;


                }
                //Paint Snek
                PaintSnek(applesEaten,xPosition,yPosition,out xPosition, out yPosition);

                //Draw Apple
                
                if (isAppleEaten)
                {
                    //Define apple position
                    SetApplePosition(random, out appleXDim, out appleYDim);
                    //Draw Apple
                    PaintApple(appleXDim, appleYDim);
                    //Increase Apple Score
                    applesEaten++;
                    //Change Snek Speed
                    snekSpeed *=  0.950m;
                    //isAppleEaten = false;
                    
                    
                }
                //Detect when Apple was eaten
                isAppleEaten = AppleWasEaten(xPosition[0], yPosition[0], appleXDim, appleYDim);

                //Set Snek Speed
                System.Threading.Thread.Sleep(Convert.ToInt32(snekSpeed));
                //Check Wall Collision
                isWallHit = DidSnakeHitWall(xPosition[0], yPosition[0]);
                //Game Over
                if (isWallHit)
                {
                    gameIsOn = false;
                    Console.SetCursorPosition(20, 20);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("You broke your spine :(");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadLine();
                }
                if (Console.KeyAvailable) command = Console.ReadKey().Key;
                //if (Console.KeyAvailable) quit = Console.ReadKey().Key;

                //Write score
                Console.SetCursorPosition(25, 0);
                Console.Write(applesEaten);
                SnekCollision(xPosition,yPosition,applesEaten, ref gameIsOn);

            } while (gameIsOn);
                      
            Console.ReadLine();

        }

        private static void PaintSnek(int applesEaten, int[] xPosition1, int[] yPosition1, out int[] xPosition2, out int[] yPosition2)
        {
            //Paint the head
            

            Console.SetCursorPosition(xPosition1[0], yPosition1[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ö");


            //Paint the body
            for(int i = 1; i < applesEaten + 1; i++)
            {
                Console.SetCursorPosition(xPosition1[i], yPosition1[i]);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("O");
            }


            //Erase last body part

            Console.SetCursorPosition(xPosition1[applesEaten + 1], yPosition1[applesEaten + 1]);
            
            Console.Write(' ');

            //Record locations
            for(int i = applesEaten + 2; i > 0; i--)
            {
                xPosition1[i] = xPosition1[i - 1];
                yPosition1[i] = yPosition1[i - 1];
            }

            //Return new array

            xPosition2 = xPosition1;
            yPosition2 = yPosition1;






        }
        //Detect when Apple was eaten
        private static bool AppleWasEaten(int xPosition, int yPosition,int appleXDim,int appleYDim)
        {
            if (xPosition == appleXDim && yPosition == appleYDim) return true; return false;
        }
        
        //Draw Apple
        private static void PaintApple(int appleXDim, int appleYDim)
        {
            Console.SetCursorPosition(appleXDim, appleYDim);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("o");
        }

        private static void SetApplePosition(Random random, out int appleXDim, out int appleYDim)
        {
            appleXDim = random.Next(2, 100);
            appleYDim = random.Next(2, 41);
        }





        //Collision
        private static bool DidSnakeHitWall(int x, int y)
        {
            if (x <= 1 || x >= 100 || y <= 1 || y >= 41) return true; return false;
                                           
        }
        //Draw Borders
        private static void BuildWall()
        {

            for(int i = 1; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("#");
                Console.SetCursorPosition(100, i);
                Console.Write("#");
            }
            for (int i = 1; i< 101; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 41);
                Console.Write("#");
                Console.SetCursorPosition(i, 1);
                Console.Write("#");
            }
        }
        private static void SnekCollision(int[] x, int[] y, int apples, ref bool gameOn)
        {
            for(int i = 2; i < apples+2; i++)
            {
                if (x[0] == x[i] && y[0] == y[i])
                {
                    bool g = false;
                    
                    Console.SetCursorPosition(25, 25);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Stop hitting yourself!");
                    gameOn = false;
                    Console.ReadLine();
                    
                    
                }
            }
            
        }
    }
}
