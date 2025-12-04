using System;

namespace Triangles
{
    // 🔷 Абстрактний базовий клас
    abstract class TriangleBase
    {
        private double _sideA;
        private double _angleA;
        private double _angleB;

        protected TriangleBase(double sideA, double angleA, double angleB)
        {
            SideA = sideA;
            AngleA = angleA;
            AngleB = angleB;
        }

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
                if (value <= 0 || value >= 180)
                    throw new ArgumentException("Кут має бути в межах (0; 180)");
                _angleA = value;
            }
        }

        public double AngleB
        {
            get => _angleB;
            protected set
            {
                if (value <= 0 || value >= 180)
                    throw new ArgumentException("Кут має бути в межах (0; 180)");
                _angleB = value;
            }
        }

        protected double DegToRad(double deg) => deg * Math.PI / 180.0;

        protected double ComputeAngleC() => 180 - (AngleA + AngleB);

        protected void ValidateTriangle()
        {
            double angleC = ComputeAngleC();

            if (angleC <= 0 || angleC >= 180)
                throw new ArgumentException("Сума двох кутів має бути < 180°");
        }

        public abstract void ShowCharacteristics();
        public abstract double Perimeter();
    }

    // 🔹 Клас рівностороннього трикутника
    class EquilateralTriangle : TriangleBase
    {
        private const double AngleConst = 60.0;

        public EquilateralTriangle(double side)
            : base(side, AngleConst, AngleConst)
        {
        }

        public override void ShowCharacteristics()
        {
            Console.WriteLine("Рівносторонній трикутник:");
            Console.WriteLine($"  Сторони: {SideA}, {SideA}, {SideA}");
            Console.WriteLine($"  Кути: {AngleConst}°, {AngleConst}°, {AngleConst}°");
        }

        public override double Perimeter() => 3 * SideA;
    }

    // 🔹 Клас звичайного трикутника
    class Triangle : TriangleBase
    {
        public Triangle(double side, double angle1, double angle2)
            : base(side, angle1, angle2)
        {
            ValidateTriangle();
        }

        private double ComputeSide(double knownSide, double knownAngle, double targetAngle)
        {
            double radKnown = DegToRad(knownAngle);
            double radTarget = DegToRad(targetAngle);
            return knownSide * Math.Sin(radTarget) / Math.Sin(radKnown);
        }

        public override void ShowCharacteristics()
        {
            double angleC = ComputeAngleC();

            double sideB = ComputeSide(SideA, angleC, AngleB);
            double sideC = ComputeSide(SideA, angleC, AngleA);

            Console.WriteLine("Звичайний трикутник:");
            Console.WriteLine($"  Кути: {AngleA:0.00}°, {AngleB:0.00}°, {angleC:0.00}°");
            Console.WriteLine($"  Сторони: a={SideA:0.00}, b={sideB:0.00}, c={sideC:0.00}");
        }

        public override double Perimeter()
        {
            double angleC = ComputeAngleC();
            double sideB = ComputeSide(SideA, angleC, AngleB);
            double sideC = ComputeSide(SideA, angleC, AngleA);
            return SideA + sideB + sideC;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Демонстрація трикутників ===\n");

            try
            {
                var eqTri = new EquilateralTriangle(5);
                eqTri.ShowCharacteristics();
                Console.WriteLine($"  Периметр: {eqTri.Perimeter():0.00}\n");

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
