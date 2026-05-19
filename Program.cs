using System;
using System.Linq;

namespace Lab5_Combined
{
    // ЗАВДАННЯ 1/2
    public abstract class Engine
    {
        protected string model;
        protected double power;

        public double Power
        {
            get => power;
            set => power = value > 0 ? value : 0;
        }

        public string Model => model;

        public Engine()
        {
            model = "Unknown Engine";
            power = 0;
            Console.WriteLine($"[Constructor] Engine: Default initialized.");
        }

        public Engine(string model)
        {
            this.model = model;
            power = 100;
            Console.WriteLine($"[Constructor] Engine: Model '{model}' initialized.");
        }

        public Engine(string model, double power)
        {
            this.model = model;
            this.power = power;
            Console.WriteLine($"[Constructor] Engine: Model '{model}' with Power {power} HP initialized.");
        }

        ~Engine()
        {
            Console.WriteLine($"[Destructor] Engine: '{model}' destroyed.");
        }

        public abstract void Start();

        public virtual void Show()
        {
            Console.Write($"Engine Model: {model}, Power: {power} HP");
        }
    }

    public class InternalCombustionEngine : Engine
    {
        protected int cylinders;

        public InternalCombustionEngine() : base()
        {
            cylinders = 4;
            Console.WriteLine(" -> [Constructor] ICE: Default.");
        }

        public InternalCombustionEngine(string model, double power) : base(model, power)
        {
            cylinders = 4;
            Console.WriteLine(" -> [Constructor] ICE: Model & Power.");
        }

        public InternalCombustionEngine(string model, double power, int cylinders) : base(model, power)
        {
            this.cylinders = cylinders;
            Console.WriteLine($" -> [Constructor] ICE: Full setup ({cylinders} cylinders).");
        }

        ~InternalCombustionEngine()
        {
            Console.WriteLine($"[Destructor] ICE: '{model}' destroyed.");
        }

        public override void Start()
        {
            Console.WriteLine($"[Start] ICE {model} rumbles to life.");
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", Type: ICE, Cylinders: {cylinders}");
        }
    }

    public class DieselEngine : InternalCombustionEngine
    {
        private double compressionRatio;

        public DieselEngine() : base()
        {
            compressionRatio = 16.5;
            Console.WriteLine("   -> [Constructor] Diesel: Default.");
        }

        public DieselEngine(string model, double power) : base(model, power)
        {
            compressionRatio = 18.0;
            Console.WriteLine("   -> [Constructor] Diesel: Model & Power.");
        }

        public DieselEngine(string model, double power, int cylinders, double compressionRatio) 
            : base(model, power, cylinders)
        {
            this.compressionRatio = compressionRatio;
            Console.WriteLine($"   -> [Constructor] Diesel: Full setup (Compression: {compressionRatio}:1).");
        }

        ~DieselEngine()
        {
            Console.WriteLine($"[Destructor] Diesel: '{model}' destroyed.");
        }

        public override void Start()
        {
            Console.WriteLine($"[Start] Diesel {model} clatters to life.");
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"      Additional: Compression Ratio {compressionRatio}:1");
        }
    }

    public class JetEngine : Engine
    {
        private double thrust;

        public JetEngine() : base()
        {
            thrust = 50;
            Console.WriteLine(" -> [Constructor] Jet: Default.");
        }

        public JetEngine(string model, double power) : base(model, power)
        {
            thrust = 100;
            Console.WriteLine(" -> [Constructor] Jet: Model & Power.");
        }

        public JetEngine(string model, double power, double thrust) : base(model, power)
        {
            this.thrust = thrust;
            Console.WriteLine($" -> [Constructor] Jet: Full setup (Thrust: {thrust} kN).");
        }

        ~JetEngine()
        {
            Console.WriteLine($"[Destructor] Jet: '{model}' destroyed.");
        }

        public override void Start()
        {
            Console.WriteLine($"[Start] Jet engine {model} roars.");
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($", Type: Jet, Thrust: {thrust} kN");
        }
    }

    // ЗАВДАННЯ 3

    public abstract class Function
    {
        public string Name { get; protected set; }

        public Function(string name)
        {
            Name = name;
        }

        public abstract double Calculate(double x);

        public virtual void PrintInfo(double x)
        {
            Console.WriteLine($"Функція: {Name} | При x = {x:F2} => y = {Calculate(x):F4}");
        }
    }

    public class Line : Function
    {
        private double a;
        private double b;

        public Line(double a, double b) : base($"Лінійна (y = {a}x + {b})")
        {
            this.a = a;
            this.b = b;
        }

        public override double Calculate(double x)
        {
            return a * x + b;
        }
    }

    public class Quadratic : Function
    {
        private double a;
        private double b;
        private double c;

        public Quadratic(double a, double b, double c) : base($"Квадратична (y = {a}x^2 + {b}x + {c})")
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override double Calculate(double x)
        {
            return a * x * x + b * x + c;
        }
    }

    public class Hyperbola : Function
    {
        private double k;

        public Hyperbola(double k) : base($"Гіпербола (y = {k} / x)")
        {
            this.k = k;
        }

        public override double Calculate(double x)
        {
            if (Math.Abs(x) < 1e-9)
            {
                return double.NaN;
            }
            return k / x;
        }
    }

    // ЗАВДАННЯ 4
    
    public sealed partial class Triangle
    {
        private int a, b, c;
        private int color;

        public Triangle(int a, int b, int c, int color)
        {
            if (IsValid(a, b, c)) { this.a = a; this.b = b; this.c = c; }
            else { this.a = 3; this.b = 4; this.c = 5; }
            this.color = color;
        }

        public object this[int index]
        {
            get => index switch { 0 => a, 1 => b, 2 => c, 3 => color, _ => "Error: Invalid index" };
        }

        private bool IsValid(int a, int b, int c) => a + b > c && a + c > b && b + c > a;

        public override string ToString() => $"{a} {b} {c} {color}";
    }

    public sealed partial class Triangle
    {
        public static Triangle operator ++(Triangle t) { t.a++; t.b++; t.c++; return t; }
        public static Triangle operator --(Triangle t) { t.a--; t.b--; t.c--; return t; }
        public static bool operator true(Triangle t) => t.IsValid(t.a, t.b, t.c);
        public static bool operator false(Triangle t) => !t.IsValid(t.a, t.b, t.c);
        public static Triangle operator *(Triangle t, int scalar) => new Triangle(t.a * scalar, t.b * scalar, t.c * scalar, t.color);
        public static implicit operator string(Triangle t) => t.ToString();
        public static explicit operator Triangle(string s)
        {
            var p = s.Split(' ');
            return new Triangle(int.Parse(p[0]), int.Parse(p[1]), int.Parse(p[2]), int.Parse(p[3]));
        }
    }


    // ГОЛОВНЕ МЕНЮ 
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("        ЛАБОРАТОРНА РОБОТА №5           ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Завдання 1 & 2 (Двигуни)");
                Console.WriteLine("2. Завдання 3 (Функції)");
                Console.WriteLine("3. Завдання 4 (Triangle partial/sealed)");
                Console.WriteLine("0. Вихід");
                Console.WriteLine("========================================");
                Console.Write("Виберіть завдання: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        RunTask1_2();
                        break;
                    case "2":
                        RunTask3();
                        break;
                    case "3":
                        RunTask4();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        break;
                }

                Console.WriteLine("\nНатисніть Enter для повернення в menu...");
                Console.ReadLine();
            }
        }

        static void RunTask1_2()
        {
            Console.WriteLine("--- Демонстрація конструкторів ---");
            InternalCombustionEngine iceTest = new InternalCombustionEngine("V-Tec v6", 250, 6);
            DieselEngine dieselTest = new DieselEngine("TDI Duramax", 350, 8, 19.5);
            JetEngine jetTest = new JetEngine("Pratt & Whitney F135", 40000, 191.3);

            Console.WriteLine("\n--- Створення масиву ---");
            Engine[] engines = new Engine[]
            {
                new InternalCombustionEngine("EcoBoost 2.0", 245, 4),
                new DieselEngine("Cummins 6.7", 400, 6, 16.2),
                new JetEngine("CFM56", 30000, 120),
                new InternalCombustionEngine("Smart-1.0", 71, 3),
                new DieselEngine("1.5 dCi", 110, 4, 15.5),
                new JetEngine("GE90", 115000, 510)
            };

            Console.WriteLine("\n--- Масив до сортування ---");
            foreach (var eng in engines) eng.Show();

            engines = engines.OrderBy(e => e.Power).ToArray();

            Console.WriteLine("\n--- Відсортований масив за Power ---");
            foreach (var eng in engines)
            {
                eng.Show();
                eng.Start();
            }

            Console.WriteLine("\n--- Очищення посилань для деструкторів ---");
            engines = null;
            iceTest = null;
            dieselTest = null;
            jetTest = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        static void RunTask3()
        {
            double targetX = 2.5;
            Console.WriteLine($"--- Обчислення у точці x = {targetX} ---\n");

            Function[] functions = new Function[]
            {
                new Line(2, 3),
                new Quadratic(1, -2, 5),
                new Hyperbola(10),
                new Line(-1.5, 4),
                new Hyperbola(-5)
            };

            foreach (var func in functions)
            {
                func.PrintInfo(targetX);
            }

            Console.WriteLine("\n--- Перевірка ділення на нуль ---");
            Function zeroTest = new Hyperbola(5);
            zeroTest.PrintInfo(0);
        }

        static void RunTask4()
        {
            Triangle t = new Triangle(3, 4, 5, 1);
            Console.WriteLine($"Початковий рядок: {t}");
            Console.WriteLine($"Індексатор [0] (Сторона a): {t[0]}");
            Console.WriteLine($"Індексатор [3] (Колір): {t[3]}");
            Console.WriteLine($"Індексатор [9] (Помилка): {t[9]}");

            if (t) Console.WriteLine("Трикутник валідний.");

            t = t * 2;
            Console.WriteLine($"Після множення на 2: {t}");

            t++;
            Console.WriteLine($"Після інкременту (++): {t}");

            string str = "6 8 10 5";
            Triangle t2 = (Triangle)str;
            Console.WriteLine($"Створено через explicit перетворення: {t2}");
        }
    }
}