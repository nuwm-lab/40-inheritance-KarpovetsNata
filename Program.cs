using System;

namespace Triangles
{
    // Базовий клас для рівностороннього трикутника
    class EquilateralTriangle
    {
        private double _side; // довжина сторони
        private const double _angle = 60; // всі кути дорівнюють 60°

        public double Side
        {
            get => _side;
            protected set
            {
                if (value <= 0) throw new ArgumentException("Side must be > 0.");
                _side = value;
            }
        }

        public double Angle => _angle;

        public EquilateralTriangle(double side = 1)
        {
            Side = side; // кут завжди 60°
        }

        // Метод для задання значень (без кута — він константний)
        public virtual void SetValues(double side)
        {
            Side = side;
        }

        // Характеристики рівностороннього трикутника
        public virtual void ShowCharacteristics()
        {
            Console.WriteLine($"Рівносторонній трикутник:");
            Console.WriteLine($"  Сторони: {Math.Round(Side, 2)}, {Math.Round(Side, 2)}, {Math.Round(Side, 2)}");
            Console.WriteLine($"  Кути: {Angle}°, {Angle}°, {Angle}°");
        }

        // Периметр
        public virtual double Perimeter()
        {
            return 3 * Side;
        }
    }

    // Похідний клас для загального трикутника
    class Triangle : EquilateralTriangle
    {
        private double _angle1;
        private double _angle2;

        public double Angle1 => _angle1;
        public double Angle2 => _angle2;

        public Triangle(double side, double angle1, double angle2) : base(side)
        {
            SetAngles(angle1, angle2);
        }

        // Не робимо припущення про рівнобедрість. Перевизначення SetValues лише змінює сторону.
        public override void SetValues(double side)
        {
            base.SetValues(side);
        }

        // Метод для задання довжини сторони та двох кутів
        public void SetValues(double side, double angle1, double angle2)
        {
            Side = side;
            SetAngles(angle1, angle2);
        }

        private void SetAngles(double angle1, double angle2)
        {
            if (angle1 <= 0 || angle2 <= 0)
                throw new ArgumentException("Angles must be > 0.");
            if (angle1 + angle2 >= 180)
                throw new ArgumentException("Sum of two angles must be less than 180°.");

            _angle1 = angle1;
            _angle2 = angle2;
        }

        // Знаходження інших характеристик із перевірками
        public override void ShowCharacteristics()
        {
            double angle3 = 180 - (_angle1 + _angle2);
            if (angle3 <= 0)
            {
                Console.WriteLine("Некоректні кути: сума кутів повинна бути < 180°.");
                return;
            }

            double sinA3 = Math.Sin(angle3 * Math.PI / 180);
            if (Math.Abs(sinA3) < 1e-12)
            {
                Console.WriteLine("Некоректні кути: sin(angle3) близький до нуля, ділення неможливе.");
                return;
            }

            double side = Side;
            double side2 = side * Math.Sin(_angle2 * Math.PI / 180) / sinA3;
            double side3 = side * Math.Sin(_angle1 * Math.PI / 180) / sinA3;

            Console.WriteLine($"Звичайний трикутник:");
            Console.WriteLine($"  Сторони: {Math.Round(side, 2)}, {Math.Round(side2, 2)}, {Math.Round(side3, 2)}");
            Console.WriteLine($"  Кути: {Math.Round(_angle1, 2)}°, {Math.Round(_angle2, 2)}°, {Math.Round(angle3, 2)}°");
        }

        // Обчислення периметра з перевірками
        public override double Perimeter()
        {
            double angle3 = 180 - (_angle1 + _angle2);
            double sinA3 = Math.Sin(angle3 * Math.PI / 180);
            if (angle3 <= 0 || Math.Abs(sinA3) < 1e-12)
                throw new InvalidOperationException("Cannot compute perimeter: invalid angles (sum >= 180 or sin(angle3)=0).");

            double side = Side;
            double side2 = side * Math.Sin(_angle2 * Math.PI / 180) / sinA3;
            double side3 = side * Math.Sin(_angle1 * Math.PI / 180) / sinA3;
            return side + side2 + side3;
        }
    }

    // Основний клас програми
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Демонстрація трикутників ===\n");

            try
            {
                // Рівносторонній трикутник
                var eqTri = new EquilateralTriangle(5);
                eqTri.ShowCharacteristics();
                Console.WriteLine($"  Периметр: {eqTri.Perimeter():0.00}\n");

                // Звичайний трикутник
                var tri = new Triangle(6, 50, 60);
                tri.ShowCharacteristics();
                Console.WriteLine($"  Периметр: {tri.Perimeter():0.00}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка: {ex.Message}");
            }
        }
    }
}

