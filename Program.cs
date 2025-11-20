using System;

namespace Triangles
{
    // 🔷 Базовий (можна вважати абстрактним) клас
    class TriangleBase
    {
        // Приватні поля
        private double _sideA;
        private double _angleA;
        private double _angleB;

        // Властивості для контролю доступу
        public double SideA
        {
            get => _sideA;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Довжина сторони має бути > 0");
                _sideA = value;
            }
        }

        public double AngleA
        {
            get => _angleA;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Кут має бути > 0");
                _angleA = value;
            }
        }

        public double AngleB
        {
            get => _angleB;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Кут має бути > 0");
                _angleB = value;
            }
        }

        public virtual void ShowCharacteristics()
        {
            Console.WriteLine("Базовий трикутник");
        }

        public virtual double Perimeter() => 0;
    }

    // 🔹 Клас рівностороннього трикутника
    class EquilateralTriangle : TriangleBase
    {
        private const double _angleConst = 60.0;

        public EquilateralTriangle(double side)
        {
            SideA = side;
        }

        // Рівносторонній — всі сторони та кути однакові
        public override void ShowCharacteristics()
        {
            Console.WriteLine($"Рівносторонній трикутник:");
            Console.WriteLine($"  Сторони: {SideA}, {SideA}, {SideA}");
            Console.WriteLine($"  Кути: {_angleConst}°, {_angleConst}°, {_angleConst}°");
        }

        public override double Perimeter() => 3 * SideA;
    }

    // 🔹 Клас звичайного трикутника
    class Triangle : TriangleBase
    {
        public Triangle(double side, double angle1, double angle2)
        {
            if (side <= 0 || angle1 <= 0 || angle2 <= 0)
                throw new ArgumentException("Довжина сторони і кути мають бути > 0");

            double angle3 = 180 - (angle1 + angle2);
            if (angle3 <= 0)
                throw new ArgumentException("Сума двох кутів має бути < 180°");

            if (Math.Abs(Math.Sin(angle3 * Math.PI / 180)) < 1e-9)
                throw new ArgumentException("Некоректні значення кутів — трикутник неіснує");

            SideA = side;
            AngleA = angle1;
            AngleB = angle2;
        }

        public override void ShowCharacteristics()
        {
            double angle3 = 180 - (AngleA + AngleB);
            double sideB = SideA * Math.Sin(AngleB * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);
            double sideC = SideA * Math.Sin(AngleA * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);

            Console.WriteLine($"Звичайний трикутник:");
            Console.WriteLine($"  Кути: {AngleA:0.00}°, {AngleB:0.00}°, {angle3:0.00}°");
            Console.WriteLine($"  Сторони: a={SideA:0.00}, b={sideB:0.00}, c={sideC:0.00}");
        }

        public override double Perimeter()
        {
            double angle3 = 180 - (AngleA + AngleB);
            double sideB = SideA * Math.Sin(AngleB * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);
            double sideC = SideA * Math.Sin(AngleA * Math.PI / 180) / Math.Sin(angle3 * Math.PI / 180);
            return SideA + sideB + sideC;
        }
    }

    // 🔸 Основна програма
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


