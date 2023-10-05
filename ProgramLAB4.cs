using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Matrix
    {
        private int _m = 0, _n = 0, _randA, _randB;
        private List<List<int>> _matrix = new List<List<int>>(0);

        public Matrix(int m, int n)
        {
            Console.WriteLine("Enter rand range [a, b]:\na = ");
            _randA = Int32.Parse(Console.ReadLine());
            Console.WriteLine("b = ");
            _randB = Int32.Parse(Console.ReadLine());
            var rand = new Random();
            _m = m;
            _n = n;
            for (int i = 0; i < m; ++i)
            {
                _matrix.Add(new List<int>());
                for (int j = 0; j < n; ++j)
                {
                    _matrix[i].Add(rand.Next(_randA, _randB));
                }
            }

        }
        public Matrix(int m, int n, int filler)
        {
            _m = m;
            _n = n;
            for (int i = 0; i < m; ++i)
            {
                _matrix.Add(new List<int>());
                for (int j = 0; j < n; ++j)
                {
                    _matrix[i].Add(filler);
                }
            }

        }

        public static Matrix operator +(Matrix mat1, Matrix mat2)
        {
            Matrix rez = new Matrix(mat1._m, mat2._n);
            for (int i = 0; i < mat1._m; ++i)
            {
                for (int j = 0; j < mat1._n; ++j)
                {
                    rez._matrix[i][j] = mat1._matrix[i][j] + mat2._matrix[i][j];
                }
            }
            return rez;
        }
        public static Matrix operator -(Matrix mat1, Matrix mat2)
        {
            Matrix rez = new Matrix(mat1._m, mat2._n);
            for (int i = 0; i < mat1._m; ++i)
            {
                for (int j = 0; j < mat1._n; ++j)
                {
                    rez._matrix[i][j] = mat1._matrix[i][j] - mat2._matrix[i][j];
                }
            }
            return rez;
        }
        public static Matrix operator *(Matrix mat1, int scalar)
        {
            Matrix rez = new Matrix(mat1._m, mat1._n);
            for (int i = 0; i < mat1._m; ++i)
            {
                for (int j = 0; j < mat1._n; ++j)
                {
                    rez._matrix[i][j] = mat1._matrix[i][j] * scalar;
                }
            }
            return rez;
        }
        public static Matrix operator *(int scalar, Matrix mat1)
        {
            Matrix rez = new Matrix(mat1._m, mat1._n);
            for (int i = 0; i < mat1._m; ++i)
            {
                for (int j = 0; j < mat1._n; ++j)
                {
                    rez._matrix[i][j] = mat1._matrix[i][j] * scalar;
                }
            }
            return rez;
        }
        public static Matrix operator /(Matrix mat1, int scalar)
        {
            Matrix rez = new Matrix(mat1._m, mat1._n);
            for (int i = 0; i < mat1._m; ++i)
            {
                for (int j = 0; j < mat1._n; ++j)
                {
                    rez._matrix[i][j] = mat1._matrix[i][j] / scalar;
                }
            }
            return rez;
        }
        public static Matrix operator /(int scalar, Matrix mat1)
        {
            Matrix rez = new Matrix(mat1._m, mat1._n);
            for (int i = 0; i < mat1._m; ++i)
            {
                for (int j = 0; j < mat1._n; ++j)
                {
                    rez._matrix[i][j] = mat1._matrix[i][j] / scalar;
                }
            }
            return rez;
        }
        public static Matrix operator *(Matrix mat1, Matrix mat2)
        {
            Matrix rez = new Matrix(mat1._m, mat2._n, 0);
            for (int i = 0; i < mat1._m; ++i)
            {
                for (int j = 0; j < mat2._n; ++j)
                {
                    for (int k = 0; k < mat1._n; ++k)
                    {
                        rez._matrix[i][j] += mat1._matrix[i][k] * mat2._matrix[k][j];
                    }
                }
            }
            return rez;
        }
        public List<int> this[int index]
        {
            get
            {
                return _matrix[index];
            }
        }
    }

    class Car
    {
        private string _name = "None";
        private int _productionYear = 0, _maxSpeed = 0;

        public Car(string name, int productionYear, int maxSpeed)
        {
            _name = name;
            _productionYear = productionYear;
            _maxSpeed = maxSpeed;
        }

        public string name { get { return _name; } }
        public int productionYear { get { return _productionYear; } }
        public int maxSpeed { get { return _maxSpeed; } }
    }

    class CarCatalog : IEnumerable<Car>
    {
        private List<Car> cars = new List<Car>();
        public void AddCar(Car car)
        {
            cars.Add(car);
        }
        public IEnumerator<Car> GetEnumerator()
        {
            return cars.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerable<Car> ByYear(int year)
        {
            foreach (var car in cars)
            {
                if (car.productionYear == year)
                    yield return car;
            }
        }
        public IEnumerable<Car> ByMaxSpeed(int maxSpeed)
        {
            foreach (var car in cars)
            {
                if (car.maxSpeed <= maxSpeed)
                    yield return car;
            }
        }

        public IEnumerable<Car> Reverse()
        {
            for (int i = cars.Count - 1; i >= 0; i--)
            {
                yield return cars[i];
            }
        }
    }

    class CarComparer : IComparer<Car>
    {
        int _mode = 0;
        public CarComparer(int mode)
        {
            _mode = mode;
        }
        public int Compare(Car car1, Car car2)
        {
            switch (_mode)
            {
                case 0:
                    return car1.name.CompareTo(car2.name);
                case 1:
                    return car1.productionYear.CompareTo(car2.productionYear);
                case 3:
                    return car1.maxSpeed.CompareTo(car2.maxSpeed);
                default:
                    throw new Exception();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ex2 example
            Car car1 = new Car("car1", 2012, 120);
            Car car2 = new Car("car2", 2019, 200);
            Car car3 = new Car("car3", 2015, 180);
            Car[] cars = { car1, car2, car3 };
            Array.Sort(cars, new CarComparer(1));
            for (int i = 0; i < cars.Length; i++) { Console.WriteLine(cars[i].productionYear); }
            //Ex3 example
            CarCatalog catalog = new CarCatalog();
            catalog.AddCar(new Car("Car1", 2000, 150));
            catalog.AddCar(new Car("Car2", 2010, 180));
            catalog.AddCar(new Car("Car3", 2020, 200));
            foreach (var car in catalog)
            {
                Console.WriteLine($"Name: {car.name}, Year: {car.productionYear}, Max Speed: {car.maxSpeed}");
            }
            foreach (var car in catalog.ByYear(2010))
            {
                Console.WriteLine($"Name: {car.name}, Year: {car.productionYear}, Max Speed: {car.maxSpeed}");
            }
            foreach (var car in catalog.ByMaxSpeed(160))
            {
                Console.WriteLine($"Name: {car.name}, Year: {car.productionYear}, Max Speed: {car.maxSpeed}");
            }
            foreach (var car in catalog.Reverse())
            {
                Console.WriteLine($"Name: {car.name}, Year: {car.productionYear}, Max Speed: {car.maxSpeed}");
            }
            Console.ReadKey();
        }
    }
}
