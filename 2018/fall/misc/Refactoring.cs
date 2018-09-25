using System;
using System.Diagnostics;
using System.Drawing;

namespace RefactorMe
{
    class Drawer
    {
        static Bitmap image = new Bitmap(800, 600);
        public static float xLineStart, yLineStart, xNextStart, yNextStart;
        static Graphics graphics;

        public static void Initialize()
        {
            image = new Bitmap(800, 600);
            graphics = Graphics.FromImage(image);
        }

        public static void SetPosition(float xStartPosition, float yStartPosition)
        {
            xLineStart = xStartPosition;
            yLineStart = yStartPosition;
        }

        public static void Line(double lenght, double angle)
        {
            //Делает шаг длиной lenght в направлении angle и рисует пройденную траекторию
            var xLineEnd = (float)(xLineStart + lenght * Math.Cos(angle));
            var yLineEnd = (float)(yLineStart + lenght * Math.Sin(angle));
            graphics.DrawLine(Pens.Yellow, xLineStart, yLineStart, xLineEnd, yLineEnd);
            xLineStart = xLineEnd;
            yLineStart = yLineEnd;
        }

        public static void ShowResult()
        {
            image.Save("result.bmp");
            Process.Start("result.bmp");
        }
        public static void DrawSide(float xStart, float yStart, double sideAngle, int sideCount)
        {
            //Рисует одну сторону невозможной фигуры с sideCount сторон из точки xStart, yStart под углом sideAngle
            Drawer.SetPosition(xStart, yStart);
            Drawer.Line(100, sideAngle);
            Drawer.Line(10 * Math.Sqrt(2), Math.PI / 4 + sideAngle);
            xNextStart = xLineStart;
            yNextStart = yLineStart;
            Drawer.Line(100, Math.PI + sideAngle);
            Drawer.Line(100 - (double)10, Math.PI * (sideCount - 2) / sideCount + sideAngle);
        }
    }

    public class StrangeThing
    {
        public static void Main()
        {
            Drawer.Initialize();
            double pi = Math.PI;
            int sideCount = 4;

            //Рисуем четыре одинаковые части невозможного квадрата.
            // Часть первая:
            Drawer.DrawSide(200, 200, 0, sideCount);

            // Часть вторая:
            Drawer.DrawSide(Drawer.xNextStart, Drawer.yNextStart, pi / 2, sideCount);

            // Часть третья:
            Drawer.DrawSide(Drawer.xNextStart, Drawer.yNextStart, pi, sideCount);

            // Часть четвертая:
            Drawer.DrawSide(Drawer.xNextStart, Drawer.yNextStart, -pi / 2, sideCount);

            Drawer.ShowResult();
        }
    }
}