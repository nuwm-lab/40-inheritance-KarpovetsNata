using System;

namespace Triangles
{
    /// <summary>
    /// Абстрактний базовий клас для будь-якого трикутника.
    /// Містить загальні властивості та базову перевірку вхідних даних.
    /// </summary>
    public abstract class TriangleBase
    {
        private double _sideA;
        private double _angleA;
        private double _angleB;

        public double SideA
        {
            get => _sideA;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Довжина сторони має бути > 0.");
                _sideA = value;
            }
        }

        public double AngleA
        {
            get => _angleA;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Кут має бути > 0.");
                _angleA = value;
            }
        }

        public double AngleB
        {
            get => _angleB;
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Кут має бути > 0.");
                _angleB = value;
            }
        }

        protected static void ValidateTriangle(double angleA, double angleB)
        {
            double angleC = 180 - (angleA + angleB);
            if (angleC <= 0)
                throw new ArgumentException("Сума двох кутів має бути < 180°.");
        }

        public abstract void ShowCharacteristics();
        public abstract double Perimeter();
    }

    /// <summary>
    /// Клас рівностороннього трикутника (усі сторони і кути рівні).
    /// </summary>
    public class EquilateralTriangle : TriangleBase
    {
        private const double DefaultAngle = 60.0;

        public EquilateralTriangle(double side)
        {
            SideA = side;
            AngleA = AngleB = DefaultAngle;
        }

        public override void ShowCharacteristics()
        {
            Console.WriteLine("Рівносторонній трикутник:");
            Console.WriteLine($"  Сторони: {SideA}, {SideA}, {SideA}");
            Console.WriteLine($"  Кути: {DefaultAngle}°, {DefaultAngle}°, {DefaultAngle}°");
        }

        public override double Perimeter() => 3 * SideA;
    }

    /// <summary>
    /// Клас загального трикутника, який визначається однією стороною та двома кутами.
    /// </summary>
    public class GeneralTriangle : TriangleBase
    {
        public GeneralTriangle(double side, double angleA, double angleB)
        {
            ValidateTriangle(angleA, angleB);
            SideA = side;
            AngleA = angleA;
            AngleB = angleB;
        }

        private double GetAngleC() => 180 - (AngleA + AngleB);

        private (double sideB, double sideC) CalculateOtherSides()
        {
            double angleC = GetAngleC();
            double sinC = Math.Sin(angleC * Math.PI / 180);
            if (Math.Abs(sinC) < 1e-10)
                throw new ArgumentException("Некоректні кути — трикутник неіснує.");

            double sideB = SideA * Math.Sin(AngleB * Math.PI / 180) / sinC;
            double sideC = SideA * Math.Sin(AngleA * Math.PI / 180) / sinC;

            return (sideB, sideC);
        }

        public override void ShowCharacteristics()
        {
            var (sideB, sideC) = CalculateOtherSides();
            double angleC = GetAngleC();

            Console.WriteLine("Загальний трикутник:");
            Console.WriteLine($"  Кути: {AngleA:0.00}°, {AngleB:0.00}°, {angleC:0.00}°");
            Console.WriteLine($"  Сторони: a={SideA:0.00}, b={sideB:0.00}, c={sideC:0.00}");
        }

        public override double Perimeter()
        {
            var (sideB, sideC) = CalculateOtherSides();
            return SideA + sideB + sideC;
        }
    }

    /// <summary>
    /// Основна програма для демонстрації роботи класів.
    /// </summary>
    public static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Демонстрація роботи трикутників ===\n");

            try
            {
                var eqTriangle = new EquilateralTriangle(5);
                eqTriangle.ShowCharacteristics();
                Console.WriteLine($"  Периметр: {eqTriangle.Perimeter():0.00}\n");

                var genTriangle = new GeneralTriangle(6, 50, 60);
                genTriangle.ShowCharacteristics();
                Console.WriteLine($"  Периметр: {genTriangle.Perimeter():0.00}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка: {ex.Message}");
            }
        }
    }
}
