using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Properties;

namespace Logic
{
    public class Polynome
    {
        private const byte DEFAULT_CAPACITY = 5;
        private double ACCURACY = Properties.Settings.Default.accuracy;
        private double[] indexes;
        private int power;

        public Polynome(params double[] indexes)
        {
            this.indexes = (double[])indexes.Reverse().ToArray().Clone() ?? new double[DEFAULT_CAPACITY];
            CalculatePower();
        }

        public int Power { get { return power; } }

        public double[] Indexes { get { return (double[])indexes.Clone(); } }

        public Polynome Add(Polynome polynome)
        {
            CheckPolynome(polynome);
            return Sum(polynome);
        }

        public Polynome Substract(Polynome polynome)
        {
            CheckPolynome(polynome);
            return Sum((Minus(polynome)));
        }

        public Polynome Multiply(Polynome polynome)
        {
            CheckPolynome(polynome);

            int length = this.indexes.Length + polynome.indexes.Length;
            double[] resultArr = new double[length];

            for (int i = 0; i < this.indexes.Length; i++)
                for (int j = 0; j < polynome.indexes.Length; j++)
                    resultArr[i + j] += this.indexes[i] * polynome.indexes[j];

            return new Polynome(resultArr.Reverse().ToArray());
        }

        public static Polynome operator +(Polynome p1, Polynome p2)
        {
            CheckPolynome(p1);
            return p1.Add(p2);
        }

        public static Polynome operator -(Polynome p1, Polynome p2)
        {
            CheckPolynome(p1);
            return p1.Substract(p2);
        }

        public static Polynome operator *(Polynome p1, Polynome p2)
        {
            CheckPolynome(p1);
            return p1.Multiply(p2);
        }

        public override string ToString()
        {
            string result = string.Empty;

            for (int i = indexes.Length - 1; i >= 0; i--)
                if (Math.Abs(indexes[i]) > ACCURACY)
                {
                    if (i != power && indexes[i] >= 0)
                        result += "+ ";
                    if (indexes[i] < 0)
                        result += "- ";
                    if (i == 1)
                        result += String.Format("{0}X ", Math.Abs(indexes[i]));
                    else if (i != 0)
                        result += String.Format("{0}X^{1} ", Math.Abs(indexes[i]), i);
                    else
                        result += Math.Abs(indexes[i]);
                }
            return result;
        }

        public override bool Equals(object obj)
        {
            Polynome p;
            if (obj == null)
                return false;
            if (this == obj)
                return true;
            if(obj is Polynome)
            {
                p = (Polynome)obj;
                if (this.power != p.power)
                    return false;
                for (int i = this.power; i >= 0; i--)
                    if ( Math.Abs(indexes[i] - p.Indexes[i]) > ACCURACY)
                        return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            unchecked
            {
                foreach (var elem in indexes)
                    if (elem != 0)
                        hash = hash ^ elem.GetHashCode();
                hash = hash ^ (DEFAULT_CAPACITY * 200) ^ (power * 140);
                return hash;
            }
        }

        private Polynome Minus(Polynome polynome)
        {
            double[] indexes = polynome.Indexes;

            for (int i = 0; i < indexes.Length; i++)
                indexes[i] *= -1;

            return new Polynome(indexes.Reverse().ToArray());
        }

        private Polynome Sum(Polynome polynome)
        {
            int length = Math.Max(this.indexes.Length, polynome.indexes.Length);
            double[] resultArr = new double[length];

            for (int i = 0; i < length; i++)
            {
                resultArr[i] += i < this.indexes.Length ? this.indexes[i] : 0;
                resultArr[i] += i < polynome.indexes.Length ? polynome.indexes[i] : 0;
            }

            return new Polynome(resultArr.Reverse().ToArray());
        }

        private static void CheckPolynome(Polynome p)
        {
            if (p == null)
                throw new ArgumentNullException("Null argument was passed");
        }

        private void CalculatePower()
        {
            int i = indexes.Length - 1;
            while (i >= 0)
            {
                if (indexes[i] != 0)
                {
                    power = i;
                    return;
                }
                i--;
            }
        }

    }
}
