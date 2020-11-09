using Microsoft.SmallBasic.Library;
using System;

namespace TurtleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Turtle.PenUp();
            GraphicsWindow.KeyDown += GraphicsWindow_KeyDown;

            GraphicsWindow.BrushColor = "Red";
            var food = Shapes.AddRectangle(10, 10);

            int xFoodPosition = 200;
            int yFoodPosition = 200;

            Shapes.Move(food, xFoodPosition, yFoodPosition);

            Random rand = new Random();

            while (true)
            {
                Turtle.Move(10);

                if (Turtle.X >= xFoodPosition && Turtle.X <= xFoodPosition + 10 && Turtle.Y >= yFoodPosition && Turtle.Y <= yFoodPosition + 10)
                {
                    xFoodPosition = rand.Next(0, GraphicsWindow.Width);
                    yFoodPosition = rand.Next(0, GraphicsWindow.Height);
                    Shapes.Move(food, xFoodPosition, yFoodPosition);
                    Turtle.Speed += 1;
                }
            }
        }

        private static void GraphicsWindow_KeyDown()
        {
            if (GraphicsWindow.LastKey == "Up")
            {
                Turtle.Angle = 0;
            }

            else if (GraphicsWindow.LastKey == "Right")
            {
                Turtle.Angle = 90;
            }

            else if (GraphicsWindow.LastKey == "Left")
            {
                Turtle.Angle = 270;
            }

            else if (GraphicsWindow.LastKey == "Down")
            {
                Turtle.Angle = 180;
            }
        }
    }
}
