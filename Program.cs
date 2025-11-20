using System;

namespace Triangles
{
    // Базовий клас
    class EquilateralTriangle
    {
        protected double side; // довжина сторони
        protected double angle; // кут між сторонами (у градусах)

        public EquilateralTriangle(double side = 1, double angle = 60)
        {
            this.side = side;
            this.angle = angle;
        }

        // Метод для задання значень
        public virtual void SetValues(double side, double angle)
        {
            this.side = side;
            this.angle = angle;
        }

        // Знаходження інших характеристик (всі сторони однакові)
        public virtual void ShowCharacteristics()
        {
            Console.WriteLine($"Рівносторонній трикутник:");
            Console.WriteLine($"  Сторони: {side}, {side}, {side}");
            Console.WriteLine($"  Кути: 60°, 60°, 60°");
        }

        // Обчислення периметра
        public virtual double Perimeter()
        {
            return 3 * side;
        }
    }

    // Похідний клас
    class Triangle : EquilateralTriangle
    {
        private double angle2;

        public Triangle(double side, double angle1, double angle2) : base(side, angle1)
        {
            this.angle2 = angle2;
        }

        // Перевизначення методу для задання нових значень
        public override void SetValues(double side, double angle1)
        {
            base.SetValues(side, angle1);
            this.angle2 = 180 - 2 * angle1; // якщо рівнобедрений
        }

        // Метод для задання довжини сторони та двох кутів
        public void SetValues(double side, double angle1, double angle2)
        {
            this.side = side;
            this.angle = angle1;
            this.angle2 = angle2;
        }

        // Знаходження інших характеристик
        public override void ShowCharacteristics()
        {
            double angle3 = 180 - (angle + angle2);
            double side2 = side * Math.Sin(angle2 * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);
            double side3 = side * Math.Sin(angle * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);

            Console.WriteLine($"Звичайний трикутник:");
            Console.WriteLine($"  Сторони: {Math.Round(side, 2)}, {Math.Round(side2, 2)}, {Math.Round(side3, 2)}");
            Console.WriteLine($"  Кути: {angle}°, {angle2}°, {Math.Round(angle3, 2)}°");
        }

        // Обчислення периметра
        public override double Perimeter()
        {
            double angle3 = 180 - (angle + angle2);
            double side2 = side * Math.Sin(angle2 * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);
            double side3 = side * Math.Sin(angle * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);
            return side + side2 + side3;
        }
    }

    // Основний клас програми
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Об’єкт рівностороннього трикутника
            EquilateralTriangle eqTri = new EquilateralTriangle(5);
            eqTri.ShowCharacteristics();
            Console.WriteLine($"  Периметр: {eqTri.Perimeter():0.00}");
            Console.WriteLine();

            // Об’єкт звичайного трикутника
            Triangle tri = new Triangle(5, 50, 60);
            tri.ShowCharacteristics();
            Console.WriteLine($"  Периметр: {tri.Perimeter():0.00}");
        }
    }
}
