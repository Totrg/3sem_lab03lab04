using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    struct Vector
    {
        public int x, y, z;
        public Vector(int X, int Y, int Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
        }
        public static Vector operator *(Vector v1, Vector v2)
        {
            return new Vector(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }
        public static Vector operator *(Vector v1, int scalar)
        {
            return new Vector(v1.x * scalar, v1.y * scalar, v1.z * scalar);
        }
        public static Vector operator *(int scalarVector v1)
        {
            return new Vector(v1.x * scalar, v1.y * scalar, v1.z * scalar);
        }
        public static bool operator ==(Vector v1, Vector v2)
        {
            double len1 = Math.Sqrt(v1.x * v1.x + v1.y * v1.y + v1.z * v1.z);
            double len2 = Math.Sqrt(v2.x * v2.x + v2.y * v2.y + v2.z * v2.z);
            return len1 == len2;
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }
    }

    class Car : IEquatable<Car> 
    {
        private string _name = "None", _engine = "None";
        private int _maxSpeed = 0;

        public Car(string name, string engine, int maxSpeed)
        {
            _name = name;
            _engine = engine;
            _maxSpeed = maxSpeed;
        }

        public override string ToString()
        {
            return _name;
        }

        public bool Equals(Car otherCar)
        {
            if (_name == otherCar._name && _engine == otherCar._engine && _maxSpeed == otherCar._maxSpeed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Info() { return _name + _engine; }
    }

    class CarsCatalog
    {
        private List<Car> _cars = new List<Car>();
        public CarsCatalog(List<Car> cars)
        {
            _cars = cars;
        }

        public string this[int index]
        {
            get
            {
                return _cars[index].Info();
            }
        }

        public void Add(Car newCar)
        {
            _cars.Add(newCar);
        }

        public int Count()
        {
            return _cars.Count;
        }
    }


    class Currency
    {
        public double Value { get; set; }

        public Currency(double value)
        {
            Value = value;
        }
    }

    class CurrencyUSD : Currency
    {
        public CurrencyUSD(double value) : base(value) { }

        public static explicit operator CurrencyEUR(CurrencyUSD usd)
        {
            double eurValue = usd.Value * 0.85;
            return new CurrencyEUR(eurValue);
        }

        public static explicit operator CurrencyRUB(CurrencyUSD usd)
        {
            double rubValue = usd.Value * 75.0;
            return new CurrencyRUB(rubValue);
        }
    }

    class CurrencyEUR : Currency
    {
        public CurrencyEUR(double value) : base(value) { }

        public static explicit operator CurrencyUSD(CurrencyEUR eur)
        {
            double usdValue = eur.Value / 0.85;
            return new CurrencyUSD(usdValue);
        }

        public static explicit operator CurrencyRUB(CurrencyEUR eur)
        {
            double rubValue = eur.Value * 88.24;
            return new CurrencyRUB(rubValue);
        }
    }

    class CurrencyRUB : Currency
    {
        public CurrencyRUB(double value) : base(value) { }

        public static explicit operator CurrencyUSD(CurrencyRUB rub)
        {
            double usdValue = rub.Value / 75.0;
            return new CurrencyUSD(usdValue);
        }

        public static explicit operator CurrencyEUR(CurrencyRUB rub)
        {
            double eurValue = rub.Value / 88.24;
            return new CurrencyEUR(eurValue);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Ex1 Example
            Console.WriteLine(((new Vector(1, 1, 1)) + (new Vector(2, 2, 2))).x);
            Console.WriteLine(((new Vector(3, 3, 3)) * (new Vector(2, 2, 2))).x);
            Console.WriteLine(new Vector(3, 3, 3) == new Vector(2, 2, 2));
            Console.WriteLine(((new Vector(3, 3, 3)) * 2).x);

            //Ex2 Example
            Car car1 = new Car("car1", "type1", 120), car2 = new Car("car2", "type2", 200);
            Car eqCar1 = new Car("car1", "type1", 120);
            Console.WriteLine(car1.ToString());
            Console.WriteLine(car1.Equals(car2));
            Console.WriteLine(car1.Equals(eqCar1));
            CarsCatalog cars = new CarsCatalog(new List<Car>());
            cars.Add(car1);
            cars.Add(car2);
            Console.WriteLine(cars[0]);

            //ex3 ex
            CurrencyUSD usd1 = new CurrencyUSD(1);
            CurrencyEUR eur1 = (CurrencyEUR)usd1;
            Console.WriteLine(eur1.Value);
            CurrencyRUB rub1 = (CurrencyRUB)eur1;
            Console.WriteLine(rub1.Value);
            usd1 = (CurrencyUSD)rub1;
            Console.WriteLine(usd1.Value);
            Console.ReadKey();
        }
    }
}
