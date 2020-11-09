using Microsoft.SmallBasic.Library;

namespace TurtleTraining
{
    class TurtleGame
    {
        static void WriteT()
        {
            Turtle.Move(80);
            Turtle.TurnLeft();
            Turtle.Move(30);
            Turtle.Turn(180);
            Turtle.Move(60);
        }

        static void WriteO(int size)
        {
            Turtle.Angle = 0;
            int i = 0;
            while (i < 8)
            {
                Turtle.Move(size);
                Turtle.Turn(45);
                i++;
            }
        }

        static void WriteR()
        {
            Turtle.Angle = 0;
            Turtle.Move(80);
            Turtle.TurnRight();
            int i = 0;
            while (i < 5)
            {
                Turtle.Move(22);
                Turtle.Turn(45);
                i++;
            }
        }
        static void Main(string[] args)
        {
            Turtle.Speed = 10;
            Turtle.X = 200;
            Turtle.Y = 200;

            WriteT();

            Turtle.X = 260;
            Turtle.Y = 170;

            WriteO(30);


            Turtle.X = 450;
            Turtle.Y = 200;

            WriteT();

            Turtle.X = 365;
            Turtle.Y = 200;

            WriteR();
            //Turtle.Move(100);

            /* for (int j = 0; j < 4; j++)
             {
                 Turtle.Move(30);
                 Turtle.TurnRight();
                 Turtle.Move(30);
                 Turtle.TurnRight();
                 Turtle.Move(30);
                 Turtle.TurnLeft();
                 Turtle.Move(30);
                 Turtle.TurnLeft();

             }

             int i = 0;
             while (i < 6)
             {
                 Turtle.Move(100);
                 Turtle.Turn(60);
                 i++;
             }*/
        }
    }
}
